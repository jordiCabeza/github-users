"use strict";

import template from "js/components/user-search/user-search.html!text";
import githubService from "js/services/github/github-service";

export function UserSearchViewModel(params) {
    var self = this;

    self.username = ko.observable("");

    self.user = ko.observable(null);    
    self.error = ko.observable(null);    

    self.search = function() {
        self.error(null);
        self.user(null);
        return githubService.getUser(self.username())
        .then(function(user) {
            self.user(user);
            return user;
        })
        .catch(function(error) {
            self.error(error);
            return Promise.reject(error);
        });        
    };    
}

export default { viewModel: UserSearchViewModel, template: template };