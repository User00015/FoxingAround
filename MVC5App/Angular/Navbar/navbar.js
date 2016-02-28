app.directive('navbar', function () {
    return {
        restrict: 'E',
        templateUrl: 'Angular/Navbar/navbar.html',
        replace: true,
        controller: 'NavbarController'
    }
});