using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core;
using Newtonsoft.Json;
using System.Dynamic;

namespace Echo_ClientSide
{
    public class Main : BaseScript
    {

        public Main()
        {
            Tick += OnTick;

            EventHandlers["onResourceStart"] += new Action<string>(OnResourceStart);
            EventHandlers["onResourceStop"] += new Action<string>(OnResourceStop);
            EventHandlers["playerSpawned"] += new Action(OnPlayerSpawned);

            RegisterCommand("cords", new Action<int>((source) =>
            {
                
                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 255, 0, 0 },
                    args = new[] { "[Координаты]", $"{GetEntityCoords(GetPlayerPed(-1), true)} {GetEntityHeading(GetPlayerPed(-1))}" } //152.4227 -1001.112 -99
                });
            }), false);

           
        }
        public enum GamePhase { WAITING, READY, STARTING, STARTED, RESET, DEAD };
        public static GamePhase currentPhase = GamePhase.WAITING;
        private bool firstTick = true; // Первый тик для проверки на присоединение.

        private void OnPlayerSpawned()
        {
            /*          Game.Player.CanControlCharacter = false;
                      Game.PlayerPed.IsVisible = false;
                      Game.PlayerPed.IsInvincible = true;
                      Game.PlayerPed.CanRagdoll = false;

                      API.NetworkSetEntityInvisibleToNetwork(Game.PlayerPed.Handle, true);*/


            TriggerServerEvent("onPlayerSpawned");
        }
        private void OnResourceStop(string resourceName)
        {
            if (API.GetCurrentResourceName() != resourceName) return;
            API.NetworkSessionKickPlayer(Game.Player.Handle);
        }
        private void OnResourceStart(string resourceName)
        {
            if (API.GetCurrentResourceName() != resourceName) return;
        }
        private async Task OnTick()
        {
            await Delay(50);
            // Если это первый тик, то триггерим ивент OnPlayerConnected
            if (firstTick)
            {
                firstTick = false;
                AddEventOnPlayerConnected();
            }
            BeginTextCommandDisplayHelp("STRING");
            AddTextComponentString(GetEntityCoords(GetPlayerPed(-1), true).ToString());
            AddTextComponentString2(GetEntityHeading(GetPlayerPed(-1)).ToString());
            DisplayHelpTextFromStringLabel(0, false, true, -1);

        }
        private static void AddEventOnPlayerConnected()
        {
            TriggerServerEvent("onPlayerConnected");
            TriggerEvent("onPlayerConnected");
        }
    }
}
