
(function () {
    'use strict';
    //#region authService service declaration 
    angular.module('bookMyTable').factory('authInterceptorService', authInterceptorService);
    //#endregion

    //#region authService service Injector 
    authInterceptorService.$inject = ['$q', '$location', 'localStorageService'];
    //#endregion

    //#region authInterceptorService main function 
    function authInterceptorService($q, $location, localStorageService) {

        var authInterceptorServiceFactory = {};

        var _request = function (config) {

            config.headers = config.headers || {};

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }

            return config;
        }

        var _responseError = function (rejection) {
            if (rejection.status === 401) {
                //toastr.error("Message", "Permission Denied")
                localStorage.removeItem('authorizationData')
                localStorageService.remove('authorizationData');
                localStorage.removeItem('userModel');
                $location.path('/login');
            }
            return $q.reject(rejection);
        }

        authInterceptorServiceFactory.request = _request;
        authInterceptorServiceFactory.responseError = _responseError;

        return authInterceptorServiceFactory;
    }
    //#endregion
}());