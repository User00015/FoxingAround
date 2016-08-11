app.service('encounterService', ['$http', 'envService', function($http, envService) {

    var self = this;

    var monstersUrl = envService.read('apiUrl') + '/api/Monsters';
    var detailsUrl = envService.read('apiUrl') + '/api/Monsters/';
    var updateUrl = envService.read('apiUrl') + '/api/Monsters/ExperienceValues';
    var savedEncounterUrl = envService.read('apiUrl') + '/api/Monsters/SavedEncounter';

    self.getMonsters = function(callback) {
        $http.get(monstersUrl).then(function(response) {
            callback(response.data);
        });
    }

    self.updateEncounters = function(callback, monstersList) {
        $http.post(updateUrl, monstersList).then(function(response) {
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

    self.getSavedEncounter = function(callback) {
        $http.get(savedEncounterUrl).then(function(response) {
            callback(response.data);
        });
    }

    return self;
}])