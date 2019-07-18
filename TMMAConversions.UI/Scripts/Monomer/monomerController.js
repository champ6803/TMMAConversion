app.controller("monomerController", function ($scope, $http) {
    $scope.ProductsViewModel = typeof ProductsViewModel !== "undefined" ? ProductsViewModel ? ProductsViewModel : {} : {};

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