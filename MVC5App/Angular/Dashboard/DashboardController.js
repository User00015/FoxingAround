app.controller('DashboardController', ['$scope', 'encounterService', '$rootScope', function ($scope, encounterService, $rootScope) {

    var finishLoading = function () {
        var params = { email: $rootScope.userProfile.email }

        encounterService.getSavedEncounters(function (encounter) {
            $scope.savedEncounters = null;
            $scope.savedEncounters = _.compact(_.concat($scope.savedEncounters, encounter));
            $scope.encountersChanged = false;
            $scope.isLoadingSavedEncounters = false;
        }, params);

    }

    $scope.$on("finishedAuthenticating", function () {
        finishLoading();
    });

    if ($rootScope.isAuthenticated) {
        finishLoading();
    };
}]);