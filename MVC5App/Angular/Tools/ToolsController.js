app.controller('ToolsController', ['$scope', '$location', function ($scope, $location) {
    var getrandom = function (arr) {
        return arr[ _.random(0, arr.length - 1)];
    }
    $scope.npc = {
        'characteristic': {},
        'ideals': {},
        'bonds': {},
        'flaws': {},
        'races': {},
        'fullname': {},
        'npc': {},
    };
    
    var char = [
        "Absentminded",
        "Arrogant",
        "Boorish",
        "Chews on something",
        "Clumsy",
        "Curious",
        "Dim-witted",
        "Fiddles and fidgets nervously",
        "Frequently uses the wrong word",
        "Friendly",
        "Irritable",
        "Prone to predictions of certain doom",
        "Pronounced Scar",
        "Slurs words, lisps, or stutters",
        "Speaks loudly or whispers",
        "Squints",
        "Stares off into distance",
        "Suspicious",
        "Uses colorful oaths and explanations",
        "Uses hippy speak or words",
    ];
    var ideals = [
        "Aspiration (any)",
        "Charity(Good)",
        "Community(Lawful)",
        "Creativity (Chaotic)",
        "Discovery (any)",
        "Fairness (Lawful)",
        "Freedom (Chaotic)",
        "Glory (any)",
        "Greater good (Good)",
        "Greed ( Evil)",
        "Honor (Lawful)",
        "Independence (Chaotic)",
        "Knowledge (Neutral)",
        "Life (Good)",
        "Live and let live (Neutral)",
        "Might (Evil)",
        "Nation (any)",
        "People (Neutral)",
        "Power (Evil)",
        "Redemption (any)",
    ];
    var bonds = [
        "Personal Goal or Achievement",
        "Family Members",
        "Colleagues or Compatriots",
        "Benefactor, patron, or employer",
        "Romantic interest",
        "Special place",
        "Keepsake",
        "Valuable possession",
        "Revenge",
    ];
    var flaws = [
        "Susceptible to Forbidden Love or Romance",
        "Indulges in excessive pleasure or luxury",
        "Arrogance",
        "Extreme Envy of someone's possessions or their title/station",
        "Overpowering greed",
        "Prone to rage",
        "Has a powerful enemy",
        "Has a specific phobia",
        "Shameful or scandalous history",
        "Committed secret crime or misdeed",
        "Possesses forbidden lore",
        "Is foolhardy brave",
    ];
    var race = [
        "Human",
        "Elf",
        "Dwarf",
        "Halfling",
        "Gnome",
        "Half-Orc",
        "Half-Elf",
        "Dragonborn",
        "Tiefling",
        "Goblin",
    ];
    var name1 = [
        "",
        "",
        "",
        "",
        "A",
        "Be",
        "De",
        "El",
        "Fa",
        "Jo",
        "Ki",
        "La",
        "Ma",
        "Na",
        "O",
        "Pa",
        "Re",
        "Si",
        "Ta",
        "Va",
    ];
    var name2 = [
        "bar",
        "ched",
        "dell",
        "far",
        "gran",
        "hal",
        "jen",
        "kel",
        "lim",
        "mor",
        "net",
        "penn",
        "quil",
        "rond",
        "sark",
        "shen",
        "tur",
        "vash",
        "yor",
        "zen",
    ];
    var name3 = [
        "",
        "a",
        "ac",
        "ai",
        "al",
        "am",
        "an",
        "ar",
        "ea",
        "el",
        "er",
        "ess",
        "ett",
        "ic",
        "id",
        "il",
        "in",
        "is",
        "or",
        "us",
    ];
    var full = 
        getrandom(name1) + getrandom(name2) + getrandom(name3)
    ;
    $scope.npc.characteristic = getrandom(char);
    $scope.npc.ideals = getrandom(ideals);
    $scope.npc.bonds = getrandom(bonds);
    $scope.npc.flaws = getrandom(flaws);
    $scope.npc.races = getrandom(race);
    $scope.npc.fullname = full;

}])