let customBrowser = undefined;
let parameters = [];

mp.events.add('createBrowser', (arguments) => {
	// Проверяем, открыт ли сейчас браузер
	if(customBrowser === undefined) {
		// Сохраняем аргументы
		parameters = arguments.slice(1, arguments.length);
		
		// Создаем окно браузера
		customBrowser = mp.browsers.new(arguments[0]);
	}
});

mp.events.add('browserDomReady', (browser) => {
	if(customBrowser === browser) {
		// Включаем курсор
		mp.gui.cursor.visible = true;
		
		if(parameters.length > 0) {
			// Вызываем функцию, заданную параметром
			mp.events.call('executeFunction', parameters);
		}
	}
});

mp.events.add('executeFunction', (arguments) => {
	// Проверяем параметры
	let input = '';
	
	for(let i = 1; i < arguments.length; i++) {
		if(input.length > 0) {
			input += ', \'' + arguments[i] + '\'';
		} else {
			input = '\'' + arguments[i] + '\'';
		}
	}
	
	// Вызываем функцию с параметром
	customBrowser.execute(`${arguments[0]}(${input});`);
});

mp.events.add('destroyBrowser', () => {
	// Выключаем курсор
	mp.gui.cursor.visible = false;
	
	// Удаляем браузер
	customBrowser.destroy();
	customBrowser = undefined;
});