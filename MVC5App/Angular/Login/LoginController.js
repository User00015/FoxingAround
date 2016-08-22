app.controller('LoginController', ['$scope', 'auth', '$location', 'store', function ($scope, auth, $location, store) {

    $scope.signIn = function () {
        auth.signin({}, function (profile, token) {
            store.set('profile', profile);
            store.set('token', token);
            $location.path("/home");
        }, function (error) {
            console.log(error);
        });
    }

    $scope.signOut = function() {
        auth.signout();
        store.remove('profile');
        store.remove('token');
        $location.path('/login');
    }
}]);