(function () {
    //'use strict';
    angular
    .module('AW.UserAccount')
    .controller('loginController', loginController);
    loginController.$inject = ['$scope', '$state', 'apiService', 'authService', 'toastr','$rootScope'];
    function loginController($scope, $state, apiService, authService, toastr,$rootScope) {

       

        $scope.showMsgs = false;
        $scope.loginData = {
            userName: "",
            password: ""
        };
        $scope.message = "";      
        $scope.login = function (formValid) {
            if (formValid) {
               // var data = "grant_type=password&username=" + $scope.loginData.userName + "&password=" + $scope.loginData.password;
                //apiService.loginUser('token', data).then(function (response) {
                //    $state.go('patient.dashboard');
                //    console.log(response);
                //    if (response.status == 200) {
                //    } function myerror(response) {
                //    }
                //})
                debugger
                authService.login($scope.loginData).then(function (response) {
                    if (response.statusText == "OK") {
                        $scope.GetUserDetails($scope.loginData);
                       
                        toastr.success('User Login successfully', '');
                      
                    }
                })
            } else {
                $scope.showMsgs = true;
            }
        }
        $scope.GetUserDetails = function (data) {
            authService.getUserDetails(data).then(function (responseData) {
                if (responseData.statusText == "OK") {
                    debugger
                    localStorage.setItem('userModel', JSON.stringify(responseData.data));
                    var userid=responseData.data.userID;
                    localStorage.IdentityData= userid;
                  
                   // $rootScope.IsAuthenticateUser = true;
                    var User_roll = responseData.data.userRole;
                    //if (User_roll == 'Patient') {
                    //    $state.go("patient.dashboard");
                    //}
                //else
                    if (User_roll == 'Professional') {
                        $state.go("admin.Appealwizard");
                    }
                    else if (User_roll == 'Superadmin') {
                        $state.go("admin.Appealwizard");
                    }
                }
                else {
                    var userid = "";
                    localStorage.setItem('IdentityData', userid);
                    //$rootScope.IsAuthenticateUser = false;
                }
            });
        }
    }
}());