theApp.controller('SeasonController', function ($scope, $resource, $http) {    
    $scope.getSeason = function () {
        var targetSeason = jQuery("#selectedSeason").val();
        var getTargetSeasonRequest = '/api/Display/Season/' + targetSeason;

        $http.get(getTargetSeasonRequest).success(function (data, status, headers) {
            $scope.targetSeason = data;
            $scope.updateTotalCost();
        });
    }

    $scope.updateTotalCost = function () {
        $scope.totalCost = 0;

        if (typeof $scope.targetSeason !== 'undefined' && $scope.targetSeason !== null) {
            if (typeof $scope.targetSeason.Programs !== 'undefined' && $scope.targetSeason.Programs !== null) {
                jQuery.each($scope.targetSeason.Programs, function (i, val) {
                    if (val.IsSelected == true)
                    {
                        $scope.totalCost += val.Price;
                    }
                });
            }
        }
    }

    $scope.onSelectProgram = function (program) {
        if (typeof program !== 'undefined' && program !== null)
        {
            program.IsSelected = !program.IsSelected;
        }
        $scope.updateTotalCost();
    }

    $scope.savePrograms = function () {
        $http.put('/api/Season/' + $scope.targetSeason.Id + "/Programs/Update", $scope.targetSeason)
            .success(function (data) {
                $scope.getSeason();
                alert('Your selections have been saved');
            })
            .error(function (data) {
                $scope.getSeason();
                alert('There was a problem saving your selections.');
            });
    }
});