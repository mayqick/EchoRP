using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Newtonsoft.Json;
using Echo.Model;
using Echo.Global;
using Echo.Account;
namespace Echo.Character
{
    class Customization : Script
    {
        [RemoteEvent("saveCustomization")]
        public void OnCloseCustomization(Client player, string playerJsonInfo, string name, string surname, int gender, int age, string clothes)
        {
            // Лист с будущим списком одежды.
            List<ClothesModel> clothesModels = new List<ClothesModel>();
            CharacterModel characterModel = new CharacterModel();
            // Внешний вид игрока.
            SkinModel skinModel = new SkinModel();
            // Получаем одежду из редактора.
            var obj_clothes = NAPI.Util.FromJson(clothes);

            for (int i = 0; i < obj_clothes.Count; i++)
            {
                ClothesModel clothes_model = new ClothesModel();
                switch (i)
                {
                    case 0:
                        clothes_model.slot = 11;
                        clothes_model.drawable = (int)obj_clothes[i];
                        clothes_model.texture = 0;
                        clothesModels.Add(clothes_model);
                        break;
                    case 1:
                        clothes_model.slot = 4;
                        clothes_model.drawable = (int)obj_clothes[i];
                        clothes_model.texture = 0;
                        clothesModels.Add(clothes_model);
                        break;
                    case 2:
                        clothes_model.slot = 6;
                        clothes_model.drawable = (int)obj_clothes[i];
                        clothes_model.texture = 0;
                        clothesModels.Add(clothes_model);
                        break;
                }
                
            }

            characterModel.characterName = name + "_" + surname;
            characterModel.sex = gender;
            characterModel.age = age;

            var data = NAPI.Util.FromJson(playerJsonInfo);

            skinModel.firstHeadShape = (int)data[0];
            skinModel.secondHeadShape = (int)data[1];
            skinModel.firstSkinTone = (int)data[2];
            skinModel.secondSkinTone = (int)data[3];

            skinModel.headMix = (float)data[4];
            skinModel.skinMix = (float)data[5];

            skinModel.hairModel = (int)data[6];
            skinModel.firstHairColor = (int)data[7];
            skinModel.secondHairColor = (int)data[8];
            skinModel.beardModel = (int)data[9];
            skinModel.beardColor = (int)data[10];
            skinModel.chestModel = (int)data[11];
            skinModel.chestColor = (int)data[12];
            skinModel.blemishesModel = (int)data[13];
            skinModel.ageingModel = (int)data[14];
            skinModel.complexionModel = (int)data[15];
            skinModel.sundamageModel = (int)data[16];
            skinModel.frecklesModel = (int)data[17];

            skinModel.eyesColor = (int)data[18];
            skinModel.eyebrowsModel = (int)data[19];
            skinModel.eyebrowsColor = (int)data[20];

            skinModel.makeupModel = (int)data[21];
            skinModel.blushModel = (int)data[22];
            skinModel.blushColor = (int)data[23];
            skinModel.lipstickModel = (int)data[24];
            skinModel.lipstickColor = (int)data[25];

            skinModel.noseWidth = (float)data[26];
            skinModel.noseHeight = (float)data[27];
            skinModel.noseLength = (float)data[28];
            skinModel.noseBridge = (float)data[29];
            skinModel.noseTip = (float)data[30];
            skinModel.noseShift = (float)data[31];
            skinModel.browHeight = (float)data[32];
            skinModel.browWidth = (float)data[33];
            skinModel.cheekboneHeight = (float)data[34];
            skinModel.cheekboneWidth = (float)data[35];
            skinModel.cheeksWidth = (float)data[36];
            skinModel.eyes = (float)data[37];
            skinModel.lips = (float)data[38];
            skinModel.jawWidth = (float)data[39];
            skinModel.jawHeight = (float)data[40];
            skinModel.chinLength = (float)data[41];
            skinModel.chinPosition = (float)data[42];
            skinModel.chinWidth = (float)data[43];
            skinModel.chinShape = (float)data[44];
            skinModel.neckWidth = (float)data[45];


            player.SetData(EntityData.PLAYER_SKIN_MODEL, skinModel);
            ApplyPlayerCustomization(player, skinModel, gender);


            NAPI.Task.Run(() =>
            {
                // Создаем персонажа и сохраняем его данные: внешний вид, одежда, информация о персонаже.
                int playerId = Database.Database.CreateCharacter(player, characterModel, skinModel, clothesModels);

            });
            
            Auth.OnCharSelected(player, characterModel.characterName);

        }
        [RemoteEvent("setGender")]
        public void OnGenderSelect(Client player, int args)
        {
            if (!player.HasSharedData(EntityData.PLAYER_PLAYING)){
                var gender = (int)args;
                // Устанавливаем пол игроку.
                player.SetSkin(gender == 1 ? PedHash.FreemodeMale01 : PedHash.FreemodeFemale01);
            }
            else NAPI.Chat.SendChatMessageToPlayer(player, Global.Constants.COLOR_ERROR + "Произошла ошибка безопасности!");
        }
        // Установка персонажа в редактор
        [RemoteEvent("setCharacterIntoCreator")]
        public void SetCharacterIntoCreatorEvent(Client player)
        {
            // Change player's skin
            NAPI.Player.SetPlayerSkin(player, PedHash.FreemodeMale01);

            // Remove clothes
            player.SetClothes(11, 15, 0);
            player.SetClothes(3, 15, 0);
            player.SetClothes(8, 15, 0);

            // Set player's position
            player.Transparency = 255;
            player.Rotation = new Vector3(0.0f, 0.0f, 180.0f);
            player.Position = new Vector3(152.3787f, -1000.644f, -99f);
        }
        // Установка одежды игрока в редакторе персонажа.
        [RemoteEvent("setClothes")]
        public void SetClothes(Client player, string args)
        {
            var obj = NAPI.Util.FromJson(args);
            player.SetClothes(11, (int)obj[0], 0);
            player.SetClothes(4, (int)obj[1], 0);
            player.SetClothes(6, (int)obj[2], 0);
        }
        public static void ApplyPlayerClothes(Client player, List <ClothesModel> clothesModel)
        {
            foreach (ClothesModel clothes in clothesModel)
            {
                player.SetClothes(clothes.slot, clothes.drawable, clothes.texture);
            }
        }
        public static void ApplyPlayerCustomization(Client player, SkinModel skinModel, int sex)
        {
            // Устанавливаем настройки головы
            HeadBlend headBlend = new HeadBlend();
            headBlend.ShapeFirst = Convert.ToByte(skinModel.firstHeadShape);
            headBlend.ShapeSecond = Convert.ToByte(skinModel.secondHeadShape);
            headBlend.SkinFirst = Convert.ToByte(skinModel.firstSkinTone);
            headBlend.SkinSecond = Convert.ToByte(skinModel.secondSkinTone);
            headBlend.ShapeMix = skinModel.headMix;
            headBlend.SkinMix = skinModel.skinMix;

            // Получаем волосы и цвет глаз
            byte eyeColor = Convert.ToByte(skinModel.eyesColor);
            byte hairColor = Convert.ToByte(skinModel.firstHairColor);
            byte hightlightColor = Convert.ToByte(skinModel.secondHairColor);

            // Устанавливаем черны лица
            float[] faceFeatures = new float[]
            {
                skinModel.noseWidth, skinModel.noseHeight, skinModel.noseLength, skinModel.noseBridge, skinModel.noseTip, skinModel.noseShift, skinModel.browHeight,
                skinModel.browWidth, skinModel.cheekboneHeight, skinModel.cheekboneWidth, skinModel.cheeksWidth, skinModel.eyes, skinModel.lips, skinModel.jawWidth,
                skinModel.jawHeight, skinModel.chinLength, skinModel.chinPosition, skinModel.chinWidth, skinModel.chinShape, skinModel.neckWidth
            };


            Dictionary<int, HeadOverlay> headOverlays = new Dictionary<int, HeadOverlay>();

            for (int i = 0; i < Constants.MAX_HEAD_OVERLAYS; i++)
            {
                int[] overlayData = GetOverlayData(skinModel, i);

                HeadOverlay headOverlay = new HeadOverlay();
                headOverlay.Index = Convert.ToByte(overlayData[0]);
                headOverlay.Color = Convert.ToByte(overlayData[1]);
                headOverlay.SecondaryColor = 0;
                headOverlay.Opacity = 1.0f;

                headOverlays[i] = headOverlay;
            }

            // Обновляем скин персонажа
            player.SetCustomization(sex == Constants.SEX_MALE, headBlend, eyeColor, hairColor, hightlightColor, faceFeatures, headOverlays, new Decoration[] { });
            player.SetClothes(2, skinModel.hairModel, 0);
        }

