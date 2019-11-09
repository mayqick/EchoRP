using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Echo.Model;
using Echo.Global;
namespace Echo.Character
{
    class Inventory : Script
    {
        private static List<InventoryModel> items;
        [RemoteEvent("LoadPlayerItems")]
        public void OnPlayerOpenIventory(Client player)
        {
            NAPI.Task.Run(() =>
            {
                items = Database.Database.LoadPlayerIventoryItems(player.GetData<int>(EntityData.PLAYER_SQL_ID));
                string _items = NAPI.Util.ToJson(items);
                player.TriggerEvent("showPlayerInventory", _items);
            });
        }
        [RemoteEvent("ChangeItemPosition")]
        public void OnPlayerItemMove(Client player, string from, string to)
        {
            NAPI.Util.ConsoleOutput(from, to);
            NAPI.Task.Run(() =>
            {
                Database.Database.ChangeItemPosition(player.GetData<int>(EntityData.PLAYER_SQL_ID), from, to);
            });  
        }
    }
}
