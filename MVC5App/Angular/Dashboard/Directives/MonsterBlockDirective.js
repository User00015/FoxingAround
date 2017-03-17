app.directive('monsterBlock', [function () {

    return {
        restrict: "AE",
        templateUrl: "Angular/Dashboard/Directives/MonsterCard.html",
        scope: {
            monsters: '='
        },
        link: function (scope) {
            console.log(scope.monsters);
        }
    }
}]);