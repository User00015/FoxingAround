app.controller('HomeController', ['$scope', 'NgMap', function ($scope, NgMap) {

    //NgMap.getMap().then(function(map) {
    //    console.log(map.getCenter());
    //    console.log('markers', map.markers);
    //    console.log('shapes', map.shapes);
    //});

    $scope.carriers = [
        {
            name: "Alltel",
            gateway:"sms.alltelwireless.com"
        },
        {
            name: "AT&T",
            gateway:"txt.att.net"
        },
        {
            name: "Boost Mobile",
            gateway:"sms.myboostmobile.com"
        },
        {
            name: "Project Fi",
            gateway:"msg.fi.google.com"
        },
        {
            name: "Sprint",
            gateway:"messaging.sprintpcs.com"
        },
        {
            name: "U.S. Cellular",
            gateway:"email.uscc.net"
        },
        {
            name: "Verizon Wireless",
            gateway:"vtext.com"
        },
        {
            name: "Virgin Mobile",
            gateway:"vmobl.com"
        },
        {
            name: "T-Mobile",
            gateway:"tmomail.net"
        }
    ];

    $scope.carrier = '';
}]);