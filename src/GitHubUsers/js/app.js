import Router from "js/router";

// Global variables
import "babel-polyfill";
import "fetch";
import "jquery";
import "bootstrap";
import "knockout";
import "knockout-validation";

// Components
import navBarComponent from "js/components/nav-bar/nav-bar";
import useraSearchComponent from "js/components/user-search/user-search";
import aboutTemplate from "js/components/about/about.html!text";


// Register components
ko.components.register("nav-bar", navBarComponent);
ko.components.register("user-search", useraSearchComponent);
ko.components.register("about", { template: aboutTemplate });

// init ko validation
ko.validation.init({
    registerExtenders: true,
    parseInputAttributes: false,
    errorElementClass: "has-error",
    errorMessageClass: "help-block"
});

var router = new Router({
    routes: [
        { url: "", params: { page: "user-search" } },
        { url: "user/{username}", params: { page: "user-search" } },
        { url: "about", params: { page: "about" } }
    ]
});

ko.applyBindings({ route: router.currentRoute });