        private static int[] GetOverlayData(SkinModel skinModel, int index)
        {
            int[] overlayData = new int[2];

            switch (index)
            {
                case 0:
                    overlayData[0] = skinModel.blemishesModel;
                    overlayData[1] = 0;
                    break;
                case 1:
                    overlayData[0] = skinModel.beardModel;
                    overlayData[1] = skinModel.beardColor;
                    break;
                case 2:
                    overlayData[0] = skinModel.eyebrowsModel;
                    overlayData[1] = skinModel.eyebrowsColor;
                    break;
                case 3:
                    overlayData[0] = skinModel.ageingModel;
                    overlayData[1] = 0;
                    break;
                case 4:
                    overlayData[0] = skinModel.makeupModel;
                    overlayData[1] = 0;
                    break;
                case 5:
                    overlayData[0] = skinModel.blushModel;
                    overlayData[1] = skinModel.blushColor;
                    break;
                case 6:
                    overlayData[0] = skinModel.complexionModel;
                    overlayData[1] = 0;
                    break;
                case 7:
                    overlayData[0] = skinModel.sundamageModel;
                    overlayData[1] = 0;
                    break;
                case 8:
                    overlayData[0] = skinModel.lipstickModel;
                    overlayData[1] = skinModel.lipstickColor;
                    break;
                case 9:
                    overlayData[0] = skinModel.frecklesModel;
                    overlayData[1] = 0;
                    break;
                case 10:
                    overlayData[0] = skinModel.chestModel;
                    overlayData[1] = skinModel.chestColor;
                    break;
            }

            return overlayData;
        }

    }
}
