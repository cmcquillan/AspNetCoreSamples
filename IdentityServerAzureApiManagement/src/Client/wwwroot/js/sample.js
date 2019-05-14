$(function () {
    var config = {
        authority: 'https://localhost:5001/',
        client_id: 'implicit-client',
        redirect_uri: 'https://localhost:5021/',
        response_type: 'token id_token',
        scope: 'openid profile',
        loadUserInfo: true
    };

    window.UM = window.UM || new Oidc.UserManager(config);

    if (window.location.hash && window.location.hash.indexOf('token') !== -1) {
        um.signinRedirectCallback().then(function (user) {
            if (user) {
                console.log('Signin', user);
            }
        }).catch(function (error) {
            console.error(error);
        });
    } else {
        var user = um.getUser().then(function (user) {
            if (user) {
                console.log('Success:', user);
            } else {
                um.signinRedirect({ state: 'some-data' });
            }
        }).catch(function (error) {
            console.error(error);
        });
    }
});

function startLogin() {
    UM.signinRedirect({ state: 'some-data' });
}
