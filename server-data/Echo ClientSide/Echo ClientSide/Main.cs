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

            RegisterCommand("setcampos", new Action<int>((source) =>
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

        private async void OnPlayerSpawned()
        {
            await Delay(0);
            /*          Game.Player.CanControlCharacter = false;
                      Game.PlayerPed.IsVisible = false;
                      Game.PlayerPed.IsInvincible = true;
                      Game.PlayerPed.CanRagdoll = false;

                      API.NetworkSetEntityInvisibleToNetwork(Game.PlayerPed.Handle, true);*/


            TriggerServerEvent("onPlayerSpawned");

        }
        private async void OnResourceStop(string resourceName)
        {
            await Delay(0);
            if (API.GetCurrentResourceName() != resourceName) return;
            API.NetworkSessionKickPlayer(Game.Player.Handle);
        }
        private async void OnResourceStart(string resourceName)
        {
            await Delay(0);
            if (API.GetCurrentResourceName() != resourceName) return;


            Debug.WriteLine($"The resource {resourceName} has been started.");

 
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
        }
        private static async void AddEventOnPlayerConnected()
        {
            await Delay(0);
            TriggerServerEvent("onPlayerConnected");
            TriggerEvent("onPlayerConnected");

        }
    }
}
