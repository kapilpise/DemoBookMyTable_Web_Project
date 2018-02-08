BookMyTableApp.controller('DailyBusinessReportCtrl', function ($scope, $http, $location, $timeout, $routeParams, toaster) {
    debugger;
    $scope.recordAvail = true;
    $scope.recordAvail1 = true;
    $scope.recordAvail3 = false;
    $scope.recordAvail2 = false;
    //getReportData();
    SearchOrderofDate();
    $scope.monthSelectorOptions = {
        start: "year",
        depth: "year"
    };
    $scope.getType = function (x) {
        return typeof x;
    };
    $scope.isDate = function (x) {
        return x instanceof Date;
    };

    $scope.getReportData= function (date){
        SearchOrderofDate(date);
    }

    function SearchOrderofDate(date) {
        debugger
       // var date = $('#customdate').val();
        //var date = $scope.searchform.dateString;
        //$http.get("http://localhost:52530/api/Hotel/GetReportData",
        $http.get("http://108.168.203.227:9100/api/Hotel/GetReportData",
          {
              params: {
                  "date": date
              }
          }).then(function (response) {
              debugger
              if ((response.data[0].AcceptedOrderStatus.length != 0 )) {
                  $scope.recordAvail = false;
                  $scope.recordAvail3 = true;
                  $scope.Acceptedorders = response.data[0].AcceptedOrderStatus;
                  $scope.totalAcceptedAmount = response.data[0].TotalAcceptedOrderPrice;
              }
              else
              {
                  $scope.recordAvail = true;
                  $scope.recordAvail3 = false;
              }
              if (response.data[0].CancelledOrderStatus.length != 0) {
                  $scope.recordAvail1 = false;
                  $scope.recordAvail2 = true;
                  $scope.CancelledOrders = response.data[0].CancelledOrderStatus;
                  $scope.totalCancelledAmount = response.data[0].TotalCancelledOrderPrice;
              }
              else
              {
                  $scope.recordAvail1 = true;
                  $scope.recordAvail2 = false;
              }
          });

     
    }

});