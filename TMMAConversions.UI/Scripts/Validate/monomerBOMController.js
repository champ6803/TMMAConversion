app.controller("monomerBOMController", function ($scope, $http) {
    $scope.MonomerViewModel = typeof ProductsViewModel !== "undefined" ? ProductsViewModel ? ProductsViewModel : {} : {};

    $scope.ArrayNumber = function (start, end) {
        var input = [];
        total = parseInt(end);

        for (var i = start; i <= total; i++) {
            input.push(i);
        }

        return input;
    };

    $scope.GotoPage = function (page) {
        if (page == 1) {
            $scope.BOMFileViewModel = angular.copy(BOMFileViewModel);
        } else {
            $scope.BOMFileViewModel = angular.copy(BOMFileViewModel);
        }
    };

    $scope.UploadValidateFile = function (bomFileID) {
        if (bomFileID) {

        } else {

        }
    }

    $scope.init = function () {

    };
});

$(function () {
    $("#main_menu").children("li").eq(3).addClass("active");
});