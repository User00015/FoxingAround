var app = angular.module('FifthEditionEncounters', ['ngRoute', 'ngAnimate', 'ngResource', 'ngMap', 'environment', 'wt.responsive', 'mgcrea.ngStrap', 'auth0', 'angular-storage', 'angular-jwt']);


app
    .constant('_', window._)
    .config([
        '$routeProvider', '$locationProvider', 'envServiceProvider', 'authProvider', 'jwtInterceptorProvider', '$httpProvider',
        function ($routeProvider, $locationProvider, envService, authProvider, jwtInterceptorProvider, $httpProvider) {
            $routeProvider
                .when('/home', { templateUrl: './Angular/Home/home.html', controller: 'HomeController' })
                //.when('/gallery', { templateUrl: './Angular/Gallery/gallery.html', controller: 'GalleryController' })
                .when('/encounter', { templateUrl: './Angular/Encounter/encounter.html', controller: 'EncounterController' })
                .when('/login', { templateUrl: './Angular/Login/login.html', controller: 'LoginController' })
                .when('/about', { templateUrl: './Angular/About/about.html', controller: 'AboutController' })
                .otherwise({ redirectTo: '/home' });

            $locationProvider.html5Mode(true);

            envService.config({
                domains: {
                    development: ['localhost'],
                    production: ['foxing-around.com', 'www.foxing-around.com']
                },
                vars: {
                    development: {
                        apiUrl: 'http://localhost:60533'
                    },
                    production: {
                        apiUrl: 'http://www.foxing-around.com'
                    }
                }
            });
            envService.check();

            authProvider.init({
                domain: 'foxing-around.auth0.com',
                clientID: 'eYDiisAw4OLNYJwybpX1sLuUmPuyaJ91',
                loginUrl: '/login'
            });

            authProvider.on('loginSuccess', ['$location', 'profilePromise', 'idToken', 'store',
                    function ($location, profilePromise, idToken, store) {

                        profilePromise.then(function (profile) {
                            store.set('profile', profile);
                            store.set('token', idToken);
                        });

                        //$location.path('/'); //TODO - Log in page maybe? If so, redirect here.
                    }]);

            //Called when login fails
            authProvider.on('loginFailure', function () {
                console.log("Error: Login failed");
            });

            jwtInterceptorProvider.tokenGetter = function (store) {
                return store.get('token');
            };

            $httpProvider.interceptors.push('jwtInterceptor');
        }
    ])

    .run(['$rootScope', 'auth', 'store', 'jwtHelper',  function ($rootScope, auth, store, jwtHelper, authManager) {
        angular.element(document).on("click", function (e) {
            $rootScope.$broadcast("documentClicked", angular.element(e.target));
        });
        $rootScope._ = window._;

        $rootScope.$on('$locationChangeStart', function () {
            if (!auth.isAuthenticated) {
                var token = store.get('token');
                if (token) {
                    if (!jwtHelper.isTokenExpired(token)) {
                        auth.authenticate(store.get('profile'), token);
                    }
                }
            }
        });
    }])

    .controller('RootController', ['$scope', '$route', '$routeParams', '$location',
            function ($scope, $route, $routeParams, $location) {

            }]);