app.directive('monsterBlock', ['encounterService', function (encounterService) {

    return {
        restrict: "AE",
        templateUrl: "Angular/Dashboard/Directives/MonsterCard.html",
        scope: {
            monsters: '='
        },
        link: function (scope) {
            scope.allMonsters = [];
            _.each(scope.monsters, function (monster) {
                encounterService.getMonsterDetails(function (data) {
                    _.push(scope.allMonsters, data);
                }, monster.id);
            });
        }
    }
}]);