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

            lockProvider.init({
                domain: 'foxing-around.auth0.com',
                clientID: 'eYDiisAw4OLNYJwybpX1sLuUmPuyaJ91'
            });


            jwtInterceptorProvider.tokenGetter = function () {
                return localStorage.getItem('id_token');
            };

            $httpProvider.interceptors.push('jwtInterceptor');
        }
    ])

    .run(['$rootScope',  'jwtHelper', 'lock', 'authService', 'authManager', function ($rootScope, jwtHelper, lock, authService, authManager) {
        angular.element(document).on("click", function (e) {
            $rootScope.$broadcast("documentClicked", angular.element(e.target));
        });
        $rootScope._ = window._;

        var token = localStorage.getItem('id_token');
        if (token != undefined) authManager.authenticate(); 

        authService.registerAuthenticationListener();

        authManager.checkAuthOnRefresh();
    }])

    .controller('RootController', ['$scope', '$route', '$routeParams', '$location',
            function ($scope, $route, $routeParams, $location) {

            }]);