BookMyTableApp.controller('EditOrderCtrl', function ($scope, $http, $location, $timeout, $routeParams, toaster) {
    
    $scope.OrderID = $routeParams.OrderID;
    $scope.customerid = $routeParams.customerid;

    var EditOrderDetails= new Object();
    var Quantity = 0;
    var totalPrice = 0;
    $scope.Quantity = 0;
    $scope.PlaceorderData = [];
    $scope.EditMenuList = [];
    $scope.TotalPrice = 0;
    $scope.IsApproveStatus = 0;
    var ToatalAmount = 0;

    $scope.HotelDetails = [];

    getEditOrderDetails();

    function getEditOrderDetails(){
        var searchorder = new Object();
        searchorder.OrderID = $scope.OrderID;//$scope.HotelId;
        searchorder.CustomerId = $scope.customerid;

        $http.get("http://108.168.203.227:9100/api/Hotel/GetEditOrderDetails",
            {
                params: {
                    "OrderID": $scope.OrderID,
                    "CustomerId": $scope.customerid
                }
            })
            .then(function (response) {
                $scope.PlaceorderData.CustomerName = response.data.CustomerName;
                $scope.PlaceorderData.EmailID = response.data.EmailID;
                $scope.IsApproveStatus = response.data.IsApproveStatus
                $scope.HotelDetails = response.data.OrderItemDetails;

                for (var i = 0; i < $scope.HotelDetails.length; i++) {
                    $scope.HotelDetails[i].DishTotalAmount = $scope.HotelDetails[i].DishUnitPrice * $scope.HotelDetails[i].Quantity;
                    totalPrice += $scope.HotelDetails[i].DishUnitPrice * $scope.HotelDetails[i].Quantity;
                        Quantity += $scope.HotelDetails[i].Quantity;
                }
                $scope.Quantity = Quantity;
                $scope.TotalPrice = totalPrice;
            });
    };

    $scope.increaseQuantity = function (item) {
        // for display Quantity in ui only
        Quantity++;
        $scope.Quantity = Quantity;
        // for display totalPrice in ui only
        totalPrice = totalPrice + item.DishUnitPrice;
        $scope.TotalPrice = totalPrice;

        item.Quantity++;
        item.DishTotalAmount = 0;
        item.DishTotalAmount = item.DishUnitPrice * item.Quantity;

    }

    $scope.decreaseQuantity = function (item) {
        if (item.Quantity <= 0) {
            item.Quantity = 0;
        } else {
            item.Quantity--;
            // for display Quantity in ui only
            Quantity--;
            $scope.Quantity = Quantity;

            // for display totalPrice in ui only
            totalPrice = totalPrice - item.DishUnitPrice;
            $scope.TotalPrice = totalPrice;
            item.DishTotalAmount = 0;
            item.DishTotalAmount = item.DishUnitPrice * item.Quantity;
        }
    }

    $scope.UpdateMyOrder = function (PlaceOrderform)
    {
        for (var i = 0; i < $scope.HotelDetails.length; i++) {
                $scope.EditMenuList.push($scope.HotelDetails[i])
        }
        EditOrderDetails.UserType = "Customer";
        EditOrderDetails.OrderID = $scope.OrderID;
        EditOrderDetails.TotalAmount = $scope.TotalPrice;
        EditOrderDetails.OrderItemDetails = $scope.EditMenuList;
        $http({
            url: "http://108.168.203.227:9100/api/Hotel/UpdateExistingOrder",
            dataType: 'json',
            method: 'POST',
            data: EditOrderDetails,
            headers: {
                "Content-Type": "application/json"
            }
        }).then(function (response) {
            console.log(response);
            EditOrderDetails = [];
            toaster.pop('success', "Order Updated", "Successfully");
            //$scope.OrderResponse = response.data;
            //$scope.SucessOrderID = $scope.OrderResponse[0].OrderID;
            //$location.path('/Success');
            $timeout(function () {
                $location.path('/');
            }, 2000);
        });
    }

    $scope.CancelMyOrder = function () {
        var data = { OrderId: $scope.OrderID, UserType: "Customer"}
        $http({
            url: "http://108.168.203.227:9100/api/Hotel/CancelOrder",
                method: 'POST',
                data: data,
                headers: {
                    "Content-Type": "application/json"
                }
            })
            .then(function (response) {
            EditOrderDetails = [];
            toaster.pop('success', "Order Cancelled", "Successfully");
            //$scope.SucessOrderID = $scope.OrderResponse[0].OrderID;
            //$location.path('/Success');
            $timeout(function () {
                $location.path('/');
            }, 2000);
        });
    }

});

