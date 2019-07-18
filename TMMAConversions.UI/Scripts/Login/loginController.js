app.controller("loginController", function ($scope, $http) {
    $scope.UserViewModel = typeof UserViewModel !== "undefined" ? UserViewModel ? UserViewModel : {} : {};
    $scope.UserModel = typeof UserModel !== "undefined" ? UserModel ? UserModel : {} : {};
    $scope.UserModelTemp = angular.copy($scope.UserModel);
    $scope.ErrorUserModel = angular.copy($scope.UserModelTemp);

    $scope.OnValidateUserModel = function (o) {
        var inValid = 0;
        $scope.ErrorUserModel = angular.copy($scope.UserModelTemp);
        if (!o.Username) {
            $scope.ErrorUserModel.Username = "invalid username.";
            $scope.UserModel.Username = "";
            inValid++;
        }

        if (!o.Password) {
            $scope.ErrorUserModel.Password = "invalid password.";
            $scope.UserModel.Password = "";
            inValid++;
        }

        return inValid == 0;
    }

    $scope.ValidDate = getCurrentDate();

    $scope.ArrayNumber = function (start, end) {
        var input = [];
        total = parseInt(end);

        for (var i = start; i <= total; i++) {
            input.push(i);
        }

        return input;
    };

    $scope.OnLogin = function (o) {
        $('.loading-screen').show();

        if ($scope.OnValidateUserModel(o)) {
            var source = {
                username: o.Username,
                password: o.Password
            }

            $http({
                method: "post",
                url: "/Login/OnLogin",
                headers: { 'Content-Type': 'application/json' },
                data: JSON.stringify(source)
            }).then(function Success(response) {
                if (response.data.Status) {
                    $scope.UserViewModel = response.data;
                    Swal.fire({
                        type: 'success',
                        title: response.data.Message,
                        allowOutsideClick: false
                    }).then((result) => {
                        if (result.value) {
                            window.location.href = "/";
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
            $('.loading-screen').hide();
        }
    }

    $scope.checkIfEnterKeyWasPressed = function ($event, o) {
        var keyCode = $event.which || $event.keyCode;
        if (keyCode === 13) {
            $scope.OnLogin(o);
        }

    };
});

$(function () {
    $("#main_menu").children("li").eq(0).addClass("active");

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