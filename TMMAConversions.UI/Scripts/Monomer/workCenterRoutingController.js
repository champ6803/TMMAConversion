app.controller("workCenterRoutingController", function ($scope, $http) {
    $scope.WorkCenterRoutingFileViewModel = typeof WorkCenterRoutingFileViewModel !== "undefined" ? WorkCenterRoutingFileViewModel ? WorkCenterRoutingFileViewModel : {} : {};

    $scope.SheetsList = [{ id: 1, name: "MMA Grade" },
    { id: 2, name: "MMA Loading" },
    { id: 3, name: "CCS Syrup" },
    { id: 4, name: "CCS Initiator" },
    { id: 5, name: "CCS Additive" },
    { id: 6, name: "CCS Casting" },
    { id: 7, name: "CCS Cut and Pack" },
    { id: 8, name: "CCS Cut and Pack Cullet" },
    { id: 9, name: "CCS Gasket" },
    { id: 10, name: "CCS Roof" },
    { id: 11, name: "CCS Heat Sealing" },
    { id: 12, name: "CCS Reprocess" }];

    $scope.OptionsList = [{ id: 1, name: "Delete Operation Routing" },
    { id: 2, name: "Delete Work Center" },
    { id: 3, name: "Create Work Center" },
    { id: 4, name: "Create Routing header" },
    { id: 5, name: "Add Operation Routing (w/o Standard value key)" }];



    $scope.SelectedSheet = null;
    $scope.SelectedOption = null;
    $scope.User = null;
    $scope.File = null;
    $scope.Files = null;
    $scope.RecObjectName = null;
    $scope.ValidDate = getCurrentDate();
    $scope.WorkCenterRoutingFileID = null;
    $scope.Order = WorkCenterRoutingFileViewModel.Filter.Order;
    $scope.Sort = WorkCenterRoutingFileViewModel.Filter.Sort;
    $scope.GenerateModel = null;

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

    $scope.toggleAll = function () {
        var toggleStatus = !$scope.isAllSelected;
        angular.forEach($scope.OptionsList, function (itm) { itm.checked = toggleStatus; });
    }

    $scope.toggleSheetsAll = function () {
        var toggleStatus = !$scope.isSheetsAllSelected;
        angular.forEach($scope.SheetsList, function (itm) { itm.checked = toggleStatus; });
    }

    $scope.optionToggled = function () {
        $scope.isAllSelected = $scope.options.every(function (itm) { return itm.selected; })
    }

    $scope.optionSheetsToggled = function () {
        $scope.isSheetsAllSelected = $scope.options.every(function (itm) { return itm.selected; })
    }

    $scope.OnGenerateOptions = function (workCenterRoutingFileID, fileName, userSAP, validDateText, path, pageNo) {
        $('#generateOptionsModal').modal();
        $scope.GenerateModel = new Object;
        $scope.GenerateModel.workCenterRoutingFileID = workCenterRoutingFileID;
        $scope.GenerateModel.fileName = fileName;
        $scope.GenerateModel.userSAP = userSAP;
        $scope.GenerateModel.validDateText = validDateText;
        $scope.GenerateModel.path = path;
        $scope.GenerateModel.pageNo = pageNo;
    };

    $scope.OnGenerateCreateTextFile = function (o, a) {
        var options = [];
        angular.forEach(o, function (value, key) {
            if (o[key].checked) {
                options.push(o[key].name);
            }
        });

        var sheets = [];
        angular.forEach(a, function (value, key) {
            if (a[key].checked) {
                sheets.push(a[key].name);
            }
        });

        if ($scope.GenerateModel.workCenterRoutingFileID, $scope.GenerateModel.fileName, $scope.GenerateModel.userSAP, $scope.GenerateModel.validDateText, $scope.GenerateModel.pageNo, options.length > 0, sheets.length > 0) {
            var validDate = convertDateFormat(new Date(parseInt($scope.GenerateModel.validDateText.substr(6))));
            var source = {
                'workCenterRoutingFileID': $scope.GenerateModel.workCenterRoutingFileID,
                'fileName': $scope.GenerateModel.fileName,
                'userSAP': $scope.GenerateModel.userSAP,
                'validDateText': validDate,
                'pathText': $scope.GenerateModel.path,
                'pageNo': $scope.GenerateModel.pageNo,
                'options': options,
                'sheets': sheets
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
                    window.location.href = "/Monomer/DownloadWorkCenterRoutingTextFile?fileName=" + $scope.GenerateModel.fileName;
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
        //}
        //else {
        //    Swal.fire({
        //        type: 'error',
        //        title: 'Error',
        //        text: "Please Select Sheet",
        //        allowOutsideClick: false
        //    });
        //}
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