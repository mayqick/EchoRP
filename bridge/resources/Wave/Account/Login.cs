using System;
using System.Collections.Generic;
using System.Text;
using Wave.Character;
using GTANetworkAPI;
using Wave.Model;
using Wave.Global;
namespace Wave.Account
{
    class Login : Script
    {
        private void InitializePlayerData(Client player)
        {
            player.Health = 100;
            player.Armor = 0;

            // Очищаем оружие
            player.RemoveAllWeapons();

            // Общие данные
            player.SetSharedData(EntityData.PLAYER_MONEY, 0);
            player.SetSharedData(EntityData.PLAYER_BANK, 200);

            player.SetData(EntityData.PLAYER_NAME, string.Empty);

            player.SetData(EntityData.PLAYER_ADMIN_RANK, 0);
            player.SetData(EntityData.PLAYER_HEALTH, 100);
            player.SetData(EntityData.PLAYER_ARMOR, 0);
        }

        private static void LoadCharacterData(Client player, CharacterModel character)
        {
            player.SetData(EntityData.PLAYER_SQL_ID, character.id);                 // sql ид персонажа

            player.SetSharedData(EntityData.PLAYER_NAME, character.characterName);  // ник персонажа
            player.SetData(EntityData.PLAYER_HEALTH, character.health);             // хп
            player.SetData(EntityData.PLAYER_ARMOR, character.armor);               // броня

            player.SetSharedData(EntityData.PLAYER_MONEY, character.money);         // деньги
            player.SetSharedData(EntityData.PLAYER_BANK, character.bank);           // банковские деньги

            player.SetSharedData(EntityData.PLAYER_HUNGER, character.hunger);       // голод
            player.SetSharedData(EntityData.PLAYER_THIRST, character.thirst);       // жажда

            player.SetData(EntityData.PLAYER_PLAYED, character.played);             // отыгранные минуты
            player.SetData(EntityData.PLAYER_XP, character.xp);                     // XP
            player.SetData(EntityData.PLAYER_LVL, character.lvl);                   // LVL    

            player.SetData(EntityData.PLAYER_ADMIN_RANK, character.adminRank);      // уровень админки

            player.SetSharedData(EntityData.PLAYER_PLAYING, true);                  // если персонаж на сервере
            player.SetData(EntityData.CHAT_MODE, "Say");                            // текущий чат мод

            player.Position = new Vector3(character.posX, character.posY, character.posZ);
            NAPI.Player.SetPlayerHealth(player, character.armor);
            NAPI.Player.SetPlayerArmor(player, character.health);

        }

        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnected(Client player)
        {
            //player.Dimension = (uint)(player.Value + 1000);
            player.SetSkin(PedHash.FreemodeMale01);

            // Тут должна быть инициализация данных игрока.
            InitializePlayerData(player);

            player.SetData("AuthCount", (int)0); // Устанавливаем счетчик попыток входа в сис-му на 0.
            player.TriggerEvent("showAuthPage");

        }

        [RemoteEvent("loginAccount")]
        public void LoginToAccount(Client player, string login, string password)
        {
            NAPI.Task.Run(() =>
            {
                int n = player.GetData("AuthCount");

                AccountModel account = Database.Database.LoginAccount(login, password);
                if (account.status == 0)
                {
                    if (n == 3)
                    {
                        player.TriggerEvent("destroyBrowser");
                        player.Kick("Вы привысили количество попыток авторизации.");
                    }
                    else
                    {
                        player.SetData("AuthCount", ++n);
                        player.TriggerEvent("showError");
                    }
                }
                else
                {
                    Console.WriteLine("Игрок {0} вошел в игру и залогинился. SocialClub: {1}", login, player.SocialClubName);
                    player.Name = login; // устанавливаем логин персонажа
                    // Получаем список персонажей в лист.
                    List<string> playerList = Database.Database.GetAccountCharacters(login);
                    List<CharacterModel> charactersInfo = Database.Database.GetCharacterInfoToLogin(login);
                    // Получаем информацию о персонажах в лист.
                    player.TriggerEvent("destroyBrowser");
                    string characters = NAPI.Util.ToJson(playerList);
                    string charactersInformation = NAPI.Util.ToJson(charactersInfo);
                    Console.WriteLine(charactersInformation);
                    player.TriggerEvent("showPlayerCharacters", characters, account.slot_3, account.slot_4, account.donate, login, charactersInformation);
                }
            });
        }

        // Если персонаж выбран, то загружаем его.
        [RemoteEvent("charSelected")]
        public static void OnCharSelected(Client player, string characterName)
        {
            NAPI.Task.Run(() =>
            {
                CharacterModel characterModel = Database.Database.LoadCharacterByName(characterName, player.Name, player.Address);
                SkinModel skinModel = Database.Database.GetCharacterSkin(characterModel.id);
                List<ClothesModel> clothesModel = Database.Database.GetCharacterClothes(characterModel.id);

                Console.WriteLine(characterName + " вошел на сервер.");

                LoadCharacterData(player, characterModel);

                NAPI.Player.SetPlayerNametag(player, characterName);
                Console.WriteLine("{0}", characterModel.sex);
                NAPI.Player.SetPlayerSkin(player, characterModel.sex == 0 ? PedHash.FreemodeMale01 : PedHash.FreemodeFemale01);
                // Устанавливаем внешний вид игроку (в том числе и волосы).
                Customization.ApplyPlayerCustomization(player, skinModel, characterModel.sex);
                // Устанавливаем одежду игроку.
                Customization.ApplyPlayerClothes(player, clothesModel);

     
            });
            //player.TriggerEvent("destroyBrowser");
            player.TriggerEvent("keyListener");
            
        }
    }
}
