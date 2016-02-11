import crossroads from "crossroads";
import hasher from "hasher";

function activateCrossroads() {
    function parseHash(newHash) { crossroads.parse(newHash); }
    crossroads.normalizeFn = crossroads.NORM_AS_OBJECT;
    hasher.initialized.add(parseHash);
    hasher.changed.add(parseHash);
    hasher.init();
}

function Router(config) {
    var currentRoute = this.currentRoute = ko.observable({});

    ko.utils.arrayForEach(config.routes, function(route) {
        crossroads.addRoute(route.url, function(requestParams) {
            currentRoute(ko.utils.extend(requestParams, route.params));
        });
    });

    activateCrossroads();
}

export default Router ;