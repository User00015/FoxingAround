app.controller('NavbarController', ['$scope', '$location', 'LoginService', function ($scope, $location, loginService) {
    $scope.isActive = function (viewLocation) {
        return (viewLocation === $location.path());
    };

    $scope.signIn = function () {
        loginService.signIn();
    }

    $scope.isLoggedIn = function () {
        return loginService.isAuthenticated();
    }

    $scope.signOut = function () {
        loginService.signOut();
    }
}])