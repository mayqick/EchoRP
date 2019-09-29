mp.events.add('createPlayerAccount', (login, password, promo) => {
	// Вызываем серверный Remote Event для создания аккаунта.
	mp.events.callRemote('createAccount', login, password, promo);
});
mp.events.add('getToken', () => {
	var token = mp.storage.data.auth;
	console.log(token);
	mp.events.callRemote('setTokenData', token);
});
mp.events.add('setToken', (authToken) => {
	mp.storage.data.auth = authToken;
	mp.storage.flush();
});
mp.events.add('regError', () => {
	mp.events.call('executeFunction', ['registerError']);
});