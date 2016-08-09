app.controller('EncounterController', ['$scope', 'encounterService', '$modal', '$timeout', function ($scope, encounterService, $modal, $timeout) {

    $scope.getMonsterDetails = function (monster) {
        $modal({
            templateUrl: 'Angular/Encounter/Statblock.html',
            controller: 'StatblockModalController',
            backdrop: true,
            resolve: {
                monsterId: function () {
                    return monster.id;
                }
            }
        });
    }

    $scope.difficulties = [
{ type: "Easy", value: 0 },
{ type: "Medium", value: 1 },
{ type: "Hard", value: 2 },
{ type: "Deadly", value: 3 }
    ];

    $scope.environments = [
        { type: "", value: -1 },
        { type: "Arctic", value: 0 },
        { type: "Coastal", value: 1 },
        { type: "Desert", value: 2 },
        { type: "Forest", value: 3 },
        { type: "Grassland", value: 4 },
        { type: "Hill", value: 5 },
        { type: "Mountain", value: 6 },
        { type: "Swamp", value: 7 },
        { type: "Underdark", value: 8 },
        { type: "Underwater", value: 9 },
        { type: "Urban", value: 10 }
    ];

    //Defaults
    $scope.difficulty = $scope.difficulties[2]; //Default to Hard
    $scope.adjustedDifficulty = "";
    $scope.toggle = true; //Defaults visible
    $scope.levels = _.range(1, 21); //Hard-coded character levels
    $scope.isLoading = false;
    $scope.environment = $scope.environments[0];


    var getDifficulty = function (xp) {
        var difficulties = $scope.encounter.difficulty;
        if (difficulties.easy >= xp) return "Easy";
        if (difficulties.medium >= xp) return "Medium";
        if (difficulties.hard >= xp) return "Hard";
        if (difficulties.deadly >= xp) return "Deadly";
        return "Deadly++";
    };

    var updateEncounters = function () {
        var monstersList = $scope.encounter.monsters;
        encounterService.updateEncounters(function (xp) {
            var difficulty = $scope.encounter.difficulty;
            difficulty.experienceValue = xp;
            $scope.adjustedDifficulty = getDifficulty(xp);
        }, monstersList);
    };

    var submit = function (params) {
        encounterService.postMonsters(function (encounter) {
            $scope.encounter = encounter;
            $scope.adjustedDifficulty = getDifficulty(encounter.encounterExperience );
            $scope.isLoading = false;
        }, params);
    };

    var testDelay = function () {
        $scope.isLoading = true;
        $timeout(function () {
            $scope.isLoading = false;
        }, 2000);
    }

    $scope.removeMonster = function (monster) {
        if (monster.quantity > 1) {
            monster.quantity = monster.quantity - 1;
        } else {
            $scope.encounter.monsters = _.filter($scope.encounter.monsters, function (m) {
                return m.id !== monster.id;
            });
        }
        updateEncounters();
    }

    $scope.addMonster = function (monster) {
        monster.quantity = monster.quantity + 1;
        updateEncounters();
    }

    $scope.createEncounters = function () {
        $scope.isLoading = true;
        var params = {
            partyLevel: $scope.levelOfCharacters,
            partySize: $scope.numberOfCharacters,
            difficulty: $scope.difficulty.value,
            environment: $scope.environment.value
        };
        submit(params);
    }
}]);