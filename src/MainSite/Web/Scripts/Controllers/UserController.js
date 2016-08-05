theApp.controller('UserController', function ($scope, $resource, $http) {
    $scope.pages = function (count) {
        var pages = [];

        for (var i = 1; i <= count; i++) {
            pages.push(i)
        }

        return pages;
    }

    $scope.getUsers = function () {
        $scope.getUsersByPage(1);
    }

    $scope.getUsersByPage = function (page) {
        var getUsers = $resource('/api/Users?page=:page');
        $scope.users = getUsers.get({ page: page });
    }

    $scope.deleteUser = function (id, page) {
        var deleteRequest = $resource('/api/User/:id', { id: id });
        deleteRequest.delete(function (data) {
            $scope.getUsersByPage(page);
        });
    }
});