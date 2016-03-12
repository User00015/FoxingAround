﻿var app = angular.module('weddingApp', ['ngRoute', 'ngResource', 'ngMap']);

app
    .config(['$routeProvider', '$locationProvider',
        function ($routeProvider, $locationProvider) {
            $routeProvider
                .when('/home', { templateUrl: './Angular/Home/home.html', controller: 'HomeController' })
                .when('/gallery', { templateUrl: './Angular/Gallery/gallery.html', controller: 'GalleryController' })
                .when('/ordering', { templateUrl: './Angular/Ordering/ordering.html', controller: 'OrderingController' })
                .when('/contacts', { templateUrl: './Angular/Contacts/contacts.html', controller: 'ContactsController' })
                .otherwise({ redirectTo: '/home' });

            $locationProvider.html5Mode(true);
        }])

    .run(function ($rootScope) {
        angular.element(document).on("click", function (e) {
            $rootScope.$broadcast("documentClicked", angular.element(e.target));
        });
    })
        .controller('RootController', ['$scope', '$route', '$routeParams', '$location',
        function ($scope, $route, $routeParams, $location) {
        }]);