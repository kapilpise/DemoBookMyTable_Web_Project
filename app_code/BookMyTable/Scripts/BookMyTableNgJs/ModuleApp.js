
var BookMyTableApp = angular.module('bookMyTable', ['ngRoute', 'toaster',"kendo.directives"]);

BookMyTableApp.config(function ($routeProvider,$locationProvider) {

       // $urlRouterProvider.otherwise("/Home");
        $routeProvider
            .when("/Login", {
                templateUrl: "../../Scripts/BookMyTableNgJs/Views/Login.html",
                controller: 'loginController'
            })
           .when("/", {
                templateUrl: "../../Scripts/BookMyTableNgJs/Views/index.html",
                controller: 'BookMyTableCtrl'
            }).when("/menuList/:DineID", {
                templateUrl: "../../Scripts/BookMyTableNgJs/Views/DishPage.html",
                controller: 'BookMyTableCtrl'
            })
            .when("/ManageOrder", {
                templateUrl: "../../Scripts/BookMyTableNgJs/Views/ManageOrder.html",
                controller: 'BookMyTableCtrl'
            }).
            when("/DailyBusinessReport", {
                templateUrl: "../../Scripts/BookMyTableNgJs/Views/DailYBusinessReport.html",
                controller: 'DailyBusinessReportCtrl'
            }).
            when("/ManagerEditOrder/:OrderID/:customerid", {
                templateUrl: "../../Scripts/BookMyTableNgJs/Views/EditDishbyManagerPage.html",
                controller: 'ManagerEditOrderCtrl'
            }).
             when("/TotalOrderList", {
                 templateUrl: "../../Scripts/BookMyTableNgJs/Views/TotalOrderList.html",
                 controller: 'BookMyTableCtrl'
             }).
            when("/Success", {
                templateUrl: "../../Scripts/BookMyTableNgJs/Views/Success.html",
                controller: 'BookMyTableCtrl'               
                //controller: 'routeDemoSecondController'
            }).
            when('/EditOrder/:OrderID/:customerid', {
            templateUrl: "../../Scripts/BookMyTableNgJs/Views/EditDishPage.html",
            controller: 'EditOrderCtrl'
            }).otherwise("/");
});

      