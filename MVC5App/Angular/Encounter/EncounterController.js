app.controller('EncounterController', ['$scope', 'encounterService', function ($scope, encounterService) {

    encounterService.getMonsters(function (data) {
        $scope.monsters = data;
    });

    $scope.toggle = true;

    var submit = function (params) {
        encounterService.postMonsters(function(encounter) {
            $scope.encounter = encounter.monsters;
            console.log($scope.encounter);
        }, params);
    };

    $scope.createEncounters = function() {
        var params = [];
        for (var i = 0; i < $scope.numberOfCharacters; ++i) {
            params.push($scope.levelOfCharacters);
        }
        submit(params);

    }


}]);