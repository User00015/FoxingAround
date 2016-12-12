app.service('LoginService', [ '$q', 'authService', '$rootScope', function (  $q, authService, $rootScope) {

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
