app.service('LoginService', ['auth', 'store', '$q', function (auth, store, $q) {

    var self = this;
    self.signIn = function () {
        var defer = $q.defer();
        if (auth.isAuthenticated) {
            defer.resolve();
        } else {
            auth.signin({}, function (profile, token) {
                store.set('profile', profile);
                store.set('token', token);
                defer.resolve();
            }, function (error) {
                console.log(error);
                defer.reject(error);
            });
        }
        return defer.promise;
    }

    self.signOut = function () {
        auth.signout();
        store.remove('profile');
        store.remove('token');
    }

    return self;
}]);
