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
            EventHandlers["onCharacterCreatorChangeSettings"] += new Action<IDictionary<string, object>>(OnCharacterCreatorChangeSettings);
        }

        // Получение mail из окна регистрации

        private async void OnPlayerCharacterCreating()
        {
            ChangePlayerFreemode(true);
            SetEntityHealth(PlayerPedId(), 200);
            /*            SetEntityCoordsNoOffset(PlayerPedId(), 152.3851f, -1000.384f, -99f, false, false, false);*/

            NetworkResurrectLocalPlayer(152.3851f, -1000.384f, -100f, 180.3265f, true, false);

            var spawnedCamera = CreateCam("DEFAULT_SCRIPTED_CAMERA", true);
            SetCamCoord(spawnedCamera, 152.3708f, -1001.75f, -98.45f);
            SetCamRot(spawnedCamera, -20.0f, 0.0f, 0.0f, 1);
            RenderScriptCams(true, false, 0, true, true);

            ClearPedTasksImmediately(PlayerPedId());
            SetEntityHealth(PlayerPedId(), 300);
            RemoveAllPedWeapons(PlayerPedId(), true);
            ClearPlayerWantedLevel(PlayerId());

            SetPedDefaultComponentVariation(Game.PlayerPed.Handle);
            ClearAllPedProps(Game.PlayerPed.Handle);
            ClearPedDecorations(Game.PlayerPed.Handle);
            ClearPedFacialDecorations(Game.PlayerPed.Handle);


            FreezeEntityPosition(PlayerPedId(), true);
            ShutdownLoadingScreen();
            Game.Player.CanControlCharacter = false;

            Exports["cef_creator"].focusCreatorCef();
            /*         Exports["cef_creator"].renderCreatorCef();*/

            await Delay(1000);
            DoScreenFadeIn(1000);
            EnableAllControlActions(0);
        }
        private async void OnCharacterCreatorChangeSettings(IDictionary<string, object> data)
        {
            data.TryGetValue("firstHeadShape", out var firstHeadShape);
            data.TryGetValue("secondHeadShape", out var secondHeadShape);
            SetPedHeadBlendData(Game.PlayerPed.Handle, (int)firstHeadShape, (int)secondHeadShape, 0, 0, 0, 0, 0.5f, 0.5f, 0f, false);
            /*TriggerServerEvent("test", itemIdObj);*/
        }
        private async void OnPlayerStartRegistation()
        {
            await Delay(0);
            // Включение курсора и фокус на окне регистрации
            Exports["cef_auth"].focusAuthCef();

            // Показ окна регистрации
            Exports["cef_auth"].renderAuthCef();

        }
        private static async void ChangePlayerFreemode(bool isMale)
        {
            await Delay(0);
            uint model = isMale ? (uint)API.GetHashKey("mp_m_freemode_01") : (uint)API.GetHashKey("mp_f_freemode_01");
            API.RequestModel(model);
            if (API.IsModelInCdimage(model))
            {
                while (!API.HasModelLoaded(model))
                {
                    await Delay(0);
                }
                API.SetPlayerModel(API.PlayerId(), model);
                API.SetModelAsNoLongerNeeded(model);
                API.SetPedDefaultComponentVariation(API.PlayerPedId());
            }
        }
    }
}
