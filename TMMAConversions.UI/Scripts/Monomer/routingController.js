app.controller("routingController", function ($scope, $http) {
    $scope.RoutingFileViewModel = typeof RoutingFileViewModel !== "undefined" ? RoutingFileViewModel ? RoutingFileViewModel : {} : {};
    $scope.User = null;
    $scope.File = null;
    $scope.Files = null;
    $scope.RecObjectName = null;
    $scope.ValidDate = getCurrentDate();
    $scope.BOMFileID = null;
    $scope.Order = RoutingFileViewModel.Filter.Order;
    $scope.Sort = RoutingFileViewModel.Filter.Sort;

    $scope.ArrayNumber = function (start, end) {
        var input = [];
        total = parseInt(end);

        for (var i = start; i <= total; i++) {
            input.push(i);
        }

        return input;
    };

    $scope.OnOrdering = function (col) {
        if (col) {
            $scope.Sort = $scope.Order != col ? "asc" : $scope.Sort == "asc" ? "desc" : "asc";
            $scope.Order = col;
            $scope.GotoPage(1);
        }
    }

    $scope.GotoPage = function (page) {
        $('.loading-screen').show(); // show loading
        $http({
            method: 'POST',
            url: '/Monomer/GetRoutingFileView',
            params: {
                pageNo: page,
                order: $scope.Order,
                sort: $scope.Sort
            },
            headers: { 'Content-Type': 'application/json' }
        }).then(function Success(response) {
            if (response.data.Status) {
                $scope.RoutingFileViewModel = response.data;
            } else {
                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: response.data.Message
                });
            }
            $('.loading-screen').hide(); // hide loading
        }, function Error(response) {
            Swal.fire({
                type: 'error',
                title: 'Oops...',
                text: 'Something went wrong!'
            });
            $('.loading-screen').hide(); // hide loading
        });
    };

    $scope.OnUploadRoutingFile = function () {
        if ($scope.User && $scope.RecObjectName && $scope.ValidDate) {
            var formData = new FormData();
            var totalFiles = document.getElementById("input_upload_file").files.length;

            for (var i = 0; i < totalFiles; i++) {
                var file = document.getElementById("input_upload_file").files[i];
                formData.append("FileUpload", file);
                formData.append("User", $scope.User);
                formData.append("RecObjectName", $scope.RecObjectName);
                formData.append("ValidDate", $scope.ValidDate);
            }
            $('.loading-screen').show(); // show loading
            $http({
                method: "post",
                url: "/Monomer/UploadRoutingFile",
                headers: { "Content-Type": undefined },
                data: formData
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.RoutingFileViewModel = response.data;
                    Swal.fire({
                        type: 'success',
                        title: 'Uploaded'
                    }).then((result) => {
                        if (result.value) {
                            $('.loading-screen').hide();
                        }
                    });
                } else {
                    Swal.fire({
                        type: 'error',
                        title: 'Oops...',
                        text: response.data.Message
                    }).then((result) => {
                        if (result.value) {
                            $('.loading-screen').hide();
                        }
                    });
                }
            }, function Error(response) {
                Swal.fire({
                    type: 'error',
                    title: 'Error',
                    text: 'Something went wrong!'
                }).then((result) => {
                    if (result.value) {
                        $('.loading-screen').hide();
                    }
                });
            });
        } else {
            Swal.fire({
                type: 'error',
                title: 'Error',
                text: 'User Invalid.'
            });
        }
    }

    $scope.OnGenerateCreateTextFile = function (routingFileID, fileName, userSAP, validDateText, pageNo) {
        if (routingFileID, fileName, userSAP, validDateText, pageNo) {
            var validDate = convertDateFormat(new Date(parseInt(validDateText.substr(6))));
            var source = {
                'routingFileID': routingFileID,
                'fileName': fileName,
                'userSAP': userSAP,
                'validDateText': validDate,
                'pageNo': pageNo
            }
            $('.loading-screen').show(); // show loading
            $http({
                method: "post",
                url: "/Monomer/GenerateCreateRoutingTextFile",
                headers: { 'Content-Type': 'application/json' },
                data: JSON.stringify(source)
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.RoutingFileViewModel = response.data;
                    Swal.fire({
                        type: 'success',
                        title: 'Generated'
                    }).then((result) => {
                        if (result.value) {
                            $('.loading-screen').hide(); // hide loading
                        }
                    });
                    // call dowload url
                    window.location.href = "/Monomer/DownloadCreateRoutingTextFile?fileName=" + fileName;
                } else {
                    Swal.fire({
                        type: 'error',
                        title: 'Oops...',
                        text: response.data.Message
                    }).then((result) => {
                        if (result.value) {
                            $('.loading-screen').hide();
                        }
                    });
                }
            }, function Error(response) {
                Swal.fire({
                    type: 'error',
                    title: 'Error',
                    text: 'Something went wrong!'
                }).then((result) => {
                    if (result.value) {
                        $('.loading-screen').hide();
                    }
                });
            });
        }
        else {
            Swal.fire({
                type: 'error',
                title: 'Error',
                text: "Can not Generate Text File"
            });
        }
    };

    $scope.SetRoutingFileID = function (routingFileID) {
        $scope.RoutingFileID = routingFileID;

        Swal.fire({
            type: 'info',
            title: 'Coming soon'
        });
    }

});

$(function () {
    $("#main_menu").children("li").eq(1).addClass("active");

    $('#txt_valid_date').datepicker({
        format: 'dd/mm/yy',
        todayBtn: true,
        autoclose: true,
        dateFormat: 'dd/mm/yy'
    });

    $('#btn_valid_date').click(function () {
        $('#txt_valid_date').datepicker('show');
    });
});