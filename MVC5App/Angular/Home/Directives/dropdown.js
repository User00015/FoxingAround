app.directive('dropdown', [ function ( ) {
    return {
        restrict: "E",
        templateUrl: "Angular/Home/Directives/dropdown.html",
        scope: {
            placeholder: "@",
            list: "=",
            selected: "=",
            property: "@"
        },
        link: function (scope) {
            scope.listVisible = false;
            scope.isPlaceholder = true;

            scope.select = function (item) {
                scope.isPlaceholder = false;
                scope.selected = item;
            };

            scope.isSelected = function (item) {
                return item[scope.property] === scope.selected[scope.property];
            };

            scope.show = function () {
                scope.listVisible = !scope.listVisible;
            };

            scope.$watch("selected", function (value) {
                scope.isPlaceholder = scope.selected[scope.property] === undefined;
                scope.display = scope.selected[scope.property];
            });
        }
    }
}]);