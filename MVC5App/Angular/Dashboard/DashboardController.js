app.controller('DashboardController', ['$scope', 'encounterService', '$rootScope', '$templateCache', function ($scope, encounterService, $rootScope, $templateCache) {

    $scope.isLoadingSavedEncounters = false;

    var finishLoading = function () {
        $scope.isLoadingSavedEncounters = true;
        var params = { email: $rootScope.userProfile.email }

        encounterService.getSavedEncounters(function (encounter) {
            $scope.savedEncounters = _.compact(_.concat($scope.savedEncounters, encounter));
            $scope.isLoadingSavedEncounters = false;
        }, params);
    }

    $scope.delete = function(encounter) {
        console.log(encounter);
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