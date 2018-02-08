(function () {
    'use strict';
    //#region authService service declaration 
    angular.module('bookMyTable').factory('authService', authService);
    //#endregion

    //#region authService service Injector 
    authService.$inject = ['$q', '$http', 'localStorageService', 'toastr'];
    //#endregion

    //#region authService main function 
    function authService($q, $http, localStorageService, toastr) {

        var authServiceFactory = {};

        var _authentication = {
            isAuth: false,
            userName: ""
        };

        //#region saveRegistration function 
        var _saveRegistration = function (registration) {

            _logOut();
            console.log(registration)
            return $http.post('api/Account/Register', registration).then(function (response) {
                localStorageService.set('authorizationData', { userName: registration.userName });
                // console.log(response);
                return response;

            });

        };
        //#endregion

        //#region login function 
        var _login = function (loginData) {
            //_logOut();
            debugger
            localStorage.removeItem('userModel')
            localStorageService.remove('authorizationData');

            // sessionStorage.remove('userModel');
            // localStorage.remove('authorizationData');
            _authentication.isAuth = false;
            //_authentication.userName = "";

            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
            var deferred = $q.defer();

            $http.post('token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then(function (response) {
                // getUserDetails(loginData);
                localStorageService.set('authorizationData', { token: response.data.access_token, UserName: loginData.UserName });
                localStorage.setItem('authorizationData', loginData.UserName);
                _authentication.isAuth = true;
                _authentication = loginData;
                _authentication.UserName = loginData.UserName;
                deferred.resolve(response);
               

            }, function (err) {
                toastr.error("Message", "Error while Login check username and password");
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;

        };
        //#endregion

        //Call when user is registration and redirect to dashboard
        var _getUserDetails = function (data) {
            var deferred = $q.defer();
            $http.post("api/Account/GetUser", data).then(function (response) {
                deferred.resolve(response);

            }, function (err, status) {
                console.log(err)
                _logOut();
                deferred.reject(err);
            });
            return deferred.promise;
        }

        // CHECKING USER AUTHENTICATION
        var _authFind = function () {
            var authcheck = localStorageService.get('authorizationData');
            if (authcheck) {
                return true;
            }
            else {
                return false;
            }
        }

        //#region logout function 
        var _logOut = function () {
            localStorage.removeItem('authorizationData')
            localStorageService.remove('authorizationData');
            localStorage.removeItem('userModel');
            localStorage.removeItem('EditSpecialities');

            _authentication.isAuth = false;
            _authentication.UserName = "";
        };
        //#endregion

        //#region fillauth function 
        var _fillAuthData = function () {
            var authData = localStorageService.get('authorizationData');
            if (authData) {
                _authentication.isAuth = true;
                _authentication.UserName = authData.UserName;
            }
        }

        //#endregion
        authServiceFactory.saveRegistration = _saveRegistration;
        authServiceFactory.login = _login;
        authServiceFactory.logOut = _logOut;
        authServiceFactory.fillAuthData = _fillAuthData;
        authServiceFactory.authentication = _authentication;
        authServiceFactory.authFind = _authFind;
        authServiceFactory.getUserDetails = _getUserDetails;

        return authServiceFactory;
    };
    //#endregion
}());