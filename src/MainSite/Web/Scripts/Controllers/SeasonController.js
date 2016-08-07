theApp.controller('SeasonController', function ($scope, $resource, $http) {
    $scope.getSeason = function () {
        var targetSeason = jQuery("#selectedSeason").val();
        var getTargetSeasonRequest = $resource('/api/Display/Season/' + targetSeason);
        $scope.targetSeason = getTargetSeasonRequest.get();
    }

    $scope.isProgramSelected = function (programId, seasonId) {
        var retVal = false;

        if ($scope.selectedPrograms !== 'undefined' && $scope.selectedPrograms !== null) {
            if ($scope.selectedPrograms[programId] !== 'undefined' && $scope.selectedPrograms[programId] !== null) {
                if ($scope.selectedPrograms[programId] === seasonId) {
                    retVal = true;
                }
            }
        }

        return retVal;
    }

    $scope.onSelectProgram = function (programId, seasonId) {
        if (typeof $scope.selectedPrograms === 'undefined' || $scope.selectedPrograms === null) {
            $scope.selectedPrograms = {};
        }

        $scope.selectedComments[programId] = seasonId;
    }
});