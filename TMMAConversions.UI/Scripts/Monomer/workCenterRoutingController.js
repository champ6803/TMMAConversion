app.controller("workCenterRoutingController", function ($scope, $http) {
    $scope.WorkCenterRoutingFileViewModel = typeof WorkCenterRoutingFileViewModel !== "undefined" ? WorkCenterRoutingFileViewModel ? WorkCenterRoutingFileViewModel : {} : {};
    $scope.User = null;
    $scope.File = null;
    $scope.Files = null;
    $scope.RecObjectName = null;
    $scope.ValidDate = getCurrentDate();
    $scope.WorkCenterRoutingFileID = null;
    $scope.Order = WorkCenterRoutingFileViewModel.Filter.Order;
    $scope.Sort = WorkCenterRoutingFileViewModel.Filter.Sort;

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
            url: '/Monomer/GetWorkCenterRoutingFileView',
            params: {
                pageNo: page,
                order: $scope.Order,
                sort: $scope.Sort
            },
            headers: { 'Content-Type': 'application/json' }
        }).then(function Success(response) {
            if (response.data.Status) {
                $scope.WorkCenterRoutingFileViewModel = response.data;
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

    $scope.OnUploadWorkCenterRoutingFile = function () {
        if ($scope.User && $scope.RecObjectName) {
            var formData = new FormData();
            var totalFiles = document.getElementById("input_upload_file").files.length;

            for (var i = 0; i < totalFiles; i++) {
                var file = document.getElementById("input_upload_file").files[i];
                formData.append("FileUpload", file);
                formData.append("User", $scope.User);
                formData.append("RecObjectName", $scope.RecObjectName);
                formData.append("ValidDate", $scope.ValidDate);
            }

            $http({
                method: "post",
                url: "/Monomer/UploadWorkCenterRoutingFile",
                headers: { "Content-Type": undefined },
                data: formData
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.WorkCenterRoutingFileViewModel = response.data;

                    Swal.fire({
                        type: 'success',
                        title: 'Uploaded'
                    });
                } else {
                    Swal.fire({
                        type: 'error',
                        title: 'Oops...',
                        text: response.data.Message
                    });
                }
            }, function Error(response) {
                Swal.fire({
                    type: 'error',
                    title: 'Error',
                    text: 'Something went wrong!'
                });
            });
        } else {
            Swal.fire({
                type: 'error',
                title: 'Error',
                text: 'Input Invalid.'
            });
        }
    };

    $scope.SetWorkCenterRoutingFileID = function (workCenterRoutingFileID) {
        $scope.WorkCenterRoutingFileID = workCenterRoutingFileID;

        Swal.fire({
            type: 'info',
            title: 'Coming soon'
        });
    }

    $scope.OnGenerateCreateTextFile = function (workCenterRoutingFileID, fileName, userSAP, validDateText, path, pageNo) {
        if (workCenterRoutingFileID, fileName, userSAP, validDateText, pageNo) {
            var validDate = convertDateFormat(new Date(parseInt(validDateText.substr(6))));
            var source = {
                'workCenterRoutingFileID': workCenterRoutingFileID,
                'fileName': fileName,
                'userSAP': userSAP,
                'validDateText': validDate,
                'pathText': path,
                'pageNo': pageNo
            }
            $('.loading-screen').show(); // show loading
            $http({
                method: "post",
                url: "/Monomer/GenerateWorkCenterRoutingTextFile",
                headers: { 'Content-Type': 'application/json' },
                data: JSON.stringify(source)
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.WorkCenterRoutingFileViewModel = response.data;
                    Swal.fire({
                        type: 'success',
                        title: 'Generated',
                        allowOutsideClick: false
                    }).then((result) => {
                        if (result.value) {
                            $('.loading-screen').hide(); // hide loading
                        }
                    });
                    // call dowload url
                    window.location.href = "/Monomer/DownloadWorkCenterRoutingTextFile?fileName=" + fileName;
                } else {
                    Swal.fire({
                        type: 'error',
                        title: 'Oops...',
                        text: response.data.Message,
                        allowOutsideClick: false
                    }).then((result) => {
                        if (result.value) {
                            $('.loading-screen').hide(); // hide loading
                        }
                    });
                }
            }, function Error(response) {
                Swal.fire({
                    type: 'error',
                    title: 'Error',
                    text: 'Something went wrong!',
                    allowOutsideClick: false
                }).then((result) => {
                    if (result.value) {
                        $('.loading-screen').hide(); // hide loading
                    }
                });
            });
        }
        else {
            Swal.fire({
                type: 'error',
                title: 'Error',
                text: "Can not Generate Text File",
                allowOutsideClick: false
            });
        }
    };

    $scope.OnGenerateDeleteTextFile = function (workCenterRoutingFileID, fileName, userSAP, pageNo) {
        if (workCenterRoutingFileID, fileName, userSAP, pageNo) {
            var source = {
                'workCenterRoutingFileID': workCenterRoutingFileID,
                'fileName': fileName,
                'userSAP': userSAP,
                'pageNo': pageNo
            }
            $('.loading-screen').show(); // show loading
            $http({
                method: "post",
                url: "/Monomer/GenerateDeleteWorkCenterRoutingTextFile",
                headers: { 'Content-Type': 'application/json' },
                data: JSON.stringify(source)
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.WorkCenterRoutingFileViewModel = response.data;
                    Swal.fire({
                        type: 'success',
                        title: 'Generated',
                        allowOutsideClick: false
                    }).then((result) => {
                        if (result.value) {
                            $('.loading-screen').hide(); // hide loading
                        }
                    });
                    // call dowload url
                    window.location.href = "/Monomer/DownloadDeleteWorkCenterRoutingTextFile?fileName=" + fileName;
                } else {
                    Swal.fire({
                        type: 'error',
                        title: 'Oops...',
                        text: response.data.Message,
                        allowOutsideClick: false
                    }).then((result) => {
                        if (result.value) {
                            $('.loading-screen').hide(); // hide loading
                        }
                    });
                }
            }, function Error(response) {
                Swal.fire({
                    type: 'error',
                    title: 'Error',
                    text: 'Something went wrong!',
                    allowOutsideClick: false
                }).then((result) => {
                    if (result.value) {
                        $('.loading-screen').hide(); // hide loading
                    }
                });
            });
        }
        else {
            Swal.fire({
                type: 'error',
                title: 'Error',
                text: "Can not Generate Text File",
                allowOutsideClick: false
            });
        }
    };

    $scope.init = function () {

    };
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