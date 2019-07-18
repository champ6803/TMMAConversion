app.controller("routingWithoutMaterialController", function ($scope, $http) {
    $scope.RoutingWithoutMaterialFileViewModel = typeof RoutingWithoutMaterialFileViewModel !== "undefined" ? RoutingWithoutMaterialFileViewModel ? RoutingWithoutMaterialFileViewModel : {} : {};
    $scope.User = "";
    $scope.File = "";
    $scope.Files = "";
    $scope.RecObjectName = "";
    $scope.RoutingWithoutMaterialFileID = null;

    $scope.ArrayNumber = function (start, end) {
        var input = [];
        total = parseInt(end);

        for (var i = start; i <= total; i++) {
            input.push(i);
        }

        return input;
    };

    $scope.GotoPage = function (page) {
        $http({
            method: 'POST',
            url: '/Monomer/GetBOMFileView',
            params: {
                pageNo: page
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
        }, function Error(response) {
            Swal.fire({
                type: 'error',
                title: 'Oops...',
                text: 'Something went wrong!'
            });
        });
    };

    $scope.OnUploadBOMFile = function () {
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
                url: "/Monomer/UploadBOMFile",
                headers: { "Content-Type": undefined },
                data: formData
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.BOMFileViewModel = response.data;

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

    $scope.SetBOMFileID = function (bomFileID) {
        $scope.BOMFileID = bomFileID;
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
});

$(function () {
    $("#main_menu").children("li").eq(1).addClass("active");
});