var userManager = new Oidc.UserManager({

});

userManager.signoutRedirectCallback().then(function (resp) {
    if (resp) {
        console.log('Signout Callback', resp);
    }
}).catch(function (error) {
    console.error(error);
});