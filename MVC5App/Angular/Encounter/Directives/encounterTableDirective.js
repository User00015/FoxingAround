app.directive('encounterTable', ['encounterService', '$modal', '$timeout', 'store', function (encounterService, $modal, $timeout, store) {
    return {
        restrict: "AE",
        templateUrl: "Angular/Encounter/Directives/encounterTable.html",
        scope: {
            encounter: '=',
            save: '&'
        },
        link: function (scope, element, attr) {

            scope.adjustedDifficulty = "";
            scope.toggle = true; //Defaults visible
            scope.isSaved = true;

            var saveHandler = scope.save(); //scope.save()(encounter)

            scope.$on("saved", function() {
                scope.isSaved = true;
            });


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
                if (_.isUndefined(scope.encounter) || _.isNull(scope.encounter))
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
                scope.isSaved = false;
                scope.$emit("encounterChanged");
            };

            scope.removeMonster = function (monster) {
                if (monster.quantity > 1) {
                    monster.quantity = monster.quantity - 1;
                } else {
                    scope.encounter.monsters = _.filter(scope.encounter.monsters, function (m) {
                        return m.id !== monster.id;
                    });
                }

                if (_.size(scope.encounter.monsters) === 0) {
                    scope.encounter = null;
                } else {
                    updateEncounters();
                }
            }

            scope.addMonster = function (monster) {
                monster.quantity = monster.quantity + 1;
                updateEncounters();
            }

            //scope.loadEncounter = function () {
            //    var profile = store.get('profile');
            //    var params = { email: profile.email }

            //    encounterService.getSavedEncounters(function (encounter) {
            //        scope.encounter = encounter;
            //        scope.adjustedDifficulty = getDifficulty(encounter.difficulty);
            //    }, params);
            //}

            scope.saveEncounter = function (encounter) {
                saveHandler(encounter);
                scope.isSaved = true;


                //    //var params = {
                //    //    email: profile.email,
                //    //    encounters: savedEncounters
                //    //};
                //    //encounterService.saveEncounters(function (result) {
                //    //    console.log("Save Encounter response: " + result.statusText + " " + result.status);
                //    //}, params);
                //    console.log(savedEncounters);
                //}, { email: profile.email });

            }
        }
    }
}]);