mp.events.add('showAuthPage', (playerName) => {
	mp.events.call('createBrowser', ['package://statics/html/auth.html']);
});
mp.events.add('showError', () => {
	// Вызываем функцию показа ошибки
	mp.events.call('executeFunction', ['showError']);
});
mp.events.add('loadPlayerAccount', (login, password) => {
	setTimeout(function() {
		mp.events.callRemote('loginAccount', login, password);
	}, 100);
});
mp.events.add('showPlayerCharacters', (characters, slot_3, slot_4, donate, playerName, charactersInfo) => {
	mp.events.call('createBrowser', ['package://statics/html/chars.html']);
	mp.events.call('executeFunction', ['showChars', characters, slot_3, slot_4, donate, playerName, charactersInfo]);
});
mp.events.add('characterSelected', (char_name) => {
	mp.events.call('destroyBrowser');
	mp.events.callRemote('charSelected', char_name);
});

