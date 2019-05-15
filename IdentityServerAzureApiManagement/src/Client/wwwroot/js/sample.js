function __appendToOutput(...items) {
    for (var i = 0; i < items.length; i++) {
        $('#outputArea').append(JSON.stringify(items[i]) + ' ');
    }
    $('#outputArea').append('\r\n\r\n');
}

function log(...items) {
    console.log(...items);
    __appendToOutput(...items);
}

function logError(...items) {
    items.unshift('Error:');
    log(...items);
}

$(function () {
    $.get('/config.json', function (data) {
        window.apiUrl = data.api;
        window.gatewayUrl = data.gateway;

        var config = {
            authority: data.authority,
            loadUserInfo: true,
            scope: 'openid profile ' + data.scopes,
            redirect_uri: window.location.origin + '/',
            post_logout_redirect_uri: window.location.origin + '/',
            client_id: 'implicit-client',
            response_type: 'token id_token'
        };

        log('Auth Config', config);
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
            log('Auth Success', user);
        })
        .catch(function (error) {
            logError(error);
        });
}

function __callAPI(url) {
    window.UM.getUser()
        .then(function (user) {
            var headers = {};
            if (user) {
                headers["Authorization"] = 'Bearer ' + user.access_token;
            }

            $.get({
                url: url,
                headers: headers,
                success: function (data) {
                    log(data);
                },
                error: function (error) {
                    logError(JSON.stringify(error));
                }
            });
        });
}

function callAPI() {
    var url = window.apiUrl + '/api/people/';
    __callAPI(url);
}

function callAPIGateway() {
    var url = window.gatewayUrl + '/api/people';
    __callAPI(url);
}