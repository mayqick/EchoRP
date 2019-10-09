using System;
using System.Collections.Generic;
using System.Text;
using Wave.Character;
using GTANetworkAPI;
using Wave.Model;
using Wave.Global;
namespace Wave.Account
{
    class Auth : Script
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

            /*player.SetData("AuthCount", (int)0); // Устанавливаем счетчик попыток входа в сис-му на 0.
            player.TriggerEvent("showAuthPage");*/

            Creator.OnCharCreate(player);
            //AuthPlayerAccount(player);

        }
        // Создаем аккаунт пользователя.
        [RemoteEvent("createAccount")]
        public void AuthPlayerAccount(Client player)
        {
            AccountModel account = new AccountModel();
            account = Database.Database.GetAccountBySocialName(player.SocialClubName);
            if (account.status == 0) // Если аккаунта нет, то создаем его.
            {
                Random random = new Random();
                string token = "";

                Char[] pwdChars = new Char[36] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                for (int i = 0; i < 31; i++)
                    token += pwdChars[random.Next(0, 35)];

                NAPI.Task.Run(() =>
                {
                    if (Database.Database.RegisterAccount(player.SocialClubName, token, player.Serial, player.Address))
                        player.TriggerEvent("setToken", token);
                });
            }
            else   
            {
                player.TriggerEvent("getToken");

                while (player.HasData("token") == false)
                {
                    continue;
                }
                if (player.GetData("token") == account.token && player.Serial == account.serial)
                    NAPI.Util.ConsoleOutput("Успешная авторизация.");
                else NAPI.Util.ConsoleOutput("Неуспешная авторизация.");
            }
        }
        [RemoteEvent("setTokenData")]
        public void SetTokenToPlayerData(Client player, string token)
        {
            player.SetData("token", token);
        }
        [RemoteEvent("loginAccount")]
        public void LoginToAccount(Client player, string login, string password)
        {
            NAPI.Task.Run(() =>
            {
                int attempts = player.GetData("AuthCount");

                AccountModel account = Database.Database.LoginAccount(login, password);
                if (account.status == 0)
                {
                    if (attempts == 3)
                    {
                        player.TriggerEvent("destroyBrowser");
                        player.Kick("Вы привысили количество попыток авторизации.");
                    }
                    else
                    {
                        player.SetData("AuthCount", ++attempts);
                        player.TriggerEvent("showError");
                    }
                }
                else
                {
                    NAPI.Util.ConsoleOutput("Игрок {0} вошел в игру и залогинился. SocialClub: {1}", login, player.SocialClubName);
                    player.Name = login; // устанавливаем логин персонажа
                    // Получаем список персонажей в лист.
                    List<string> playerList = Database.Database.GetAccountCharacters(login);
                    List<CharacterModel> charactersInfo = Database.Database.GetCharacterInfoToLogin(login);
                    // Получаем информацию о персонажах в лист.
                    player.TriggerEvent("destroyBrowser");
                    string characters = NAPI.Util.ToJson(playerList);
                    string charactersInformation = NAPI.Util.ToJson(charactersInfo);
                    NAPI.Util.ConsoleOutput(charactersInformation);
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

                NAPI.Util.ConsoleOutput(characterName + " вошел на сервер.");

                LoadCharacterData(player, characterModel);

                NAPI.Player.SetPlayerNametag(player, characterName);
                NAPI.Util.ConsoleOutput("{0}", characterModel.sex);
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
