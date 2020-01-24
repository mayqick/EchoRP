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
    class Auth : BaseScript
    {
        public Auth()
        {
            EventHandlers.Add("onPlayerStartRegistation", new Action(OnPlayerStartRegistation));
            EventHandlers.Add("onPlayerCharacterCreating", new Action(OnPlayerCharacterCreating));
            RegisterNUICallback("SendMailToRegistration", OnSendMailToRegistration);
        }

        // Получение mail из окна регистрации
        private CallbackDelegate OnSendMailToRegistration(IDictionary<string, object> data, CallbackDelegate result)
        {
            if (data.TryGetValue("mail", out var mail))
            {
                result("ok");
                // отправка mail на сервер и начало регистрации
                TriggerServerEvent("onPlayerRegistration", mail.ToString());
            }
            return result;
        }
        private async void OnPlayerCharacterCreating()
        {
            SetEntityHealth(PlayerPedId(), 200);
            SetEntityCoordsNoOffset(PlayerPedId(), 0f, 0f, 500f, false, false, false);

            NetworkResurrectLocalPlayer(0f, 0f, 500f, 0f, true, false);
            ClearPedTasksImmediately(PlayerPedId());
            SetEntityHealth(PlayerPedId(), 300);
            RemoveAllPedWeapons(PlayerPedId(), true);
            ClearPlayerWantedLevel(PlayerId());
            SetEntityCoordsNoOffset(PlayerPedId(), 0f, 0f, 500f, false, false, false);

            FreezeEntityPosition(PlayerPedId(), true);
            ShutdownLoadingScreen();
            SetEntityVisible(PlayerPedId(), false, false);

            await Delay(1000);
            DoScreenFadeIn(1000);
            EnableAllControlActions(0);
        }
        private async void OnPlayerStartRegistation()
        {
            await Delay(0);
            // Включение курсора и фокус на окне регистрации
            API.SetNuiFocus(true, true);
            // Показ окна регистрации
            API.SendNuiMessage(JsonConvert.SerializeObject(new
            {
                type = "render"
            }));

        }
        public void RegisterNUICallback(string msg, Func<IDictionary<string, object>, CallbackDelegate, CallbackDelegate> callback)
        {
            RegisterNuiCallbackType(msg);

            EventHandlers[$"__cfx_nui:{msg}"] += new Action<ExpandoObject, CallbackDelegate>((body, resultCallback) =>
            {
                CallbackDelegate err = callback.Invoke(body, resultCallback);
            });
        }
    }
}
