app.controller('StatblockModalController', ['$scope', 'encounterService', 'monsterId', function ($scope, encounterService, monsterId) {

    encounterService.getMonsterDetails(function (data) {
        $scope.monsterDetails = data;
    }, monsterId);
}])