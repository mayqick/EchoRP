mp.events.add('getToken', () => {
	var token = mp.storage.data.auth;
	mp.events.callRemote('setTokenData', token);
});
mp.events.add('setToken', (authToken) => {
	mp.storage.data.auth = authToken;
	mp.events.call('executeFunction', ['app.renderType(3)']);
});
mp.events.add('showPlayerRegister', () => {
	mp.events.call('createBrowser', ['package://statics/html/auth/index.html']);
	mp.events.call('executeFunction', ['app.renderType(1)']);
});
mp.events.add('mailVerification', (mail) => {
	mp.events.callRemote('mailVerification', mail);
});
mp.events.add('checkCode', (code) => {
	var authCode = mp.players.local.getVariable('AUTH_CODE').ToString();
	mp.events.call('executeFunction', ["console.log('"+authCode+"');"]);
	mp.events.call('executeFunction', ["console.log('"+code+"');"]);
	if (code == authCode){
		mp.events.callRemote('registerPlayerAccount');
	}
	else
	{
		mp.events.call('executeFunction', ["app.showError(2)"]);
	}
});