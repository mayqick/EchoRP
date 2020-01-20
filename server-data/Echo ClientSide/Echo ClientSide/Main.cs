using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core.Native;
using CitizenFX.Core;
using Newtonsoft.Json;
using System.Dynamic;

namespace Echo_ClientSide
{
    public class Main : BaseScript
    {

        public Main()
        {

            EventHandlers["onResourceStart"] += new Action<string>(OnResourceStart);
            EventHandlers["playerSpawned"] += new Action(OnPlayerSpawned);
            EventHandlers.Add("onPlayerStartRegistation", new Action(OnPlayerStartRegistation));


            RegisterNUICallback("SendMailToRegistration", OnSendMailToRegistration);
        }
        private CallbackDelegate OnSendMailToRegistration(IDictionary<string, object> data, CallbackDelegate result)
        {

            if (data.TryGetValue("mail", out var mail))
            {
                result("ok");
                TriggerServerEvent("onPlayerRegistration",  mail.ToString());
            }
/*            TriggerServerEvent("onPlayerRegistration", mail.ToString());*/
            return result;
        }
        private async void OnPlayerStartRegistation()
        {
            await Delay(0);
            API.SetNuiFocus(true, true);

            API.SendNuiMessage(JsonConvert.SerializeObject(new
            {
                type = "render"
            }));

        }
        private async void OnPlayerSpawned()
        {
            await Delay(0);
            Game.Player.CanControlCharacter = false;
            Game.PlayerPed.IsVisible = false;
            Game.PlayerPed.IsInvincible = true;
            Game.PlayerPed.CanRagdoll = false;

            API.NetworkSetEntityInvisibleToNetwork(Game.PlayerPed.Handle, true);

            TriggerServerEvent("onPlayerSpawned");

        }
        private async void OnResourceStart(string resourceName)
        {
            await Delay(0);
            if (API.GetCurrentResourceName() != resourceName) return;

         
            Exports["spawnmanager"].setAutoSpawn(false);
            Debug.WriteLine($"The resource {resourceName} has been started.");

        }
        private void RegisterNUICallback(string msg, Func<IDictionary<string, object>, CallbackDelegate, CallbackDelegate> callback)
        {
            API.RegisterNuiCallbackType(msg);

            EventHandlers[$"__cfx_nui:{msg}"] += new Action<ExpandoObject, CallbackDelegate>((body, resultCallback) =>
            {
                CallbackDelegate err = callback.Invoke(body, resultCallback);
            });
        }
    }
}
