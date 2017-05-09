app.controller('TrackerController', ['$scope', '$location', function ($scope, $location) {

    $scope.encounter = [];

    $scope.addCombatant = function () {
        $scope.encounter.push({
            init: $scope.newCombatantInit,
            name: $scope.newCombatantName,
            hp: $scope.newCombatantHp
        });

        $scope.newCombatantInit = null;
        $scope.newCombatantName = null;
        $scope.newCombatantHp = null;

    }

    $scope.addHp = function (combatant) {
        combatant.hp += 1;
    }

    $scope.subtractHp = function (combatant) {
        combatant.hp -= 1;
    }
}])