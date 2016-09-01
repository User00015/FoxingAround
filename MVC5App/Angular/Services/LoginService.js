app.service('LoginService', ['lock', 'store', '$q', 'envService', 'authService', '$rootScope', function (lock, store, $q, envService, authService, $rootScope) {

    var self = this;

    self.signIn = function () {
        var defer = $q.defer();
        if ($rootScope.isAuthenticated) {
            defer.resolve();
        } else {
            authService.login();
        };
        return defer.promise;
    }

    self.signOut = function () {
        authService.logout();
    };

    return self;
}]);
