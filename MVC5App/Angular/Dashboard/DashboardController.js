app.controller('DashboardController', ['$scope', 'encounterService', '$rootScope', function ($scope, encounterService, $rootScope) {

    var finishLoading = function () {
        $scope.isLoadingSavedEncounters = true;
        var params = { email: $rootScope.userProfile.email }

        encounterService.getSavedEncounters(function (encounter) {
            $scope.savedEncounters = _.compact(_.concat($scope.savedEncounters, encounter));
            $scope.isLoadingSavedEncounters = false;
        }, params);

    }

    $scope.saveEncounters = function() {
        console.log("saved");
    }

    $scope.foo = function() {
        console.log("foo");
    }

    $scope.$on("finishedAuthenticating", function () {
        finishLoading();
    });

    if ($rootScope.isAuthenticated) {
        finishLoading();
    };
}]);