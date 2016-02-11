"use strict";

function getUser(username) {
    let url = "api/github/"+ username;
    let options=  {
        method: "GET",
        headers: {
            "Accept" : "application/json",
            "Content-Type" : "application/json"
        }
    };

    function checkStatus(response) {
        if (response.status >= 200 && response.status <= 300) {
            return response.json();
        } else {
            var error = new Error(response.statusText);
            error.response = response;
            throw error;
        }
    }

    function handleError(error) {
        return Promise.reject(error);
    }

    return window.fetch(url, options)
        .then(checkStatus)
        .catch(handleError);;
}

export default {
    getUser : getUser
}