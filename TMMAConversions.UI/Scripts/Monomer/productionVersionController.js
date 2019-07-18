app.controller("productionVersionController", function ($scope, $http) {
    $scope.ProductionVersionFileViewModel = typeof ProductionVersionFileViewModel !== "undefined" ? ProductionVersionFileViewModel ? ProductionVersionFileViewModel : {} : {};
    $scope.User = null;
    $scope.File = null;
    $scope.Files = null;
    $scope.RecObjectName = null;
    $scope.ValidDate = getCurrentDate();
    $scope.ProductionVersionFileID = null;
    $scope.Order = ProductionVersionFileViewModel.Filter.Order;
    $scope.Sort = ProductionVersionFileViewModel.Filter.Sort;

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
            url: '/Monomer/GetProductionVersionFileView',
            params: {
                pageNo: page,
                order: $scope.Order,
                sort: $scope.Sort
            },
            headers: { 'Content-Type': 'application/json' }
        }).then(function Success(response) {
            if (response.data.Status) {
                $scope.ProductionVersionFileViewModel = response.data;
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

    $scope.OnUploadProductionVersionFile = function () {
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
            $('.loading-screen').show(); //show loading
            $http({
                method: "post",
                url: "/Monomer/UploadProductionVersionFile",
                headers: { "Content-Type": undefined },
                data: formData
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.ProductionVersionFileViewModel = response.data;

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

    $scope.OnGenerateCreateTextFile = function (productionVersionFileID, fileName, userSAP, validDateText, pageNo) {
        if (productionVersionFileID, fileName, userSAP, validDateText, pageNo) {
            var validDate = convertDateFormat(new Date(parseInt(validDateText.substr(6))));
            var source = {
                'productionVersionFileID': productionVersionFileID,
                'fileName': fileName,
                'userSAP': userSAP,
                'validDateText': validDate,
                'pageNo': pageNo
            }
            $('.loading-screen').show(); // show loading
            $http({
                method: "post",
                url: "/Monomer/GenerateCreateProductionVersionTextFile",
                headers: { 'Content-Type': 'application/json' },
                data: JSON.stringify(source)
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.ProductionVersionFileViewModel = response.data;
                    Swal.fire({
                        type: 'success',
                        title: 'Generated'
                    }).then((result) => {
                        if (result.value) {
                            $('.loading-screen').hide();
                        }
                    });
                    // call dowload url
                    window.location.href = "/Monomer/DownloadCreateProductionVersionTextFile?fileName=" + fileName;
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

    $scope.OnGenerateChangeTextFile = function (productionVersionFileID, fileName, userSAP, validDateText, pageNo) {
        if (productionVersionFileID, fileName, userSAP, validDateText, pageNo) {
            var validDate = convertDateFormat(new Date(parseInt(validDateText.substr(6))));
            var source = {
                'productionVersionFileID': productionVersionFileID,
                'fileName': fileName,
                'userSAP': userSAP,
                'validDateText': validDate,
                'pageNo': pageNo
            }
            $('loading-screen').show(); // show loading
            $http({
                method: "post",
                url: "/Monomer/GenerateChangeProductionVersionTextFile",
                headers: { 'Content-Type': 'application/json' },
                data: JSON.stringify(source)
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.ProductionVersionFileViewModel = response.data;
                    Swal.fire({
                        type: 'success',
                        title: 'Generated'
                    }).then((result) => {
                        if (result.value) {
                            $('.loading-screen').hide();
                        }
                    });
                    // call dowload url
                    window.location.href = "/Monomer/DownloadChangeProductionVersionTextFile?fileName=" + fileName;
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

    $scope.OnGenerateDeleteTextFile = function (productionVersionFileID, fileName, userSAP, validDateText, pageNo) {
        if (productionVersionFileID, fileName, userSAP, validDateText, pageNo) {
            var validDate = convertDateFormat(new Date(parseInt(validDateText.substr(6))));
            var source = {
                'productionVersionFileID': productionVersionFileID,
                'fileName': fileName,
                'userSAP': userSAP,
                'validDateText': validDate,
                'pageNo': pageNo
            }
            $('.loading-screen').show(); // show loading
            $http({
                method: "post",
                url: "/Monomer/GenerateDeleteProductionVersionTextFile",
                headers: { 'Content-Type': 'application/json' },
                data: JSON.stringify(source)
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.ProductionVersionFileViewModel = response.data;
                    Swal.fire({
                        type: 'success',
                        title: 'Generated'
                    }).then((result) => {
                        if (result.value) {
                            $('.loading-screen').hide();
                        }
                    });
                    // call dowload url
                    window.location.href = "/Monomer/DownloadDeleteProductionVersionTextFile?fileName=" + fileName;
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

    $scope.SetProductionVersionFileID = function (productionVersionFileID) {
        $scope.ProductionVersionFileID = productionVersionFileID;
        Swal.fire({
            type: 'info',
            title: 'Coming soon'
        });
    }

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