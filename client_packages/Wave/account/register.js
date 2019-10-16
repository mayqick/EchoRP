mp.events.add('createPlayerAccount', (login, password, promo) => {
	// Вызываем серверный Remote Event для создания аккаунта.
	mp.events.callRemote('createAccount', login, password, promo);
});
mp.events.add('getToken', () => {
	var token = mp.storage.data.auth;
	mp.events.callRemote('setTokenData', token);
});
mp.events.add('setToken', (authToken) => {
	mp.storage.data.auth = authToken;
});
mp.events.add('regError', () => {
	mp.events.call('executeFunction', ['registerError']);
});