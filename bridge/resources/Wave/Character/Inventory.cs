using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Wave.Model;
using Wave.Global;
namespace Wave.Character
{
    class Inventory : Script
    {
        private static List<InventoryModel> items;
        [RemoteEvent("LoadPlayerItems")]
        public void OnPlayerOpenIventory(Client player)
        {
            NAPI.Task.Run(() =>
            {
                items = Database.Database.LoadPlayerIventoryItems(player.GetData(EntityData.PLAYER_SQL_ID));
                string _items = NAPI.Util.ToJson(items);
                player.TriggerEvent("showPlayerInventory", _items);
            });
        }
        [RemoteEvent("ChangeItemPosition")]
        public void OnPlayerItemMove(Client player, string from, string to)
        {
            Console.WriteLine(from, to);
            NAPI.Task.Run(() =>
            {
                Database.Database.ChangeItemPosition(player.GetData(EntityData.PLAYER_SQL_ID), from, to);
            });  
        }
    }
}
