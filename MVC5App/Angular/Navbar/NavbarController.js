app.controller('NavbarController', ['$scope', 'auth', '$location', 'store', 'LoginService', function ($scope, auth, $location, store, loginService) {
    $scope.isActive = function (viewLocation) {
        return (viewLocation === $location.path());
    };

    $scope.signIn = function() {
        loginService.signIn();
    }

    $scope.isLoggedIn = function() {
        return auth.isAuthenticated;
    }

    $scope.signOut = function () {
        loginService.signOut();
    }
}])