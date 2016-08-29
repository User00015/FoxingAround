var app = angular.module('FifthEditionEncounters', ['ngRoute', 'ngAnimate', 'ngResource', 'ngMap', 'environment', 'wt.responsive', 'mgcrea.ngStrap', 'auth0.lock', 'angular-storage', 'angular-jwt']);


app
    .constant('_', window._)
    .config([
        '$routeProvider', '$locationProvider', 'envServiceProvider', 'lockProvider', 'jwtInterceptorProvider', '$httpProvider',
        function ($routeProvider, $locationProvider, envService, lockProvider, jwtInterceptorProvider, $httpProvider) {
            $routeProvider
                .when('/home', { templateUrl: './Angular/Home/home.html', controller: 'HomeController' })
                //.when('/gallery', { templateUrl: './Angular/Gallery/gallery.html', controller: 'GalleryController' })
                .when('/encounter', { templateUrl: './Angular/Encounter/encounter.html', controller: 'EncounterController' })
                //.when('/login', { templateUrl: './Angular/Login/login.html', controller: 'LoginController' }) //Maybe later
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
            //var lock = new Auth0Lock(
            //    'eYDiisAw4OLNYJwybpX1sLuUmPuyaJ91',
            //    'foxing-around.auth0.com'
            //);


            lockProvider.init({
                domain: 'foxing-around.auth0.com',
                clientID: 'eYDiisAw4OLNYJwybpX1sLuUmPuyaJ91',
                callbackURL: '/login'
            });


            jwtInterceptorProvider.tokenGetter = function (store) {
                return store.get('token');
            };

            $httpProvider.interceptors.push('jwtInterceptor');
        }
    ])

    .run(['$rootScope', 'store', 'jwtHelper', 'lock', function ($rootScope, store, jwtHelper, lock) {
        angular.element(document).on("click", function (e) {
            $rootScope.$broadcast("documentClicked", angular.element(e.target));
        });
        $rootScope._ = window._;

        //$rootScope.$on('$locationChangeStart', function () {
        //    if (!lock.isAuthenticated) {
        //        var token = store.get('token');
        //        if (token) {
        //            if (!jwtHelper.isTokenExpired(token)) {
        //                lock.authenticate(store.get('profile'), token);
        //            }
        //        }
        //    }
        //});

        lock.on('authenticated', function (authResult) {
            console.log(authResult);
                    lock.getProfile(authResult.idToken, function (error, profile) {
                        if (error) {
                            console.log("Error: Login failed");
                        } else {
                            store.set('profile', profile);
                            store.set('token', authResult.idToken);
                        };

                    });
                    //profilePromise.then(function (profile) {
                    //    store.set('profile', profile);
                    //    store.set('token', idToken);
                    //});

                    //$location.path('/'); //TODO - Log in page maybe? If so, redirect here.
                });
    }])

    .controller('RootController', ['$scope', '$route', '$routeParams', '$location',
            function ($scope, $route, $routeParams, $location) {

            }]);