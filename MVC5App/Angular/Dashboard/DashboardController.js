app.controller('DashboardController', ['$scope', 'encounterService', '$rootScope', 'dashboardService', function ($scope, encounterService, $rootScope, dashboardService) {

    $scope.isLoadingSavedEncounters = true;

    var finishLoading = function () {
        $scope.isLoadingSavedEncounters = true;
        var params = { email: $rootScope.userProfile.email }

        encounterService.getSavedEncounters(function (encounter) {
            $scope.savedEncounters = _.compact(_.concat($scope.savedEncounters, encounter));
            $scope.isLoadingSavedEncounters = false;
        }, params);

    }

    $scope.setEncounter = function (savedEncounter) {
        $scope.monsters = [];
        //dashboardService.encounter = savedEncounter;
        //console.log(dashboardService);
        _.each(savedEncounter.monsters, function (monster) {
            encounterService.getMonsterDetails(function (data) {
                _.push($scope.monsters, data);
            }, monster.id);
        });
    };

    $scope.report = function() {
        console.log($scope.monsters);
    };

    $scope.$on("finishedAuthenticating", function () {
        finishLoading();
    });

    if ($rootScope.isAuthenticated) {
        finishLoading();
    };
}]);