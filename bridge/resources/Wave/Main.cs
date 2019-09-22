using System;
using GTANetworkAPI;

namespace Wave
{
    public class Main : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void ResourceStart()
        {
            //NAPI.Server.SetAutoRespawnAfterDeath(false);
            //NAPI.Server.SetAutoSpawnOnConnect(false);
            //NAPI.Server.SetAutoRespawnAfterDeath(false);

            NAPI.Server.SetCommandErrorMessage("[~r~Ошибка~w~] Команда не найдена!");
        }
    }
}
