var app = angular.module('FifthEditionEncounters', ['ngRoute', 'ngResource', 'ngMap', 'environment', 'smart-table']);

app
    .config(['$routeProvider', '$locationProvider','envServiceProvider',
        function ($routeProvider, $locationProvider, envService) {
            $routeProvider
                .when('/home', { templateUrl: './Angular/Home/home.html', controller: 'HomeController' })
                //.when('/gallery', { templateUrl: './Angular/Gallery/gallery.html', controller: 'GalleryController' })
                .when('/encounter', { templateUrl: './Angular/Encounter/encounter.html', controller: 'EncounterController' })
                .when('/about', { templateUrl: './Angular/about/about.html', controller: 'AboutController' })
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
        }])

    .run(function ($rootScope) {
        angular.element(document).on("click", function (e) {
            $rootScope.$broadcast("documentClicked", angular.element(e.target));
        });

    })
        .controller('RootController', ['$scope', '$route', '$routeParams', '$location',
            function ($scope, $route, $routeParams, $location) {

            }]);