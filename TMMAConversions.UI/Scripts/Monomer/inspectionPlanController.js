app.controller("inspectionPlanController", function ($scope, $http) {
    $scope.InspectionPlanFileViewModel = typeof InspectionPlanFileViewModel !== "undefined" ? InspectionPlanFileViewModel ? InspectionPlanFileViewModel : {} : {};
    $scope.User = null;
    $scope.File = null;
    $scope.Files = null;
    $scope.RecObjectName = null;
    $scope.ValidDate = getCurrentDate();
    $scope.InspectionPlanFileID = null;
    $scope.Order = InspectionPlanFileViewModel.Filter.Order;
    $scope.Sort = InspectionPlanFileViewModel.Filter.Sort;

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
        $('.loading-screen').show();
        $http({
            method: 'POST',
            url: '/Monomer/GetInspectionPlanFileView',
            params: {
                pageNo: page,
                order: $scope.Order,
                sort: $scope.Sort
            },
            headers: { 'Content-Type': 'application/json' }
        }).then(function Success(response) {
            if (response.data.Status) {
                $scope.InspectionPlanFileViewModel = response.data;
                $('.loading-screen').hide();
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
                title: 'Oops...',
                text: 'Something went wrong!',
                allowOutsideClick: false
            }).then((result) => {
                if (result.value) {
                    $('.loading-screen').hide();
                }
            });
        });
    };

    $scope.OnUploadInspectionPlanFile = function () {
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
                url: "/Monomer/UploadInspectionPlanFile",
                headers: { "Content-Type": undefined },
                data: formData
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.InspectionPlanFileViewModel = response.data;

                    Swal.fire({
                        type: 'success',
                        title: 'Uploaded',
                        allowOutsideClick: false
                    }).then((result) => {
                        if (result.value) {
                            $('.loading-screen').hide();
                        }
                    });
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
        } else {
            Swal.fire({
                type: 'error',
                title: 'Error',
                text: 'Input Invalid.'
            });
        }
    };

    $scope.SetInspectionPlanFileID = function (inspectionPlanFileID) {
        $scope.InspectionPlanFileID = inspectionPlanFileID;
        Swal.fire({
            type: 'info',
            title: 'Coming soon'
        });
    }

    $scope.OnUploadValidateInspectionPlanFile = function (files) {
        if (files && $scope.InspectionPlanFileID) {
            var formData = new FormData();
            var totalFiles = files.length;

            for (var i = 0; i < totalFiles; i++) {
                var file = files[i];
                formData.append("FileUpload", file);
                formData.append("InspectionPlanFileID", $scope.InspectionPlanFileID);
                formData.append("RecObjectName", $scope.RecObjectName);
            }
            $('.loading-screen').show();
            $http({
                method: "post",
                url: "/Monomer/UploadValidateInspectionPlanFile",
                headers: { "Content-Type": undefined },
                data: formData
            }).then(function Success(response) {
                if (response.data.Status) {
                    Swal.fire({
                        type: 'success',
                        title: 'Uploaded',
                        allowOutsideClick: false
                    }).then((result) => {
                        if (result.value) {
                            $('.loading-screen').hide();
                        }
                    });
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
        } else {
            Swal.fire({
                type: 'error',
                title: 'Error',
                text: 'No InspectionPlanFileID'
            });
        }
    };

    $scope.OnGenerateCreateTextFile = function (inspectionPlanFileID, fileName, userSAP, validDateText, path, pageNo) {
        if (inspectionPlanFileID, fileName, userSAP, validDateText, pageNo) {
            var validDate = convertDateFormat(new Date(parseInt(validDateText.substr(6))));
            $(".loading-screen").show(); // call loading
            var source = {
                'inspectionPlanFileID': inspectionPlanFileID,
                'fileName': fileName,
                'userSAP': userSAP,
                'validDateText': validDate,
                'pathText': path,
                'pageNo': pageNo
            }

            $http({
                method: "post",
                url: "/Monomer/GenerateCreateInspectionPlanTextFile",
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
                    window.location.href = "/Monomer/DownloadCreateInspectionPlanTextFile?fileName=" + fileName;
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