"use strict";

import githubService from "js/services/github/github-service";

describe("js/services/github/github-service.spec", function () {
    it("- Should get user by username ", function(done) {
        // arrange        
        var username = "username";
        var expectedUser = { name: "name", username: username };

        spyOn(window, "fetch").and.returnValue(Promise.resolve({ status: 200, json: function() { return expectedUser; } }));

        // act
        githubService.getUser(username).then((user) => {
            // assert
            expect(user).toBe(expectedUser);

            var options = { method: "GET", headers: { "Accept": "application/json", "Content-Type": "application/json" } };
            expect(window.fetch).toHaveBeenCalledWith("api/github/" + username, options);
            done();
        });
    });
});