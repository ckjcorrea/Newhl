theApp.controller('RequestTokenController', function ($scope, $resource, $http) {
    $scope.pages = function (count) {
        var pages = [];

        for (var i = 1; i <= count; i++) {
            pages.push(i)
        }

        return pages;
    }

    $scope.getTokens = function () {
        $scope.getTokensByPage(1);
    }

    $scope.getTokensByPage = function (page) {
        if ($scope.searchForm.consumerKey !== undefined && $scope.searchForm.consumerKey) {
            $scope.getTokensByPageAndConsumerKey(page, $scope.searchForm.consumerKey, $scope.searchForm.startDate, $scope.searchForm.endDate);
        }
        else {
            $scope.getTokensByPageAndUserName(page, $scope.searchForm.userName, $scope.searchForm.startDate, $scope.searchForm.endDate);
        }
    }

    $scope.getTokensByPageAndConsumerKey = function (page, consumerKey, startDate, endDate) {
        var getTokensRequest = $resource('/api/Consumer/:consumerKey/RequestTokens?page=:page&startDate=:startDate&endDate=:endDate');
        $scope.requestTokens = getTokensRequest.get({page: page, consumerKey: consumerKey, startDate: startDate, endDate: endDate});
    }

    $scope.getTokensByPageAndUserName = function (page, userName, startDate, endDate) {
        var getTokensRequest = $resource('/api/User/:userName/RequestTokens?page=:page&startDate=:startDate&endDate=:endDate');
        $scope.requestTokens = getTokensRequest.get({ page: page, userName: userName, startDate: startDate, endDate: endDate });
    }

    $scope.deleteRequestToken = function (id, page) {
        var deleteTokenRequest = $resource('/api/RequestToken/:id', {id: id});
        deleteTokenRequest.delete(function (data) {
            $scope.getTokensByPage(page);
        });
    }
});