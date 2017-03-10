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

    $scope.setEncounter = function(savedEncounter) {
        dashboardService.encounter = savedEncounter;
        console.log(dashboardService);
    };

    $scope.$on("finishedAuthenticating", function () {
        finishLoading();
    });

    if ($rootScope.isAuthenticated) {
        finishLoading();
    };
}]);