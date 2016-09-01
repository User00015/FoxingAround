app.service('authService', [
    '$rootScope', 'lock', 'authManager',  function($rootScope, lock, authManager) {

        var self = this;

        self.login = function () {
            lock.show();
        }

        $rootScope.userProfile = JSON.parse(localStorage.getItem('profile')) || {};

        // Logging out just requires removing the user's
        // id_token and profile
        self.logout = function () {
            localStorage.removeItem('id_token');
            localStorage.removeItem('profile');
            authManager.unauthenticate();
            self.userProfile = {};
        }

        // Set up the logic for when a user authenticates
        // This method is called from app.run.js
        self.registerAuthenticationListener = function() {
            lock.on('authenticated', function(authResult) {
                localStorage.setItem('id_token', authResult.idToken);
                authManager.authenticate();

                lock.getProfile(authResult.idToken, function(error, profile) {
                    if (error) {
                        console.log(error);
                    }

                    localStorage.setItem('profile', JSON.stringify(profile));
                    //$rootScope.$broadcast('userProfileSet', profile);
                });
            });
        }

        return self;
    }
]);