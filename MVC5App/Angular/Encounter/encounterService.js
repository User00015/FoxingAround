app.service('encounterService', ['$http', 'envService', function($http, envService) {

    var self = this;

    var monstersUrl = envService.read('apiUrl') + '/api/Monsters';
    var detailsUrl = envService.read('apiUrl') + '/api/Monsters/';

    self.getMonsters = function(callback) {
        $http.get(monstersUrl).then(function(response) {
            callback(response.data);
        });
    }

    self.getMonsterDetails = function(callback, monsterId) {
        $http.get(detailsUrl + monsterId).then(function(response) {
            callback(response.data);
        });
    }

    self.postMonsters = function(callback, params) {
        $http.post(monstersUrl, params).then(function(response) {
            self.encounter = response.data;
            callback(response.data);
        });
    }

    return self;
}])