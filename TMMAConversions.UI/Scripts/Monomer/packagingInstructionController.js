app.controller("packagingInstructionController", function ($scope, $http) {
    $scope.PackagingInstructionFileViewModel = typeof PackagingInstructionFileViewModel !== "undefined" ? PackagingInstructionFileViewModel ? PackagingInstructionFileViewModel : {} : {};
    $scope.User = null;
    $scope.File = null;
    $scope.Files = null;
    $scope.RecObjectName = null;
    $scope.PackagingInstructionFileID = null;
    $scope.Order = PackagingInstructionFileViewModel.Filter.Order;
    $scope.Sort = PackagingInstructionFileViewModel.Filter.Sort;

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
            url: '/Monomer/GetPackagingInstructionFileView',
            params: {
                pageNo: page,
                order: $scope.Order,
                sort: $scope.Sort
            },
            headers: { 'Content-Type': 'application/json' }
        }).then(function Success(response) {
            if (response.data.Status) {
                $scope.PackagingInstructionFileViewModel = response.data;
            } else {
                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: response.data.Message
                });
            }
            $('.loading-screen').hide();
        }, function Error(response) {
            Swal.fire({
                type: 'error',
                title: 'Oops...',
                text: 'Something went wrong!'
            });
            $('.loading-screen').hide();
        });
    };

    $scope.OnUploadPackagingInstructionFile = function () {
        if ($scope.User && $scope.RecObjectName && $scope.ValidDate) {
            var formData = new FormData();
            var totalFiles = document.getElementById("input_upload_file").files.length;

            for (var i = 0; i < totalFiles; i++) {
                var file = document.getElementById("input_upload_file").files[i];
                formData.append("FileUpload", file);
                formData.append("User", $scope.User);
                formData.append("RecObjectName", $scope.RecObjectName);
            }
            $('.loading-screen').show(); // show loading
            $http({
                method: "post",
                url: "/Monomer/UploadPackagingInstructionFile",
                headers: { "Content-Type": undefined },
                data: formData
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.PackagingInstructionFileViewModel = response.data;

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

    $scope.SetPackagingInstructionFileID = function (packagingInstructionFileID) {
        $scope.PackagingInstructionFileID = packagingInstructionFileID;
    }

    $scope.OnUploadValidatePackagingInstructionFile = function (files) {
        if (files && $scope.BOMFileID) {
            var formData = new FormData();
            var totalFiles = files.length;

            for (var i = 0; i < totalFiles; i++) {
                var file = files[i];
                formData.append("FileUpload", file);
                formData.append("BOMFileID", $scope.BOMFileID);
                formData.append("RecObjectName", $scope.RecObjectName);
            }
            $('.loading-screen').show();
            $http({
                method: "post",
                url: "/Monomer/UploadValidateBOMFile",
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
                text: 'No BOMFileID'
            });
        }
    };
    
    $scope.SetPackagingInstructionFileID = function (packagingInstructionFileID) {
        $scope.PackagingInstructionFileID = packagingInstructionFileID;
        Swal.fire({
            type: 'info',
            title: 'Coming soon'
        });
    }
});

$(function () {
    $("#main_menu").children("li").eq(1).addClass("active");
});