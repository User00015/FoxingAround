app.controller('EncounterController', ['$scope', 'encounterService', function ($scope, encounterService) {

    encounterService.getMonsters(function (data) {
        $scope.monsters = data;
    });

    $scope.difficulties = [
        { type: "Easy", value: 0 },
        { type: "Medium", value: 1 },
        { type: "Hard", value: 2 },
        { type: "Deadly", value: 3 }
    ];

    var levels = [];

    for (var i = 1; i <= 20; ++i) {
        levels.push(i);
    };

    //Defaults
    $scope.difficulty = $scope.difficulties[2];
    $scope.toggle = true;
    $scope.levels = levels;

    var submit = function (params) {
        encounterService.postMonsters(function (encounter) {
            $scope.encounter = encounter.monsters;
        }, params);
    };

    $scope.createEncounters = function () {
        var params = {
            partyLevel: $scope.levelOfCharacters,
            partySize: $scope.numberOfCharacters,
            difficulty: $scope.difficulty.value
        };
        console.log(params);
        submit(params);

    }


}]);