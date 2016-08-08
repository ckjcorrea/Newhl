theApp.controller('PaymentController', function ($scope, $resource, $http) {
    $scope.getSeasonPaymentDetails = function () {
        var targetSeason = jQuery("#selectedSeason").val();
        var getTargetSeasonRequest = '/api/Season/' + targetSeason + '/Registered/Payment';

        $http.get(getTargetSeasonRequest).success(function (data, status, headers) {
            $scope.seasonPaymentDetails = data;
            $scope.calculateAmountPaid();
        });
    }

    $scope.calculateAmountPaid = function () {
        $scope.amountPaid = 0;

        if (typeof $scope.seasonPaymentDetails !== 'undefined' && $scope.seasonPaymentDetails !== null) {
            if (typeof $scope.seasonPaymentDetails.Payments !== 'undefined' && $scope.seasonPaymentDetails.Payments !== null) {
                jQuery.each($scope.seasonPaymentDetails.Payments, function (i, val) {
                     $scope.amountPaid += val.Amount;
                });
            }
        }
    }
});