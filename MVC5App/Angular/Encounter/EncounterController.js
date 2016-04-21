app.controller('EncounterController', ['$scope', 'encounterService', function ($scope, encounterService) {

    encounterService.getMonsters(function (data) {
        $scope.monsters = data;
    });

   

    var submit = function (params) {
        encounterService.postMonsters(params);
    };

    $scope.createEncounters = function() {
        var params = {
            characters: $scope.numberOfCharacters,
            levels: $scope.levelOfCharacters
        };
        submit(params);

    }


}]);