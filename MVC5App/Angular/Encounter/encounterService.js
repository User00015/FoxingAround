app.service('encounterService', ['$http', function($http) {

    var self = this;



    $http.get('http://localhost:60533/api/Monsters').then(function(response) {
        self.monsters = response.data;
        console.log(JSON.stringify(response.data, null, '\t'));
    }, function() {
        console.log('broke');
    });

    return self;
}])