app.service('encounterService', ['$http', 'envService', function($http, envService) {

    var self = this;

    var monstersUrl = envService.read('apiUrl') + '/api/Monsters';
    var emptyMonstersUrl = envService.read('apiUrl') + '/api/Monsters/Empty';
    var detailsUrl = envService.read('apiUrl') + '/api/Monsters/';
    var updateUrl = envService.read('apiUrl') + '/api/Monsters/ExperienceValues';
    var loadEncountersUrl = envService.read('apiUrl') + '/api/Monsters/LoadEncounters';
    var saveEncountersUrl = envService.read('apiUrl') + '/api/Monsters/SaveEncounters';

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

    self.randomizeMonsterEncounter = function(callback, params) {
        $http.post(monstersUrl, params).then(function(response) {
            self.encounter = response.data;
            callback(response.data);
        });
    }

    self.emptyMonsterEncounter = function(callback, params) {
        $http.post(emptyMonstersUrl, params).then(function(response) {
            self.encounter = response.data;
            callback(response.data);
        });
    }


    self.getSavedEncounters = function(callback) {
        $http.get(loadEncountersUrl).then(function(response) {
            callback(response.data);
        });
    }

    self.saveEncounters = function(encounters) {
        $http.post(saveEncountersUrl, encounters);
    }

    return self;
}])