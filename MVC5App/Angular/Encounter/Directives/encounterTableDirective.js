app.directive('encounterTable', ['encounterService', '$modal', '$timeout', 'store', function (encounterService, $modal, $timeout, store) {
    return {
        restrict: "AE",
        templateUrl: "Angular/Encounter/Directives/encounterTable.html",
        scope: {
            encounter: '='
        },
        link: function (scope, element, attr) {

            scope.adjustedDifficulty = "";
            scope.toggle = true; //Defaults visible

            scope.getMonsterDetails = function (monster) {
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
            var getDifficulty = function (difficulties) {
                var xp = scope.encounter.difficulty.experienceValue;
                if (difficulties.easy >= xp) return "Easy";
                if (difficulties.medium >= xp) return "Medium";
                if (difficulties.hard >= xp) return "Hard";
                if (difficulties.deadly >= xp) return "Deadly";
                return "Deadly++";
            };

            scope.$watch("encounter", function () {
                if (_.isUndefined(scope.encounter)) 
                    return;

                scope.adjustedDifficulty = getDifficulty(scope.encounter.difficulty);
            });


            var updateEncounters = function () {
                var monstersList = scope.encounter.monsters;
                encounterService.updateEncounters(function (xp) {
                    var difficulty = scope.encounter.difficulty;
                    difficulty.experienceValue = xp;
                    scope.adjustedDifficulty = getDifficulty(difficulty);
                }, monstersList);
            };

            scope.removeMonster = function (monster) {
                if (monster.quantity > 1) {
                    monster.quantity = monster.quantity - 1;
                } else {
                    scope.encounter.monsters = _.filter(scope.encounter.monsters, function (m) {
                        return m.id !== monster.id;
                    });
                }
                updateEncounters();
            }

            scope.addMonster = function (monster) {
                monster.quantity = monster.quantity + 1;
                updateEncounters();
            }

            scope.loadEncounter = function () {
                var profile = store.get('profile');
                var params = { email: profile.email }

                encounterService.getSavedEncounters(function (encounter) {
                    scope.encounter = encounter;
                    scope.adjustedDifficulty = getDifficulty(encounter.difficulty);
                }, params);
            }

            scope.saveEncounter = function () {
                var profile = store.get('profile');
                var encounters = _.concat([], scope.encounter);

                var params = {
                    email: profile.email,
                    encounters: encounters
                };
                encounterService.saveEncounters(function (result) {
                    console.log("Save Encounter response: " + result.statusText + " " + result.status);
                }, params);
            }
        }
    }
}]);