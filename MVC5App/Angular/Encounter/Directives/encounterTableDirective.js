app.directive('encounterTable', [function () {
    return {
        restrict: "E",
        templateUrl: "Angular/Encounter/Directives/encounterTable.html",
        scope: {
            encounters: "="
        },
        link: function (scope) {

        }
    }
}]);