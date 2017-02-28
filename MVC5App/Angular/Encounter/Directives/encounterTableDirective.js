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

            scope.$on("saved", function () {
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

            scope.maximumDifficulty = function() {
                if (_.isNil(scope.encounter) || _.isNil(scope.$parent) || _.isNil(scope.$parent.difficulty)) return 0;

                return _.nth(_.values(scope.encounter.difficulty), scope.$parent.difficulty.value);
            }

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
                scope.$emit("encountersChanged");
            };

            scope.$on("updateEncounter", function () {
                if (_.isUndefined(scope.encounter) || _.isNull(scope.encounter))
                    return;
                updateEncounters();
            });

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

            scope.saveEncounter = function (encounter) {
                saveHandler(encounter);
                scope.isSaved = true;
            }
        }
    }
}]);