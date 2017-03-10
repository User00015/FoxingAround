app.controller('DashboardController', ['$scope', 'encounterService', '$rootScope', 'dashboardService', function ($scope, encounterService, $rootScope, dashboardService) {

    $scope.isLoadingSavedEncounters = true;

    var finishLoading = function () {
        var params = { email: $rootScope.userProfile.email }

        encounterService.getSavedEncounters(function (encounter) {
            $scope.savedEncounters = null;
            $scope.savedEncounters = _.compact(_.concat($scope.savedEncounters, encounter));
            $scope.encountersChanged = false;
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