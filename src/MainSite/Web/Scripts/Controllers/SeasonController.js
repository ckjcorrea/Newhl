theApp.controller('SeasonController', function ($scope, $resource, $http) {
    $scope.getSeason = function () {
        var targetSeason = jQuery("#selectedSeason").val();
        var getTargetSeasonRequest = $resource('/api/Display/Season/' + targetSeason);
        $scope.targetSeason = getTargetSeasonRequest.get();
    }

    $scope.onSelectProgram = function (program) {
        if (typeof program !== 'undefined' && program !== null)
        {
            program.IsSelected = !program.IsSelected;
        }
    }

    $scope.savePrograms = function () {
        $http.put('/api/Season/' + $scope.targetSeason.Id + "/Programs/Update", $scope.targetSeason)
            .success(function (data) {
                $scope.getSeason();
            });
    }

});