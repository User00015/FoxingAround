app.controller('EncounterController', ['$scope', 'encounterService', function ($scope, encounterService) {

    encounterService.getMonsters(function (data) {
        $scope.monsters = data;
    });


    $scope.getMonsterDetails = function(id) {
        encounterService.getMonsterDetails(function(data) {
            $scope.monsterDetails = data;
        }, id);
    }

    $scope.difficulties = [
        { type: "Easy", value: 0 },
        { type: "Medium", value: 1 },
        { type: "Hard", value: 2 },
        { type: "Deadly", value: 3 }
    ];

    var levels = _.range(1, 20);

    //Defaults
    $scope.difficulty = $scope.difficulties[2]; //Default to Hard
    $scope.toggle = true; //Defaults visible
    $scope.levels = levels; //Hard-coded character levels

    var submit = function (params) {
        encounterService.postMonsters(function (encounter) {
            $scope.encounter = encounter;
        }, params);
    };

    $scope.createEncounters = function () {
        var params = {
            partyLevel: $scope.levelOfCharacters,
            partySize: $scope.numberOfCharacters,
            difficulty: $scope.difficulty.value
        };
        submit(params);

    }



}]);