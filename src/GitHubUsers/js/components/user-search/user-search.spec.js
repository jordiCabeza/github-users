import "babel-polyfill";
import userSearchComponent from "js/components/user-search/user-search";
import githubService from "js/services/github/github-service";

describe("js/components/dates-diff/dates-diff.spec", function () {
    describe("Init component", function(){
        it("- Should create calculatorViewModel", () => {
            // act
            var viewModel = new userSearchComponent.viewModel();

            // assert
            expect(viewModel.username()).toBe("");
            expect(viewModel.user()).toBe(null);
            expect(viewModel.error()).toBe(null);
        });
    });


    describe("Search user", function(){
        it("- Should search user", (done) => {
            // arrange
            var username = "username";            
            var viewModel = new userSearchComponent.viewModel();
            viewModel.username(username);

            var expectedUser = { username: "username" };
            spyOn(githubService, "getUser").and.returnValue(Promise.resolve(expectedUser));
            
            // act
            viewModel.search().then((user) =>
            {
                // assert                
                expect(user).toBe(expectedUser);  
                expect(viewModel.user()).toBe(user);
                expect(viewModel.error()).toBe(null);
                expect(githubService.getUser).toHaveBeenCalledWith(username);
                done();
            });           
        });

         it("- Should search user show error when service fails", (done) => {
            // arrange
            var username = "username";            
            var viewModel = new userSearchComponent.viewModel();
            viewModel.username(username);

            spyOn(githubService, "getUser").and.returnValue(Promise.reject({ somerError : "someValue"}));
            
            // act
            viewModel.search().catch((error) =>
            {
                // assert                
                expect(viewModel.user()).toBe(null);
                expect(viewModel.error()).toBe(error);
                expect(githubService.getUser).toHaveBeenCalledWith(username);
                done();
            });           
        });
    });
});