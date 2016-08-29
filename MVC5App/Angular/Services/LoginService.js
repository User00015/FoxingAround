app.service('LoginService', ['lock', 'store', '$q', 'envService', function (lock, store, $q, envService) {

    var self = this;

    self.signIn = function () {
        lock.show({
            callbackURL: envService.read('apiUrl'),
            responseType: 'code',
            authParams: {
                scope: 'openid offline_access'
            }
        });
    };
    //self.signIn = function () {
    //    var defer = $q.defer();
    //    if (lock.isAuthenticated) {
    //        defer.resolve();
    //    } else {
    //        lock.signin({}, function (profile, token) {
    //            store.set('profile', profile);
    //            store.set('token', token);
    //            defer.resolve();
    //        }, function (error) {
    //            console.log(error);
    //            defer.reject(error);
    //        });
    //    }
    //    return defer.promise;
    //}

    //self.signOut = function () {
    //    lock.signout();
    //    store.remove('profile');
    //    store.remove('token');
    //}
    self.signOut = function () {
        lock = null;
        store.remove('profile');
        store.remove('token');
    };

    self.isAuthenticated = function () {
        var p = store.get('profile');
        return p != null;
    }

    return self;
}]);
