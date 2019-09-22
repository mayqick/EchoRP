mp.events.add('createPlayerAccount', (login, password, promo) => {
	// Вызываем серверный Remote Event для создания аккаунта.
	mp.events.callRemote('createAccount', login, password, promo);
});
mp.events.add('regError', () => {
	mp.events.call('executeFunction', ['registerError']);
});