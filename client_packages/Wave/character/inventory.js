mp.events.add('showPlayerInventory', (items) => {
    let name  = mp.players.local.getVariable('PLAYER_NAME');
    let money = mp.players.local.getVariable('PLAYER_MONEY');
    let bank  = mp.players.local.getVariable('PLAYER_BANK');
    
    mp.events.call('createBrowser', ['package://statics/html/inventory.html']);
    mp.events.call('executeFunction', ['SetInventoryInformation', name, money, bank]);
    mp.events.call('executeFunction', ['IntantiateCell', items]);
});
mp.events.add('inventoryItemMove', (from, to) => {
    mp.events.callRemote('ChangeItemPosition', from, to);
});