app.service('encounterService', ['$http', 'envService', function($http, envService) {

    var self = this;

    var url = envService.read('apiUrl') + '/api/Monsters';

    self.getMonsters = function(callback) {
        $http.get(url).then(function(response) {
            self.monsters = response.data;
            callback(response.data);
        });
    }

    self.postMonsters = function(params) {
        $http.post(url, params).then(function() {
            console.log(params);
            //self.foo = data;
            //callback(data);
        });
    }

    return self;
}])