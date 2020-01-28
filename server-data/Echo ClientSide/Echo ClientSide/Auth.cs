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
            EventHandlers["changePlayerFreemode"] += new Action<IDictionary<string, object>>(OnPlayerChangeFreemodeGender);
            EventHandlers["onSavePlayerCharacter"] += new Action<IDictionary<string, object>>(OnSavePlayerCharacter);
            EventHandlers["onPlayerFinishedCharacterCustomizing"] += new Action(OnPlayerFinishedCharacterCustomizing);
            EventHandlers["onUpdateCusomizationCamSettings"] += new Action<IDictionary<string, object>>(OnUpdateCusomizationCamSettings);
            EventHandlers["setPlayerRegisterMailCode"] += new Action<string>(SetPlayerRegisterMailCode);
        }
        private int customizationCameraHandle;
        private string registerCode;

        private async void SetPlayerRegisterMailCode(string code)
        {
            await Delay(0);
            registerCode = code;
            Debug.WriteLine(code);
        }
        private async void OnUpdateCusomizationCamSettings(IDictionary<string, object> data)
        {
            await Delay(0);
            data.TryGetValue("height", out var height);
            data.TryGetValue("range", out var range);
/*            data.TryGetValue("rotate", out var rotate);*/
            SetCamCoord(customizationCameraHandle, 152.3708f, -1001.75f + Convert.ToSingle(range), -98.45f + Convert.ToSingle(height));
/*            SetCamRot(customizationCameraHandle, -20.0f, 0.0f, 0.0f + Convert.ToSingle(rotate), 1);*/
        }
        private async void OnPlayerFinishedCharacterCustomizing()
        {
            await Delay(0);
            Exports["cef_creator"].focusCreatorCef(false);
            Exports["cef_creator"].renderCreatorCef(false);
            /* SetEntityCoordsNoOffset(PlayerPedId(), -802.311f, 175.056f, 72.8446f, false, false, false); // Вроде бы в этом нет необходимости*/
            /* NetworkResurrectLocalPlayer(-802.311f, 175.056f, 72.8446f, 180.3265f, true, false);*/

            // Возвращаем контроль персонажа
            FreezeEntityPosition(PlayerPedId(), false);
            EnableAllControlActions(0);
            Game.Player.CanControlCharacter = true;
            RenderScriptCams(false, false, 0, true, true);
            DisplayRadar(true);
        }
        // Ивент вызывается при сохранении персонажа
        private async void OnSavePlayerCharacter(IDictionary<string, object> data)
        {
            // TODO: СДЕЛАТЬ ПРОВЕРКУ НА НАЛИЧИЕ ПОВТОРЕНИЯ ЛОГИНА В БД!!!!
            await Delay(0);
            // На всякий пожарный еще раз обновляем внешний вид
            OnCharacterCreatorChangeSettings(data);

            // TODO: ВСЮ ЭТУ ХРЕНЬ НУЖНО БУДЕТ ПЕРЕСМОТРЕТЬ В БУДУЩЕМ
            data.TryGetValue("name", out var name);
            data.TryGetValue("surname", out var surname);
            data.TryGetValue("gender", out var gender);
            data.TryGetValue("firstHeadShape", out var firstHeadShape);
            data.TryGetValue("secondHeadShape", out var secondHeadShape);
            data.TryGetValue("shapeMix", out var shapeMix);
            data.TryGetValue("blemishesModel", out var blemishesModel);
            data.TryGetValue("ageingModel", out var ageingModel);
            data.TryGetValue("ageingOpacity", out var ageingOpacity);
            data.TryGetValue("frecklesModel", out var frecklesModel);
            data.TryGetValue("currentEyeColor", out var currentEyeColor);
            data.TryGetValue("currentHairColor", out var currentHairColor);
            data.TryGetValue("currentBeardColor", out var currentBeardColor);
            data.TryGetValue("currentSkinColor", out var currentSkinColor);
            data.TryGetValue("hairStyle", out var hairStyle);
            data.TryGetValue("beardModel", out var beardModel);
            data.TryGetValue("beardOpacity", out var beardOpacity);
            data.TryGetValue("browHeight", out var browHeight);
            data.TryGetValue("browWidth", out var browWidth);

            data.TryGetValue("browModel", out var browModel);
            data.TryGetValue("browOpacity", out var browOpacity);

            data.TryGetValue("noseWidth", out var noseWidth);
            data.TryGetValue("noseHeight", out var noseHeight);
            data.TryGetValue("noseLength", out var noseLength);
            data.TryGetValue("noseTip", out var noseTip);
            data.TryGetValue("noseBridge", out var noseBridge);
            data.TryGetValue("noseShift", out var noseShift);
            data.TryGetValue("lips", out var lips);
            data.TryGetValue("cheekboneHeight", out var cheekboneHeight);
            data.TryGetValue("cheekboneWidth", out var cheekboneWidth);
            data.TryGetValue("jawHeight", out var jawHeight);
            data.TryGetValue("jawWidth", out var jawWidth);
            data.TryGetValue("chinLength", out var chinLength);
            data.TryGetValue("chinPosition", out var chinPosition);
            data.TryGetValue("chinWidth", out var chinWidth);
            data.TryGetValue("chinShape", out var chinShape);
            data.TryGetValue("neckWidth", out var neckWidth);

            Models.SkinModel skinModel = new Models.SkinModel();
            Models.CharacterModel characterModel = new Models.CharacterModel();

            characterModel.characterName = Convert.ToString(name) + "_" + Convert.ToString(surname);
            characterModel.isMale = Convert.ToString(gender) == "male" ? true : false;

            skinModel.firstHeadShape = Convert.ToInt16(firstHeadShape);
            skinModel.secondHeadShape = Convert.ToInt16(secondHeadShape);

            skinModel.headMix = Convert.ToSingle(shapeMix);
            skinModel.blemishesModel = Convert.ToInt16(blemishesModel);
            skinModel.ageingModel = Convert.ToInt16(ageingModel);
            skinModel.ageingOpacity = Convert.ToInt16(ageingOpacity);
            skinModel.frecklesModel = Convert.ToInt16(frecklesModel);
            skinModel.eyesColor = Convert.ToInt16(currentEyeColor);
            skinModel.firstSkinTone = Convert.ToInt16(currentSkinColor);
            skinModel.secondSkinTone = Convert.ToInt16(currentSkinColor);
            skinModel.beardColor = Convert.ToInt16(currentBeardColor);
            skinModel.firstHairColor = Convert.ToInt16(currentHairColor);
            skinModel.secondHairColor = Convert.ToInt16(currentHairColor);
            skinModel.hairModel = Convert.ToInt16(hairStyle);
            skinModel.beardModel = Convert.ToInt16(beardModel);
            skinModel.beardOpacity = Convert.ToInt16(beardOpacity);

            skinModel.browHeight = Convert.ToSingle(browHeight);
            skinModel.browWidth = Convert.ToSingle(browWidth);

            skinModel.eyebrowsModel = Convert.ToInt16(browModel);
            skinModel.eyebrowsOpacity = Convert.ToInt16(browOpacity);

            skinModel.noseBridge = Convert.ToSingle(noseBridge);
            skinModel.noseHeight = Convert.ToSingle(noseHeight);
            skinModel.noseLength = Convert.ToSingle(noseLength);
            skinModel.noseShift = Convert.ToSingle(noseShift);
            skinModel.noseWidth = Convert.ToSingle(noseWidth);
            skinModel.noseTip = Convert.ToSingle(noseTip);

            skinModel.lips = Convert.ToSingle(lips);
            skinModel.cheekboneHeight = Convert.ToSingle(cheekboneHeight);
            skinModel.cheekboneWidth = Convert.ToSingle(cheekboneWidth);
            skinModel.jawHeight = Convert.ToSingle(jawHeight);
            skinModel.jawWidth = Convert.ToSingle(jawWidth);
            skinModel.chinPosition = Convert.ToSingle(chinPosition);
            skinModel.chinLength = Convert.ToSingle(chinLength);
            skinModel.chinShape = Convert.ToSingle(chinShape);
            skinModel.neckWidth = Convert.ToSingle(neckWidth);

            // преобразуем в JSON наши данные чтобы потом на стороне сервера преобразовать их к классовым типам. Гениально 
            string skin = JsonConvert.SerializeObject(skinModel);
            string characrer = JsonConvert.SerializeObject(characterModel);
            // Ивент сохранения скина и создания персонажа

            TriggerServerEvent("onPlayerSaveCharacterInformation", skin, characrer);

        }




        private async void OnPlayerCharacterCreating() // Отправка игрока на кастомизацю
        {
            ChangePlayerFreemode(true);
            SetEntityHealth(PlayerPedId(), 200);
            /*SetEntityCoordsNoOffset(PlayerPedId(), 152.3851f, -1000.384f, -99f, false, false, false);  // Вроде бы в этом нет необходимости*/

            NetworkResurrectLocalPlayer(152.3851f, -1000.384f, -100f, 180.3265f, true, false);

            customizationCameraHandle = CreateCam("DEFAULT_SCRIPTED_CAMERA", true);
            SetCamCoord(customizationCameraHandle, 152.3708f, -1001.75f, -98.45f);
            SetCamRot(customizationCameraHandle, -20.0f, 0.0f, 0.0f, 1);
            RenderScriptCams(true, false, 0, true, true);

            DisplayRadar(false);
            ClearPedTasksImmediately(PlayerPedId());
            SetEntityHealth(PlayerPedId(), 300);
            RemoveAllPedWeapons(PlayerPedId(), true);
            ClearPlayerWantedLevel(PlayerId());

            // Возможно не нужно. Дебильная синяя футболка
            SetPedDefaultComponentVariation(Game.PlayerPed.Handle);
            ClearAllPedProps(Game.PlayerPed.Handle);
            ClearPedDecorations(Game.PlayerPed.Handle);
            ClearPedFacialDecorations(Game.PlayerPed.Handle);


            FreezeEntityPosition(PlayerPedId(), true);
            ShutdownLoadingScreen();
            DisableAllControlActions(0);
            Game.Player.CanControlCharacter = false;
            // скрытие страницы ввода mail
            Exports["cef_auth"].focusAuthCef(false);
            Exports["cef_auth"].renderAuthCef(false);

            // фокус на странице кастомизации
            Exports["cef_creator"].focusCreatorCef(true);
            Exports["cef_creator"].renderCreatorCef(true);

            await Delay(1000);
            DoScreenFadeIn(1000);
            DisableAllControlActions(0);
        }
        // Вызывается при любой смене параметров перса на кастомизации
        private async void OnCharacterCreatorChangeSettings(IDictionary<string, object> data) 
        {
            await Delay(0);

            // TODO: ТУТ НЕ ВСЕ НАСТРОЙКИ ПЕРСОНАЖА ИЛИ НЕКОТОРЫЕ ЗАДЕЙСТВОВАНЫ НЕ ПОЛНОСТЬЮ
            // TODO: ВСЮ ЭТУ ХРЕНЬ НУЖНО БУДЕТ ПЕРЕСМОТРЕТЬ В БУДУЩЕМ
            data.TryGetValue("firstHeadShape", out var firstHeadShape);
            data.TryGetValue("secondHeadShape", out var secondHeadShape);
            data.TryGetValue("shapeMix", out var shapeMix);
            data.TryGetValue("blemishesModel", out var blemishesModel);
            data.TryGetValue("ageingModel", out var ageingModel);
            data.TryGetValue("ageingOpacity", out var ageingOpacity);
            data.TryGetValue("frecklesModel", out var frecklesModel);
            data.TryGetValue("currentEyeColor", out var currentEyeColor);
            data.TryGetValue("currentHairColor", out var currentHairColor);
            data.TryGetValue("currentBeardColor", out var currentBeardColor);
            data.TryGetValue("currentSkinColor", out var currentSkinColor);
            data.TryGetValue("hairStyle", out var hairStyle);
            data.TryGetValue("beardModel", out var beardModel);
            data.TryGetValue("beardOpacity", out var beardOpacity);
            data.TryGetValue("browHeight", out var browHeight);
            data.TryGetValue("browWidth", out var browWidth);
            data.TryGetValue("browModel", out var browModel);
            data.TryGetValue("browOpacity", out var browOpacity);
            data.TryGetValue("noseWidth", out var noseWidth);
            data.TryGetValue("noseHeight", out var noseHeight);
            data.TryGetValue("noseLength", out var noseLength);
            data.TryGetValue("noseTip", out var noseTip);
            data.TryGetValue("noseBridge", out var noseBridge);
            data.TryGetValue("noseShift", out var noseShift);
            data.TryGetValue("lips", out var lips);
            data.TryGetValue("cheekboneHeight", out var cheekboneHeight);
            data.TryGetValue("cheekboneWidth", out var cheekboneWidth);
            data.TryGetValue("jawHeight", out var jawHeight);
            data.TryGetValue("jawWidth", out var jawWidth);
            data.TryGetValue("chinLength", out var chinLength);
            data.TryGetValue("chinPosition", out var chinPosition);
            data.TryGetValue("chinWidth", out var chinWidth);
            data.TryGetValue("chinShape", out var chinShape);
            data.TryGetValue("neckWidth", out var neckWidth);

            SetPedHeadBlendData(Game.PlayerPed.Handle, (int)firstHeadShape, (int)secondHeadShape, (int)firstHeadShape, Convert.ToInt16(currentSkinColor), Convert.ToInt16(currentSkinColor), Convert.ToInt16(currentSkinColor), Convert.ToSingle(shapeMix), 0.5f, 0f, false);
            SetPedEyeColor(Game.PlayerPed.Handle, (int)currentEyeColor);
            if (Convert.ToInt16(blemishesModel) == 0) SetPedHeadOverlay(Game.PlayerPed.Handle, 0, Convert.ToInt16(blemishesModel), 0.0f);
            else SetPedHeadOverlay(Game.PlayerPed.Handle, 0, Convert.ToInt16(blemishesModel), 1.0f);
            if (Convert.ToInt16(frecklesModel) == 0) SetPedHeadOverlay(Game.PlayerPed.Handle, 9, Convert.ToInt16(blemishesModel), 0.0f);
            else SetPedHeadOverlay(Game.PlayerPed.Handle, 9, Convert.ToInt16(frecklesModel), 1.0f);


            SetPedHeadOverlay(Game.PlayerPed.Handle, 3, Convert.ToInt16(ageingModel), Convert.ToInt16(ageingOpacity) * 0.1f);
            SetPedComponentVariation(Game.PlayerPed.Handle, 2, Convert.ToInt16(hairStyle), 0, 2);
            SetPedHairColor(Game.PlayerPed.Handle, Convert.ToInt16(currentHairColor), Convert.ToInt16(currentHairColor));

            SetPedHeadOverlay(Game.PlayerPed.Handle, 2, Convert.ToInt16(browModel), Convert.ToInt16(browOpacity) * 0.1f);

            SetPedHeadOverlay(Game.PlayerPed.Handle, 1, Convert.ToInt16(beardModel), Convert.ToInt16(beardOpacity) * 0.1f);
            SetPedHeadOverlayColor(Game.PlayerPed.Handle, 1, 1, Convert.ToInt16(currentBeardColor), Convert.ToInt16(currentBeardColor));
            SetPedHeadOverlayColor(Game.PlayerPed.Handle, 2, 1, Convert.ToInt16(currentBeardColor), Convert.ToInt16(currentBeardColor));

            SetPedFaceFeature(Game.PlayerPed.Handle, 0, Convert.ToSingle(noseWidth));
            SetPedFaceFeature(Game.PlayerPed.Handle, 1, Convert.ToSingle(noseHeight));
            SetPedFaceFeature(Game.PlayerPed.Handle, 2, Convert.ToSingle(noseLength));
            SetPedFaceFeature(Game.PlayerPed.Handle, 3, Convert.ToSingle(noseBridge));
            SetPedFaceFeature(Game.PlayerPed.Handle, 4, Convert.ToSingle(noseTip));
            SetPedFaceFeature(Game.PlayerPed.Handle, 5, Convert.ToSingle(noseShift));
            SetPedFaceFeature(Game.PlayerPed.Handle, 6, Convert.ToSingle(browHeight));
            SetPedFaceFeature(Game.PlayerPed.Handle, 7, Convert.ToSingle(browWidth));
            SetPedFaceFeature(Game.PlayerPed.Handle, 8, Convert.ToSingle(cheekboneHeight));
            SetPedFaceFeature(Game.PlayerPed.Handle, 9, Convert.ToSingle(cheekboneWidth));
            // Maybe todo:
        /*    SetPedFaceFeature(Game.PlayerPed.Handle, 10, Convert.ToSingle());
            SetPedFaceFeature(Game.PlayerPed.Handle, 11, Convert.ToSingle(noseWidth));
*/
            SetPedFaceFeature(Game.PlayerPed.Handle, 12, Convert.ToSingle(lips));
            SetPedFaceFeature(Game.PlayerPed.Handle, 13, Convert.ToSingle(jawWidth));
            SetPedFaceFeature(Game.PlayerPed.Handle, 14, Convert.ToSingle(jawHeight));

            SetPedFaceFeature(Game.PlayerPed.Handle, 15, Convert.ToSingle(chinLength));
            SetPedFaceFeature(Game.PlayerPed.Handle, 16, Convert.ToSingle(chinPosition));
            SetPedFaceFeature(Game.PlayerPed.Handle, 17, Convert.ToSingle(chinWidth));
            SetPedFaceFeature(Game.PlayerPed.Handle, 18, Convert.ToSingle(chinShape));
            SetPedFaceFeature(Game.PlayerPed.Handle, 19, Convert.ToSingle(neckWidth));



        }
        // Показ окна регистрации (ввод mail)
        private async void OnPlayerStartRegistation()
        {
            await Delay(0);
            // Включение курсора и фокус на окне регистрации
            Exports["cef_auth"].focusAuthCef(true);

            // Показ окна регистрации
            Exports["cef_auth"].renderAuthCef(true);

        }
        // Ивент для NUI Callback, который принимает значения пола из JSON и передает в метод
        private static async void OnPlayerChangeFreemodeGender(IDictionary<string, object> data)
        {
            await Delay(0);
            data.TryGetValue("isMale", out var isMale);
            ChangePlayerFreemode((bool)isMale);
        }
        // Меняем пол freemode модели персонажа с ожиданием загрузки
        private static async void ChangePlayerFreemode(bool isMale, bool firstJoin = false)
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
                /*API.SetModelAsNoLongerNeeded(model);*/

                // Делаем проверку ибо при отправке на кастомизацю мы ужесбрасываем компоненты педа
                if (firstJoin) SetPedDefaultComponentVariation(API.PlayerPedId()); 
                SetPedHeadBlendData(Game.PlayerPed.Handle, 0, 0, 0, 0, 0, 0, 0.5f, 0.5f, 0f, false);
            }
        }
    }
}
