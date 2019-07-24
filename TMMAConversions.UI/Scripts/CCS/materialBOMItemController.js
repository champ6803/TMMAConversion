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
    $scope.GenerateModel = null;

    $scope.SheetsList = [{ id: 1, name: "BOM Special Pack" },
    { id: 2, name: "BOM CCS Cut and Pack" },
    { id: 3, name: "BOM CCS PMMA" },
    { id: 4, name: "BOM Additive" },
    { id: 5, name: "BOM CCS Syrup" },
    { id: 6, name: "BOM CCS Initiator" },
    { id: 7, name: "BOM Packing Pattern" },
    { id: 8, name: "BOM Gasket" }];

    $scope.OptionsList = [{ id: 1, name: "BOM Create Header" },
    { id: 2, name: "BOM Delete Component (All)" },
    { id: 3, name: "BOM Add Line Item" },
    { id: 4, name: "Change Routing Header" },
    { id: 5, name: "Assign material to Routing" },
    { id: 6, name: "Change Detail Op Routing" },
    { id: 7, name: "Delete Production version" },
    { id: 8, name: "Create Production Version" }];

    $scope.OptionsUniqList = [{ id: 1, name: "BOM Delete Header" }];

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
            url: '/CCS/GetBOMFileView',
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
                url: "/CCS/UploadBOMFile",
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
                        title: 'Error',
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
                url: "/CCS/UploadValidateBOMFile",
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

    $scope.toggleAll = function () {
        var toggleStatus = !$scope.isAllSelected;
        angular.forEach($scope.OptionsList, function (itm) { itm.checked = toggleStatus; });

        angular.forEach($scope.OptionsUniqList, function (itm) { itm.checked = false; }); // set false all
    }

    $scope.unCheckAll = function () {
        angular.forEach($scope.OptionsList, function (itm) { itm.checked = false; });
        $scope.isAllSelected = false;
    }

    $scope.toggleSheetsAll = function () {
        var toggleStatus = !$scope.isSheetsAllSelected;
        angular.forEach($scope.SheetsList, function (itm) { itm.checked = toggleStatus; });
    }

    $scope.optionToggled = function () {
        $scope.isAllSelected = $scope.OptionsList.every(function (itm) { return itm.selected; });

        angular.forEach($scope.OptionsUniqList, function (itm) { itm.checked = false; }); // set false all
    }

    $scope.optionSheetsToggled = function () {
        $scope.isSheetsAllSelected = $scope.SheetsList.every(function (itm) { return itm.selected; })
    }

    $scope.OnGenerateOptions = function (bomFileID, fileName, userSAP, validDateText, path, pageNo) {
        $('#generateOptionsModal').modal();
        $scope.GenerateModel = new Object;
        $scope.GenerateModel.bomFileID = bomFileID;
        $scope.GenerateModel.fileName = fileName;
        $scope.GenerateModel.userSAP = userSAP;
        $scope.GenerateModel.validDateText = validDateText;
        $scope.GenerateModel.path = path;
        $scope.GenerateModel.pageNo = pageNo;
    };

    $scope.OnGenerateCreateTextFile = function (o, a, c) {
        var options = [];

        if (!c[0].checked) {
            angular.forEach(o, function (value, key) {
                if (o[key].checked) {
                    options.push(o[key].name);
                }
            });
        } else {
            angular.forEach(c, function (value, key) {
                if (c[key].checked) {
                    options.push(c[key].name);
                }
            });
        }

        var sheets = [];
        angular.forEach(a, function (value, key) {
            if (a[key].checked) {
                sheets.push(a[key].name);
            }
        });

        if ($scope.GenerateModel.bomFileID, $scope.GenerateModel.fileName, $scope.GenerateModel.userSAP, $scope.GenerateModel.validDateText, $scope.GenerateModel.path, $scope.GenerateModel.pageNo, options.length > 0, sheets.length > 0) {
            var validDate = convertDateFormat(new Date(parseInt($scope.GenerateModel.validDateText.substr(6))));
            $(".loading-screen").show(); // call loading
            var source = {
                'bomFileID': $scope.GenerateModel.bomFileID,
                'fileName': $scope.GenerateModel.fileName,
                'userSAP': $scope.GenerateModel.userSAP,
                'validDateText': validDate,
                'pathText': $scope.GenerateModel.path,
                'pageNo': $scope.GenerateModel.pageNo,
                'options': options,
                'sheets': sheets
            }

            $http({
                method: "post",
                url: "/CCS/GenerateCreateBOMTextFile",
                headers: { 'Content-Type': 'application/json' },
                data: JSON.stringify(source)
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.BOMFileViewModel = response.data;
                    Swal.fire({
                        type: 'success',
                        title: 'Generated'
                    }).then((result) => {
                        if (result.value) {
                            $('.loading-screen').hide();
                        }
                    });
                    // call dowload url
                    window.location.href = "/CCS/DownloadCreateBOMTextFile?fileName=" + $scope.GenerateModel.fileName;

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
                url: "/CCS/GenerateDeleteBOMTextFile",
                headers: { 'Content-Type': 'application/json' },
                data: JSON.stringify(source)
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.BOMFileViewModel = response.data;
                    Swal.fire({
                        type: 'success',
                        title: 'Generated'
                    }).then((result) => {
                        if (result.value) {
                            $('.loading-screen').hide();
                        }
                    });
                    // call dowload url
                    window.location.href = "/CCS/DownloadDeleteBOMTextFile?fileName=" + fileName;
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
            }).then((result) => {
                if (result.value) {
                    $('.loading-screen').hide();
                }
            });
        }
    };

    $scope.init = function () {

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