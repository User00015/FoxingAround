app.controller('EncounterController', ['$scope', 'encounterService', '$modal', '$timeout', 'authService', 'LoginService', function ($scope, encounterService, $modal, $timeout, authService, loginService) {



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

    var finishAddingNewEncounter = function (item) {
        $scope.savedEncounters = _.filter($scope.savedEncounters, function (enc) {
            return _.size(enc.monsters) > 0;
        });

        $scope.savedEncounters = _.compact(_.concat($scope.savedEncounters, item));
        $scope.encounter = null;
        $scope.encountersChanged = true;
    }

    $scope.saveNewEncounter = function (item) {
        loginService.signIn().then(function() {
            finishAddingNewEncounter(item);
        });
    };

    var finishUpdating = function (item) {
        $scope.savedEncounters = _.filter($scope.savedEncounters, function (enc) {
            return _.size(enc.monsters) > 0;
        });

        if (item != null) {
            _.replace($scope.savedEncounters, item.$$hashKey, item);
        }
        var params = {
            email: authService.userProfile.email,
            encounters: $scope.savedEncounters
        };
        encounterService.saveEncounters(function () {
            $scope.$broadcast("saved", true);
            $scope.encountersChanged = false;
        }, params);
    };

    $scope.updateEncounter = function (item) {
        loginService.signIn().then(function () {
            finishUpdating(item);
        });
    };


    $scope.$on("encountersChanged", function () {
        $scope.encountersChanged = true;
    });

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
    };

    var testDelay = function () {
        $scope.isLoading = true;
        $timeout(function () {
            $scope.isLoading = false;
        }, 2000);
    };

    var finishLoading = function () {
        var params = { email: authService.userProfile.email }

        encounterService.getSavedEncounters(function (encounter) {
            $scope.savedEncounters = null;
            $scope.savedEncounters = _.compact(_.concat($scope.savedEncounters, encounter));
        }, params);
    };

    $scope.loadEncounter = function () {
        loginService.signIn().then(function () {
            finishLoading();
        });
    }
}]);