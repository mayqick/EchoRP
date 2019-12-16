mp.events.add('keyListener', () => {
    let chatIsOpen = false;
    let inventoryIsOpen = false;
    mp.keys.bind(0x49, true, function() { // I
        if (!inventoryIsOpen && !chatIsOpen && mp.players.local.getVariable('PLAYER_PLAYING')){
            mp.events.callRemote('LoadPlayerItems');
            inventoryIsOpen = true;
        }
        else{
            mp.events.call('destroyBrowser'); // закрытие инвентаря
            inventoryIsOpen = false;
        }
    });
    mp.keys.bind(0x54, true, function () { // T
        if (!chatIsOpen && mp.players.local.getVariable('PLAYER_PLAYING')) { // открытие свитчера чат мода
            mp.events.callRemote('ChatOpen');
            mp.events.call('createBrowser', ['package://statics/html/chat.html']);
            chatIsOpen = true;
        }
    });
    mp.keys.bind(0x26, true, function () { // T

        let phone = mp.browsers.new("package://statics/html/iphone/index.html");
        browser.execute('devicesApp.phone(1)');

    });
    mp.keys.bind(0x1B, true, function () { // Escape
        if (chatIsOpen || inventoryIsOpen) {  // Закрытие свитчера чат мода и инвентаря
            mp.events.call('destroyBrowser');
            chatIsOpen = false;
        }
    });
    mp.keys.bind(0x0D, true, function () { // Enter
        if (chatIsOpen) {
            mp.events.call('destroyBrowser');
            chatIsOpen = false;
        }
    });
    mp.keys.bind(0x09, true, function () { // Tab
        if (chatIsOpen) {
            mp.events.callRemote('ChatModeTabChanged');
        }
    });
});
mp.events.add('SetActiveChatMode', (chatMode) => {
    mp.events.call('executeFunction', ['SetActiveChatMode', chatMode]);
});
mp.events.add('ChatModeChanged', (chatMode) => {
    mp.events.callRemote('ChatModeChanged', chatMode);
});
mp.events.add('StaminaMod', (mode) => {
    mp.game.player.restoreStamina(mode == 0 ? 100 : 0);
});