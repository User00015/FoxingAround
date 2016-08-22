app.controller('EncounterController', ['$scope', 'encounterService', '$modal', '$timeout', 'store', function ($scope, encounterService, $modal, $timeout, store) {



    $scope.difficulties = [
{ type: "Easy", value: 0 },
{ type: "Medium", value: 1 },
{ type: "Hard", value: 2 },
{ type: "Deadly", value: 3 }
    ];

    var profile = store.get('profile');

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

    $scope.saveNewEncounter = function (item) {
        if (profile == null) return;
        $scope.savedEncounters = _.filter($scope.savedEncounters, function (enc) {
            return _.size(enc.monsters) > 0;
        });
        $scope.savedEncounters = _.compact(_.concat($scope.savedEncounters, item));

        //var params = {
        //    email: profile.email,
        //    encounters: allEncounters
        //};
        //encounterService.saveEncounters(function (result) {
        //    console.log("Save Encounter response: " + result.statusText + " " + result.status);
        //}, params);
        console.log($scope.savedEncounters);
        $scope.encounter = null;

    };

    $scope.updateEncounter = function (item) {
        if (profile == null) return;

        $scope.savedEncounters = _.filter($scope.savedEncounters, function (enc) {
            return _.size(enc.monsters) > 0;
        });

        if (item != null) {
            _.replace($scope.savedEncounters, item.$$hashKey, item);
        }
        var params = {
            email: profile.email,
            encounters: $scope.savedEncounters
        };
        console.log($scope.savedEncounters);
        //encounterService.saveEncounters(function (result) {
        //    console.log("Save Encounter response: " + result.statusText + " " + result.status);
        //}, params);
    };

    $scope.createEncounters = function () {
        $scope.isLoading = true;
        var params = {
            partyLevel: $scope.levelOfCharacters,
            partySize: $scope.numberOfCharacters,
            difficulty: $scope.difficulty.value,
            environment: $scope.environment.value
        };

        encounterService.postMonsters(function (encounter) {
            $scope.encounter = encounter;
            $scope.isLoading = false;
        }, params);
    }

    var testDelay = function () {
        $scope.isLoading = true;
        $timeout(function () {
            $scope.isLoading = false;
        }, 2000);
    }

    $scope.loadEncounter = function () {
        if (profile == null) return;
        var params = { email: profile.email }

        encounterService.getSavedEncounters(function (encounter) {
            $scope.savedEncounters = null;
            $scope.savedEncounters = _.compact(_.concat($scope.savedEncounters, encounter));
        }, params);
    }


}]);