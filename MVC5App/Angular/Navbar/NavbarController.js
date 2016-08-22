app.controller('NavbarController', ['$scope', 'auth', '$location', 'store', function ($scope, auth, $location, store) {
    $scope.isActive = function (viewLocation) {
        return (viewLocation === $location.path());
    };

    $scope.signIn = function () {
        auth.signin({}, function (profile, token) {
            store.set('profile', profile);
            store.set('token', token);
        }, function (error) {
            console.log(error);
        });
    }

    $scope.isLoggedIn = function() {
        return auth.isAuthenticated;
    }

    $scope.signOut = function () {
        auth.signout();
        store.remove('profile');
        store.remove('token');
    }
}])