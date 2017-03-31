app.controller('DashboardController', ['$scope', 'encounterService', '$rootScope', function ($scope, encounterService, $rootScope) {

    $scope.isLoadingSavedEncounters = true;

    var finishLoading = function () {
        $scope.isLoadingSavedEncounters = true;
        var params = { email: $rootScope.userProfile.email }

        encounterService.getSavedEncounters(function (encounter) {
            $scope.savedEncounters = _.compact(_.concat($scope.savedEncounters, encounter));
            $scope.isLoadingSavedEncounters = false;
        }, params);

    }

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










    $scope.groups = [
     {
         title: 'Dynamic Group Header - 1',
         content: 'Dynamic Group Body - 1'
     },
     {
         title: 'Dynamic Group Header - 2',
         content: 'Dynamic Group Body - 2'
     }
    ];

    $scope.items = ['Item 1', 'Item 2', 'Item 3'];

    $scope.addItem = function () {
        var newItemNo = $scope.items.length + 1;
        $scope.items.push('Item ' + newItemNo);
    };

    $scope.status = {
        isFirstOpen: true,
        isFirstDisabled: false
    };

}]);