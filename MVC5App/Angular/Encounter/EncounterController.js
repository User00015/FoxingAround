app.controller('EncounterController', ['$scope', 'encounterService', '$modal', '$timeout', function ($scope, encounterService, $modal, $timeout) {

    $scope.getMonsterDetails = function (id) {
        $modal({
            templateUrl: 'Angular/Encounter/Statblock.html',
            controller: 'StatblockModalController',
            backdrop: true,
            resolve: {
                monsterId: function () {
                    return id;
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

    //Defaults
    $scope.difficulty = $scope.difficulties[2]; //Default to Hard
    $scope.adjustedDifficulty = ""; 
    $scope.toggle = true; //Defaults visible
    $scope.levels = _.range(1, 21); //Hard-coded character levels
    $scope.isLoading = false;


    var getDifficulty = function(xp) {
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
            $scope.adjustedDifficulty = getDifficulty(encounter.difficulty.experienceValue);
            $scope.isLoading = false;
        }, params);
    };

    var testDelay = function () {
        $scope.isLoading = true;
        $timeout(function () {
            $scope.isLoading = false;
        }, 2000);
    }

    $scope.removeMonster = function (id) {
        $scope.encounter.monsters = _.filter($scope.encounter.monsters, function (monster) {
            return monster.id !== id;
        });
        updateEncounters();
    }

    $scope.addMonster = function (monster) {
        monster.quantity = monster.quantity + 1;
        updateEncounters();
    }

    //Not used. If there's a need to fine tune monsters this much, I'll turn it back on.
    $scope.subtractMonster = function (monster) {

        //Don't drop below 1 monster. Deleting is handled separately.
        if (monster.quantity > 1) {
            monster.quantity = monster.quantity - 1;
            updateEncounters();
        }
    }

    $scope.createEncounters = function () {
        $scope.isLoading = true;
        var params = {
            partyLevel: $scope.levelOfCharacters,
            partySize: $scope.numberOfCharacters,
            difficulty: $scope.difficulty.value
        };
        submit(params);
    }
}]);