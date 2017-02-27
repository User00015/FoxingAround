_.mixin({
    'replace': function (collection, identity, replacement) {
        var index = _.indexOf(collection, _.find(collection, identity));
        collection.splice(index, 1, replacement);
    }
});

_.mixin({
    'isNil': function(obj) {
        return _.isUndefined(obj) || _.isNull(obj);
    }
});

