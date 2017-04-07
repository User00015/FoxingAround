app.controller('EncounterController', ['$rootScope', '$scope', 'encounterService', '$modal', '$timeout',  function ($rootScope, $scope, encounterService, $modal, $timeout) {


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

    $scope.levels = _.range(1, 21); //Hard-coded character levels
    $scope.isLoading = false;
    $scope.environment = $scope.environments[0];
    $scope.encountersChanged = false;

    encounterService.getMonsters(function (monsters) {
        $scope.allMonsters = monsters;
    });

    $scope.getDifficulty = function () {
        if (_.isNil($scope.encounter)) return null;

        var difficulties = $scope.encounter.difficulty;
        if (difficulties.easy >= difficulties.experienceValue) return "Easy";
        if (difficulties.medium >= difficulties.experienceValue) return "Medium";
        if (difficulties.hard >= difficulties.experienceValue) return "Hard";
        if (difficulties.deadly >= difficulties.experienceValue) return "Deadly";
        return "Deadly++";
    };

    $scope.saveNewEncounter = function (item) {
        $scope.isSaving = true;
        var params = {
            email: $rootScope.userProfile.email,
            encounters: null
        };

        encounterService.getSavedEncounters(function (encounter) {
            params.encounters = _.compact(_.concat(item, encounter));
            $scope.encounter = null;
            encounterService.saveEncounters(params).then(function() {
                $scope.isSaving = false;
            });
        }, params);
    };

    $scope.xpFilter = function (monster) {
        if (_.isNil($scope.encounter) || _.isNil($scope.encounter.difficulty)) return true; //Return all if no encounter is present
        var xpFilter = _.nth(_.values($scope.encounter.difficulty), $scope.difficulty.value);
        return (monster.experienceValue <= xpFilter);
    }

    $scope.terrainFilter = function (monster) {
        if ($scope.environment.value === -1) return true; //return all if no Terrain is selected

        return _.findKey(monster.environment, function (value, key) { return key == $scope.environment.type.toLowerCase() && value === "yes"; });
    }

    $scope.createEncounter = function () {
        var params = {
            partyLevel: $scope.levelOfCharacters,
            partySize: $scope.numberOfCharacters,
            difficulty: $scope.difficulty.value,
            environment: $scope.environment.value
        };

        encounterService.emptyMonsterEncounter(function (encounter) {
            $scope.encounter = encounter;
            $scope.isLoadingNewEncounter = false;
        }, params);
    }

    $scope.randomizeEncounter = function () {
        $scope.isLoadingNewEncounter = true;
        var params = {
            partyLevel: $scope.levelOfCharacters,
            partySize: $scope.numberOfCharacters,
            difficulty: $scope.difficulty.value,
            environment: $scope.environment.value
        };

        encounterService.randomizeMonsterEncounter(function (encounter) {
            $scope.encounter = encounter;
            $scope.isLoadingNewEncounter = false;
        }, params);
    };

    var testDelay = function () {
        $scope.isLoadingSavedEncounters = true;
        $timeout(function () {
            $scope.isLoadingSavedEncounters = false;
        }, 10000);
    };

    $scope.addMonster = function (monster) {
        if (_.includes($scope.encounter.monsters, monster)) {
            _.find($scope.encounter.monsters, monster).quantity += 1;
        } else {
            monster.quantity = 1;
            $scope.encounter.monsters = _.concat($scope.encounter.monsters, monster);
        }
    };

    $scope.getTotalMonsters = function() {
        if (_.isNil($scope.encounter)) return 0;
        return _.reduce($scope.encounter.monsters, function(result, value) { return result + value.quantity; }, 0);
    }
}]);