app.controller('LoginController', ['$scope', 'auth', '$location', 'store', function ($scope, auth, $location, store) {

    //$scope.signIn = function () {
    //    auth.signin({}, function (profile, token) {
    //        store.set('profile', profile);
    //        store.set('token', token);
    //        $location.path("/home");
    //    }, function (error) {
    //        console.log(error);
    //    });
    //}

    //$scope.signOut = function() {
    //    auth.signout();
    //    store.remove('profile');
    //    store.remove('token');
    //    $location.path('/login');
    //}

    $scope.user = '';
    $scope.pass = '';

    function onLoginSuccess(profile, token) {
        $scope.message.text = '';
        store.set('profile', profile);
        store.set('token', token);
        $location.path('/');
        $scope.loading = false;
    }

    function onLoginFailed() {
        $scope.message.text = 'invalid credentials';
        $scope.loading = false;
    }

    $scope.reset = function () {
        auth.reset({
            email: 'hello@bye.com',
            password: 'hello',
            connection: 'Username-Password-Authentication'
        });
    };

    $scope.submit = function () {
        $scope.message.text = 'loading...';
        $scope.loading = true;
        auth.signin({
            connection: 'Username-Password-Authentication',
            username: $scope.user,
            password: $scope.pass,
            authParams: {
                scope: 'openid name email'
            }
        }, onLoginSuccess, onLoginFailed);

    };

    $scope.doGoogleAuthWithPopup = function () {
        $scope.message.text = 'loading...';
        $scope.loading = true;

        auth.signin({
            popup: true,
            connection: 'google-oauth2',
            scope: 'openid name email'
        }, onLoginSuccess, onLoginFailed);
    };
}]);