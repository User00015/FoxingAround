app.controller('EncounterController', ['$scope', 'encounterService', function ($scope, encounterService) {

    encounterService.getMonsters(function (data) {
        $scope.monsters = data;
    });


}]);