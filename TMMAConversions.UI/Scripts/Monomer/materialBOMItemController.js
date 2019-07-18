app.controller("materialBOMItemController", function ($scope, $http) {
    $scope.BOMFileViewModel = typeof BOMFileViewModel !== "undefined" ? BOMFileViewModel ? BOMFileViewModel : {} : {};
    $scope.User = null;
    $scope.File = null;
    $scope.Files = null;
    $scope.RecObjectName = null;
    $scope.ValidDate = getCurrentDate();
    $scope.BOMFileID = null;
    $scope.Order = BOMFileViewModel.Filter.Order;
    $scope.Sort = BOMFileViewModel.Filter.Sort;

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
            url: '/Monomer/GetBOMFileView',
            params: {
                pageNo: page,
                order: $scope.Order,
                sort: $scope.Sort
            },
            headers: { 'Content-Type': 'application/json' }
        }).then(function Success(response) {
            if (response.data.Status) {
                $scope.BOMFileViewModel = response.data;
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

    $scope.OnUploadBOMFile = function () {
        if ($scope.User && $scope.RecObjectName && $scope.ValidDate) {
            var formData = new FormData();
            var totalFiles = document.getElementById("input_upload_file").files.length;
            $('.loading-screen').show(); // show loading
            for (var i = 0; i < totalFiles; i++) {
                var file = document.getElementById("input_upload_file").files[i];
                formData.append("FileUpload", file);
                formData.append("User", $scope.User);
                formData.append("RecObjectName", $scope.RecObjectName);
                formData.append("ValidDate", $scope.ValidDate);
            }

            $http({
                method: "post",
                url: "/Monomer/UploadBOMFile",
                headers: { "Content-Type": undefined },
                data: formData
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.BOMFileViewModel = response.data;
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
                text: 'Input Invalid.'
            });
        }
    };

    $scope.SetBOMFileID = function (bomFileID) {
        $scope.BOMFileID = bomFileID;

        Swal.fire({
            type: 'info',
            title: 'Coming soon'
        });
    }

    $scope.OnUploadValidateBOMFile = function (files) {
        if (files && $scope.BOMFileID) {
            var formData = new FormData();
            var totalFiles = files.length;

            for (var i = 0; i < totalFiles; i++) {
                var file = files[i];
                formData.append("FileUpload", file);
                formData.append("BOMFileID", $scope.BOMFileID);
                formData.append("RecObjectName", $scope.RecObjectName);
            }

            $http({
                method: "post",
                url: "/Monomer/UploadValidateBOMFile",
                headers: { "Content-Type": undefined },
                data: formData
            }).then(function Success(response) {
                if (response.data.Status) {
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
                text: 'No BOMFileID'
            });
        }
    };

    $scope.OnGenerateCreateTextFile = function (bomFileID, fileName, userSAP, validDateText, path, pageNo) {
        if (bomFileID, fileName, userSAP, validDateText, path, pageNo) {
            var validDate = convertDateFormat(new Date(parseInt(validDateText.substr(6))));
            $(".loading-screen").show(); // call loading
            var source = {
                'bomFileID': bomFileID,
                'fileName': fileName,
                'userSAP': userSAP,
                'validDateText': validDate,
                'pathText': path,
                'pageNo': pageNo
            }

            $http({
                method: "post",
                url: "/Monomer/GenerateCreateBOMTextFile",
                headers: { 'Content-Type': 'application/json' },
                data: JSON.stringify(source)
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.BOMFileViewModel = response.data;
                    Swal.fire({
                        type: 'success',
                        title: 'Generated',
                        allowOutsideClick: false
                    }).then((result) => {
                        if (result.value) {
                            $('.loading-screen').hide();
                        }
                    });
                    // call dowload url
                    window.location.href = "/Monomer/DownloadCreateBOMTextFile?fileName=" + fileName;
                } else {
                    Swal.fire({
                        type: 'error',
                        title: 'Oops...',
                        text: response.data.Message,
                        allowOutsideClick: false
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
                    text: 'Something went wrong!',
                    allowOutsideClick: false
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
                text: "Can not Generate Text File",
                allowOutsideClick: false
            }).then((result) => {
                if (result.value) {
                    $('.loading-screen').hide();
                }
            });
        }
    };

    $scope.OnGenerateDeleteTextFile = function (bomFileID, fileName, userSAP, pageNo) {
        if (bomFileID, fileName, userSAP, pageNo) {
            var source = {
                'bomFileID': bomFileID,
                'fileName': fileName,
                'userSAP': userSAP,
                'pageNo': pageNo
            }
            $(".loading-screen").show(); // call loading
            $http({
                method: "post",
                url: "/Monomer/GenerateDeleteBOMTextFile",
                headers: { 'Content-Type': 'application/json' },
                data: JSON.stringify(source)
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.BOMFileViewModel = response.data;
                    Swal.fire({
                        type: 'success',
                        title: 'Generated',
                        allowOutsideClick: false
                    }).then((result) => {
                        if (result.value) {
                            $('.loading-screen').hide();
                        }
                    });
                    // call dowload url
                    window.location.href = "/Monomer/DownloadDeleteBOMTextFile?fileName=" + fileName;
                } else {
                    Swal.fire({
                        type: 'error',
                        title: 'Oops...',
                        text: response.data.Message,
                        allowOutsideClick: false
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
                    text: 'Something went wrong!',
                    allowOutsideClick: false
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
                text: "Can not Generate Text File",
                allowOutsideClick: false
            }).then((result) => {
                if (result.value) {
                    $('.loading-screen').hide();
                }
            });
        }
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