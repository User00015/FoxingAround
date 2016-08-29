app.controller('NavbarController', ['$scope', 'lock', '$location', 'store', 'LoginService', function ($scope, lock, $location, store, loginService) {
    $scope.isActive = function (viewLocation) {
        return (viewLocation === $location.path());
    };

    $scope.signIn = function() {
        loginService.signIn();
    }

    $scope.isLoggedIn = function() {
        return loginService.isAuthenticated();
    }

    $scope.signOut = function () {
        loginService.signOut();
    }
}])