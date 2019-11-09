using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
namespace Echo.Character
{
    class Creator : Script
    {
        // Устанавливаем пользователя в редактор персонажа.
        [RemoteEvent("charCreate")]
        static public void OnCharCreate(Client player)
        {
            player.Position = new Vector3(-811.6723f, 175.2313f, 76.74538f);
            player.Rotation = new Vector3(0.0f, 0.0f, 106.2622f);

            player.TriggerEvent("showCreatorPage");
        }
    }
}
