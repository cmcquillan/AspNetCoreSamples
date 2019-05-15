$(function () {
    $.get('/config.json', function (data) {
        window.apiUrl = data.api;

        var config = {
            authority: data.authority,
            loadUserInfo: true,
            scope: 'openid profile ' + data.scopes,
            redirect_uri: window.location.origin + '/',
            post_logout_redirect_uri: window.location.origin + '/',
            client_id: 'implicit-client',
            response_type: 'token id_token'
        };

        console.log('Auth Config', config);
        window.UM = new Oidc.UserManager(config);
    });
});

function startLogin() {
    window.UM.signinRedirect();
}

function startLogout() {
    window.UM.signoutRedirect();
}

function acquireUser() {
    window.UM.signinRedirectCallback()
        .then(function (user) {
            console.log('Auth Success', user);
        })
        .catch(function (error) {
            console.error(error);
        });
}

function callAPI() {
    var url = window.apiUrl + '/api/people/';

    window.UM.getUser()
        .then(function (user) {
            if (user) {
                $.get({
                    url: url,
                    headers: {
                        'Authorization': 'Bearer ' + user.access_token
                    },
                    success: function (data) {
                        $('#outputArea').text(JSON.stringify(data));
                    }
                });
            }
        });
}
