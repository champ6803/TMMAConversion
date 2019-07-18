app.controller("workCenterController", function ($scope, $http) {
    $scope.WorkCenterFileViewModel = typeof WorkCenterFileViewModel !== "undefined" ? WorkCenterFileViewModel ? WorkCenterFileViewModel : {} : {};
    $scope.User = null;
    $scope.File = null;
    $scope.Files = null;
    $scope.RecObjectName = null;
    $scope.WorkCenterFileID = null;
    $scope.Order = WorkCenterFileViewModel.Filter.Order;
    $scope.Sort = WorkCenterFileViewModel.Filter.Sort;

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
            url: '/Monomer/GetWorkCenterFileView',
            params: {
                pageNo: page,
                order: $scope.Order,
                sort: $scope.Sort
            },
            headers: { 'Content-Type': 'application/json' }
        }).then(function Success(response) {
            if (response.data.Status) {
                $scope.WorkCenterFileViewModel = response.data;
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

    $scope.OnUploadWorkCenterFile = function () {
        if ($scope.User && $scope.RecObjectName) {
            var formData = new FormData();
            var totalFiles = document.getElementById("input_upload_file").files.length;

            for (var i = 0; i < totalFiles; i++) {
                var file = document.getElementById("input_upload_file").files[i];
                formData.append("FileUpload", file);
                formData.append("User", $scope.User);
                formData.append("RecObjectName", $scope.RecObjectName);
            }

            $http({
                method: "post",
                url: "/Monomer/UploadWorkCenterFile",
                headers: { "Content-Type": undefined },
                data: formData
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.WorkCenterFileViewModel = response.data;

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

    $scope.SetWorkCenterFileID = function (workCenterFileID) {
        $scope.WorkCenterFileID = workCenterFileID;

        Swal.fire({
            type: 'info',
            title: 'Coming soon'
        });
    }

    $scope.OnGenerateCreateTextFile = function (workCenterFileID, fileName, userSAP, pageNo) {
        if (workCenterFileID, fileName, userSAP, pageNo) {
            var source = {
                'workCenterFileID': workCenterFileID,
                'fileName': fileName,
                'userSAP': userSAP,
                'pageNo': pageNo
            }
            $('.loading-screen').show(); // show loading
            $http({
                method: "post",
                url: "/Monomer/GenerateCreateWorkCenterTextFile",
                headers: { 'Content-Type': 'application/json' },
                data: JSON.stringify(source)
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.WorkCenterFileViewModel = response.data;
                    Swal.fire({
                        type: 'success',
                        title: 'Generated'
                    }).then((result) => {
                        if (result.value) {
                            $('.loading-screen').hide(); // hide loading
                        }
                    });
                    // call dowload url
                    window.location.href = "/Monomer/DownloadCreateWorkCenterTextFile?fileName=" + fileName;
                } else {
                    Swal.fire({
                        type: 'error',
                        title: 'Oops...',
                        text: response.data.Message
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
                    text: 'Something went wrong!'
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
                text: "Can not Generate Text File"
            });
        }
    };

    $scope.OnGenerateDeleteTextFile = function (workCenterFileID, fileName, userSAP, pageNo) {
        if (workCenterFileID, fileName, userSAP, pageNo) {
            var source = {
                'workCenterFileID': workCenterFileID,
                'fileName': fileName,
                'userSAP': userSAP,
                'pageNo': pageNo
            }
            $('.loading-screen').show(); // show loading
            $http({
                method: "post",
                url: "/Monomer/GenerateDeleteWorkCenterTextFile",
                headers: { 'Content-Type': 'application/json' },
                data: JSON.stringify(source)
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.WorkCenterFileViewModel = response.data;
                    Swal.fire({
                        type: 'success',
                        title: 'Generated'
                    }).then((result) => {
                        if (result.value) {
                            $('.loading-screen').hide(); // hide loading
                        }
                    });
                    // call dowload url
                    window.location.href = "/Monomer/DownloadDeleteWorkCenterTextFile?fileName=" + fileName;
                } else {
                    Swal.fire({
                        type: 'error',
                        title: 'Oops...',
                        text: response.data.Message
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
                    text: 'Something went wrong!'
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
                text: "Can not Generate Text File"
            });
        }
    };

    $scope.init = function () {

    };
});

$(function () {
    $("#main_menu").children("li").eq(1).addClass("active");

    $('#txt_created_date').datepicker({
        format: 'dd/mm/yyyy',
        todayBtn: true,
        autoclose: true
    });

    $('#btn_created_date').click(function () {
        $('#txt_created_date').datepicker();
    });

    $('#txt_updated_date').datepicker({
        format: 'dd/mm/yyyy',
        todayBtn: true,
        autoclose: true
    });

});