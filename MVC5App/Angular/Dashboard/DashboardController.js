app.controller('DashboardController', ['$scope', 'encounterService', '$rootScope', function ($scope, encounterService, $rootScope) {

    $scope.isLoadingSavedEncounters = false;

    var finishLoading = function () {
        $scope.isLoadingSavedEncounters = true;

        encounterService.getSavedEncounters(function (encounter) {
            $scope.savedEncounters = _.compact(_.concat($scope.savedEncounters, encounter));
            $scope.isLoadingSavedEncounters = false;
        });
    }

    $scope.delete = function (encounter) {
        _.remove($scope.savedEncounters, encounter);

        var params = {
            email: null,
            encounters: $scope.savedEncounters
        };

        encounterService.saveEncounters(params);
    };

    $scope.selectEncounter = function (savedEncounter) {
        $scope.monsters = [];
        _.each(savedEncounter.monsters, function (monster) {
            encounterService.getMonsterDetails(function (data) {
                _.push($scope.monsters, data);
            }, monster.id);
        });
    };

    $scope.$on("finishedAuthenticating", function () {
        finishLoading();
    });

    if ($rootScope.isAuthenticated) {
        finishLoading();
    };

}]);