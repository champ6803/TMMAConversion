var app = angular.module("app", []);

app.directive('file', function () {
    return {
        scope: {
            file: '='
        },
        link: function (scope, el, attrs) {
            el.bind('change', function (event) {
                var file = event.target.files[0];
                scope.file = file ? file : undefined;
                scope.$apply();
            });
        }
    };
});

app.directive('keyEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.keyEnter);
                });

                event.preventDefault();
            }
        });
    };
});

function getCurrentDate() {
    var d = new Date();
    var year = d.getFullYear();
    var month = d.getMonth() + 1;
    if (month < 10) {
        month = "0" + month;
    };

    var day = d.getDate();

    if (day < 10) {
        day = "0" + day;
    }


    return day + "/" + month + "/" + year;
}

function convertDateFormat(d) {
    var year = d.getFullYear();
    var month = d.getMonth() + 1;
    if (month < 10) {
        month = "0" + month;
    };

    var day = d.getDate();

    if (day < 10) {
        day = "0" + day;
    }
    return day + "/" + month + "/" + year;
}