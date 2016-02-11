"use strict";

import template from "js/components/nav-bar/nav-bar.html!text";

export function NavBarViewModel(params) {
    var self = this;

    self.route = params.route;
}

export default { viewModel: NavBarViewModel, template: template };