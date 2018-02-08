BookMyTableApp.controller('loginController', function ($scope, $http, $location, $timeout, $routeParams, toaster, $rootScope) {
    $scope.loginForm = [
    userName = '',
    password=''
    ];
    $rootScope.loggeduser = false;
    //$scope.BookMyTableLoging = function () {

    //    if ($scope.LoginPageFrom.$valid) {


    //    }

    //};
    $scope.login = function (username, password) {
        debugger
        var model = {
            Email: username,
            Password: password
        };


        $http({
            url: "http://108.168.203.227:9100/api/Hotel/Login",
            //url: "http://localhost:52530/api/Hotel/Login",
            dataType: 'json',
            method: 'Post',
            data: model,
            headers: {
                "Content-Type": "application/json"
            }
        }).then(function (response) {
            debugger
            if(response.data != "no role")
            {
                $('#showorhide2').css("display", "block");
                $('#showorhide1').css("display", "block");
                $('#showorhide3').css("display", "none");
                $('#showorhide4').css("display", "block");
                $('#showorhide5').css("display", "none");
                $location.path('/TotalOrderList/');
            }
            else
                toaster.pop('alert', "Failed", "Incorrect Username or Password");
        });

    }
});