using GTANetworkAPI;
using Wave.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Text;
using System;

namespace Wave.Database
{
    public class Database : Script
    {
        private static string connectionString = Wave.Global.Constants.connectionString;

        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            NAPI.Util.ConsoleOutput("Подключение к базе данных успешно инициализировано.");

        }
        public static AccountModel GetAccountBySocialName(string socialName)
        {
            // Узнаем статус аккаунта. 0 - не существует, -1 - забанен, иначе существует.
            // Эта простая проверка позволит понять, есть ли аккаунт в базе.
            // Если аккаунт существует, то возвращаем токен и HWID для авторизации.

            AccountModel account = new AccountModel();
            account.status = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT `serial`, `token` FROM `accounts` WHERE `socialName` = @socialName LIMIT 1";
                command.Parameters.AddWithValue("@socialName", socialName);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        account.status = 1;
                        account.serial = reader.GetString("serial");
                        account.token = reader.GetString("token");
                        return account;
                    }
                    else
                    {
                        account.status = 0;
                        return account;
                    }
                }
               
            }

        }
        public static AccountModel GetAccount(string login) 
        {
            // Узнаем статус аккаунта. 0 - не существует, -1 - забанен, иначе существует.
            // Эта простая проверка позволит понять, есть ли аккаунт в базе.

            AccountModel account = new AccountModel();
            account.status = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT status FROM accounts WHERE login = @login LIMIT 1";
                command.Parameters.AddWithValue("@login", login);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        account.status = reader.GetInt16("status");
                    }
                }
            }

            return account;
        }


        public static AccountModel LoginAccount(string login, string password)
        {
            AccountModel account = new AccountModel();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT `status`, `donate`, `slot_3`, `slot_4` FROM `accounts` WHERE `login` = @login AND `password` = SHA2(@password, '256') LIMIT 1";
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@password", password);
                NAPI.Util.ConsoleOutput(command.CommandText);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        account.status = reader.GetInt32("status");
                        account.donate = reader.GetInt32("donate");
                        account.slot_3 = reader.GetBoolean("slot_3");
                        account.slot_4 = reader.GetBoolean("slot_4");
                    }
                    else
                    {
                        account.status = 0;
                        return account;
                    }
                }
            }

            return account;
        }

        public static bool RegisterAccount(string socialName, string token, string hwid, string regIp)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO `accounts` (`socialName` , `token`, `serial`, `regIp`) VALUES(@socialName, @token, @serial, @regIp)";
                    command.Parameters.AddWithValue("@socialName", socialName);
                    command.Parameters.AddWithValue("@token", token);
                    command.Parameters.AddWithValue("@serial", hwid);
                    command.Parameters.AddWithValue("@regIp", regIp);
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    NAPI.Util.ConsoleOutput("[EXCEPTION RegisterAccount] " + ex.Message);
                    NAPI.Util.ConsoleOutput("[EXCEPTION RegisterAccount] " + ex.StackTrace);
                    return false;
                }
            }
        }

        public static List<string> GetAccountCharacters(string login)
        {
            List<string> characters = new List<string>();


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT characterName FROM characterlist WHERE login = @account";
                command.Parameters.AddWithValue("@account", login);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString("characterName");
                        characters.Add(name);
                    }
                }
            }

            return characters;
        }

        public static int CreateCharacter(Client player, CharacterModel characterModel, SkinModel skin, List<ClothesModel> clothesModels)
        {
            int playerId = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO characterlist (characterName, age, sex, login) VALUES (@characterName, @playerAge, @playerSex, @login)";
                    command.Parameters.AddWithValue("@characterName", characterModel.characterName);
                    command.Parameters.AddWithValue("@playerAge", characterModel.age);
                    command.Parameters.AddWithValue("@playerSex", characterModel.sex);
                    command.Parameters.AddWithValue("@login", player.Name);
                    command.ExecuteNonQuery();

                    // Получаем ID созданного персонажа
                    playerId = (int)command.LastInsertedId;

                    // Store player's skin
                    command.CommandText = "INSERT INTO skins VALUES (@playerId, @firstHeadShape, @secondHeadShape, @firstSkinTone, @secondSkinTone, @headMix, @skinMix, ";
                    command.CommandText += "@hairModel, @firstHairColor, @secondHairColor, @beardModel, @beardColor, @chestModel, @chestColor, @blemishesModel, @ageingModel, ";
                    command.CommandText += "@complexionModel, @sundamageModel, @frecklesModel, @noseWidth, @noseHeight, @noseLength, @noseBridge, @noseTip, @noseShift, @browHeight, ";
                    command.CommandText += "@browWidth, @cheekboneHeight, @cheekboneWidth, @cheeksWidth, @eyes, @lips, @jawWidth, @jawHeight, @chinLength, @chinPosition, @chinWidth, ";
                    command.CommandText += "@chinShape, @neckWidth, @eyesColor, @eyebrowsModel, @eyebrowsColor, @makeupModel, @blushModel, @blushColor, @lipstickModel, @lipstickColor)";
                    command.Parameters.AddWithValue("@playerId", playerId);
                    command.Parameters.AddWithValue("@firstHeadShape", skin.firstHeadShape);
                    command.Parameters.AddWithValue("@secondHeadShape", skin.secondHeadShape);
                    command.Parameters.AddWithValue("@firstSkinTone", skin.firstSkinTone);
                    command.Parameters.AddWithValue("@secondSkinTone", skin.secondSkinTone);
                    command.Parameters.AddWithValue("@headMix", skin.headMix);
                    command.Parameters.AddWithValue("@skinMix", skin.skinMix);
                    command.Parameters.AddWithValue("@hairModel", skin.hairModel);
                    command.Parameters.AddWithValue("@firstHairColor", skin.firstHairColor);
                    command.Parameters.AddWithValue("@secondHairColor", skin.secondHairColor);
                    command.Parameters.AddWithValue("@beardModel", skin.beardModel);
                    command.Parameters.AddWithValue("@beardColor", skin.beardColor);
                    command.Parameters.AddWithValue("@chestModel", skin.chestModel);
                    command.Parameters.AddWithValue("@chestColor", skin.chestColor);
                    command.Parameters.AddWithValue("@blemishesModel", skin.blemishesModel);
                    command.Parameters.AddWithValue("@ageingModel", skin.ageingModel);
                    command.Parameters.AddWithValue("@complexionModel", skin.complexionModel);
                    command.Parameters.AddWithValue("@sundamageModel", skin.sundamageModel);
                    command.Parameters.AddWithValue("@frecklesModel", skin.frecklesModel);
                    command.Parameters.AddWithValue("@noseWidth", skin.noseWidth);
                    command.Parameters.AddWithValue("@noseHeight", skin.noseHeight);
                    command.Parameters.AddWithValue("@noseLength", skin.noseLength);
                    command.Parameters.AddWithValue("@noseBridge", skin.noseBridge);
                    command.Parameters.AddWithValue("@noseTip", skin.noseTip);
                    command.Parameters.AddWithValue("@noseShift", skin.noseShift);
                    command.Parameters.AddWithValue("@browHeight", skin.browHeight);
                    command.Parameters.AddWithValue("@browWidth", skin.browWidth);
                    command.Parameters.AddWithValue("@cheekboneHeight", skin.cheekboneHeight);
                    command.Parameters.AddWithValue("@cheekboneWidth", skin.cheekboneWidth);
                    command.Parameters.AddWithValue("@cheeksWidth", skin.cheeksWidth);
                    command.Parameters.AddWithValue("@eyes", skin.eyes);
                    command.Parameters.AddWithValue("@lips", skin.lips);
                    command.Parameters.AddWithValue("@jawWidth", skin.jawWidth);
                    command.Parameters.AddWithValue("@jawHeight", skin.jawHeight);
                    command.Parameters.AddWithValue("@chinLength", skin.chinLength);
                    command.Parameters.AddWithValue("@chinPosition", skin.chinPosition);
                    command.Parameters.AddWithValue("@chinWidth", skin.chinWidth);
                    command.Parameters.AddWithValue("@chinShape", skin.chinShape);
                    command.Parameters.AddWithValue("@neckWidth", skin.neckWidth);
                    command.Parameters.AddWithValue("@eyesColor", skin.eyesColor);
                    command.Parameters.AddWithValue("@eyebrowsModel", skin.eyebrowsModel);
                    command.Parameters.AddWithValue("@eyebrowsColor", skin.eyebrowsColor);
                    command.Parameters.AddWithValue("@makeupModel", skin.makeupModel);
                    command.Parameters.AddWithValue("@blushModel", skin.blushModel);
                    command.Parameters.AddWithValue("@blushColor", skin.blushColor);
                    command.Parameters.AddWithValue("@lipstickModel", skin.lipstickModel);
                    command.Parameters.AddWithValue("@lipstickColor", skin.lipstickColor);
                    command.ExecuteNonQuery();
                    
                    // Получаем модели одежды из листа моделей.
                    foreach (ClothesModel cloth in clothesModels)
                    {
                        // Очищаем параметры команды.
                        command.Parameters.Clear();
                        command.CommandText = "INSERT INTO clothes (characterid, slot, drawable, texture) VALUES (@characterid, @slot, @drawable, @texture)";
                        command.Parameters.AddWithValue("@characterid", playerId);
                        command.Parameters.AddWithValue("@slot", cloth.slot);
                        command.Parameters.AddWithValue("@drawable", cloth.drawable);
                        command.Parameters.AddWithValue("@texture", cloth.texture);
                        command.ExecuteNonQuery();
                    }
                    
                }
                catch (Exception ex)
                {
                    NAPI.Util.ConsoleOutput("[EXCEPTION CreateCharacter] " + ex.Message);
                    NAPI.Util.ConsoleOutput("[EXCEPTION CreateCharacter] " + ex.StackTrace);
                    player.TriggerEvent("characterNameDuplicated", characterModel.characterName);
                }
            }

            return playerId;
        }
        // Загрузка внешнего вида персонажа.
        public static SkinModel GetCharacterSkin(int characterId)
        {
            SkinModel skin = new SkinModel();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM skins WHERE characterId = @characterId LIMIT 1";
                command.Parameters.AddWithValue("@characterId", characterId);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        skin.firstHeadShape = reader.GetInt32("firstHeadShape");
                        skin.secondHeadShape = reader.GetInt32("secondHeadShape");
                        skin.firstSkinTone = reader.GetInt32("firstSkinTone");
                        skin.secondSkinTone = reader.GetInt32("secondSkinTone");
                        skin.headMix = reader.GetFloat("headMix");
                        skin.skinMix = reader.GetFloat("skinMix");
                        skin.hairModel = reader.GetInt32("hairModel");
                        skin.firstHairColor = reader.GetInt32("firstHairColor");
                        skin.secondHairColor = reader.GetInt32("secondHairColor");
                        skin.beardModel = reader.GetInt32("beardModel");
                        skin.beardColor = reader.GetInt32("beardColor");
                        skin.chestModel = reader.GetInt32("chestModel");
                        skin.chestColor = reader.GetInt32("chestColor");
                        skin.blemishesModel = reader.GetInt32("blemishesModel");
                        skin.ageingModel = reader.GetInt32("ageingModel");
                        skin.complexionModel = reader.GetInt32("complexionModel");
                        skin.sundamageModel = reader.GetInt32("sundamageModel");
                        skin.frecklesModel = reader.GetInt32("frecklesModel");
                        skin.noseWidth = reader.GetFloat("noseWidth");
                        skin.noseHeight = reader.GetFloat("noseHeight");
                        skin.noseLength = reader.GetFloat("noseLength");
                        skin.noseBridge = reader.GetFloat("noseBridge");
                        skin.noseTip = reader.GetFloat("noseTip");
                        skin.noseShift = reader.GetFloat("noseShift");
                        skin.browHeight = reader.GetFloat("browHeight");
                        skin.browWidth = reader.GetFloat("browWidth");
                        skin.cheekboneHeight = reader.GetFloat("cheekboneHeight");
                        skin.cheekboneWidth = reader.GetFloat("cheekboneWidth");
                        skin.cheeksWidth = reader.GetFloat("cheeksWidth");
                        skin.eyes = reader.GetFloat("eyes");
                        skin.lips = reader.GetFloat("lips");
                        skin.jawWidth = reader.GetFloat("jawWidth");
                        skin.jawHeight = reader.GetFloat("jawHeight");
                        skin.chinLength = reader.GetFloat("chinLength");
                        skin.chinPosition = reader.GetFloat("chinPosition");
                        skin.chinWidth = reader.GetFloat("chinWidth");
                        skin.chinShape = reader.GetFloat("chinShape");
                        skin.neckWidth = reader.GetFloat("neckWidth");
                        skin.eyesColor = reader.GetInt32("eyesColor");
                        skin.eyebrowsModel = reader.GetInt32("eyebrowsModel");
                        skin.eyebrowsColor = reader.GetInt32("eyebrowsColor");
                        skin.makeupModel = reader.GetInt32("makeupModel");
                        skin.blushModel = reader.GetInt32("blushModel");
                        skin.blushColor = reader.GetInt32("blushColor");
                        skin.lipstickModel = reader.GetInt32("lipstickModel");
                        skin.lipstickColor = reader.GetInt32("lipstickColor");
                    }
                }
            }

            return skin;
        }
        // Получение одежды персонажа по ID.
        public static List<ClothesModel> GetCharacterClothes(int characterId)
        {
            List<ClothesModel> clothesList = new List<ClothesModel>();
            

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM clothes WHERE characterid = @characterid";
                command.Parameters.AddWithValue("@characterid", characterId);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ClothesModel clothes = new ClothesModel();
                        clothes.slot = reader.GetInt32("slot");
                        clothes.drawable = reader.GetInt32("drawable");
                        clothes.texture = reader.GetInt32("texture");
                        clothesList.Add(clothes);
                    }
                }
            }

            return clothesList;
        }
        public static CharacterModel LoadCharacterByName(string characterName, string playername, string ip)
        {
            CharacterModel character = new CharacterModel();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                // Обновление последнего IP.
                command.CommandText = "UPDATE accounts SET lastIp = @lastIp WHERE login = @login LIMIT 1";
                command.Parameters.AddWithValue("@lastIp", ip);
                command.Parameters.AddWithValue("@login", playername);
                command.ExecuteNonQuery();

                command.CommandText = "SELECT * FROM characterlist WHERE characterName = @characterName LIMIT 1";
                command.Parameters.AddWithValue("@characterName", characterName);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        character.id            = reader.GetInt32("id");
                        character.characterName = reader.GetString("characterName");
                        character.sex           = reader.GetInt32("sex");
                        character.posX          = reader.GetFloat("posX");
                        character.posY          = reader.GetFloat("posY");
                        character.posZ          = reader.GetFloat("posZ");
                        character.rotation      = reader.GetFloat("rotation");
                        character.adminRank     = reader.GetInt32("adminRank");
                        character.money         = reader.GetInt32("money");
                        character.bank          = reader.GetInt32("bank");
                        character.health        = reader.GetInt32("health");
                        character.armor         = reader.GetInt32("armor");
                        character.played        = reader.GetInt32("played");
                        character.xp            = reader.GetInt32("xp");
                        character.lvl           = reader.GetInt32("lvl");
                        character.hunger        = reader.GetFloat("hunger");
                        character.thirst        = reader.GetFloat("thirst");
                    }
                }
            }

            return character;
        }
        public static void SaveCharacterById(CharacterModel characterModel)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE characterlist SET posX = @posX, posY = @posY, posZ = @posZ, health = @health, armor = @armor, money =  @money, bank = @bank, played = @played, hunger = @hunger, thirst = @thirst WHERE id = @id LIMIT 1";
                command.Parameters.AddWithValue("@id", characterModel.id);
                command.Parameters.AddWithValue("@posX", characterModel.posX);
                command.Parameters.AddWithValue("@posY", characterModel.posY);
                command.Parameters.AddWithValue("@posZ", characterModel.posZ);
                command.Parameters.AddWithValue("@health", characterModel.health);
                command.Parameters.AddWithValue("@armor", characterModel.armor);
                command.Parameters.AddWithValue("@money", characterModel.money);
                command.Parameters.AddWithValue("@bank", characterModel.bank);
                command.Parameters.AddWithValue("@played", characterModel.played);
                command.Parameters.AddWithValue("@hunger", characterModel.hunger);
                command.Parameters.AddWithValue("@thirst", characterModel.thirst);
                command.ExecuteNonQuery();
            }
        }
        public static void PayDayUpdate(int lvl, int xp, int played, int characterId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE characterlist SET lvl = @lvl, xp = @xp, played = @played WHERE id = @id LIMIT 1";
                command.Parameters.AddWithValue("@id", characterId);
                command.Parameters.AddWithValue("@lvl", lvl);
                command.Parameters.AddWithValue("@xp", xp);
                command.Parameters.AddWithValue("@played", played);

                command.ExecuteNonQuery();
            }
        }

        public static List<InventoryModel> LoadPlayerIventoryItems(int characterId)
        {
            List<InventoryModel> inventoryModels = new List<InventoryModel>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM inventory_players WHERE characterid = @characterid";
                command.Parameters.AddWithValue("@characterid", characterId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InventoryModel item = new InventoryModel
                        {
                            id = reader.GetString("itemid"),
                            slot = reader.GetString("slot"),
                            count = reader.GetInt32("count")
                        };

                        inventoryModels.Add(item);
                    }
                }
            }

            return inventoryModels;
        }

        public static void ChangeItemPosition(int characterId, string from, string to)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE inventory_players SET slot = @to WHERE characterid = @characterid AND slot = @from LIMIT 1";
                command.Parameters.AddWithValue("@characterid", characterId);
                command.Parameters.AddWithValue("@to", to);
                command.Parameters.AddWithValue("@from", from);

                command.ExecuteNonQuery();
            }
        }
        public static void SetAdminRank(int sqlId, int adminRank)
        {
            // Устанавливаем админ лвл персонажу по его ID.
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE characterlist SET adminRank = @adminRank WHERE id = @characterid LIMIT 1";
                command.Parameters.AddWithValue("@characterid", sqlId);
                command.Parameters.AddWithValue("@adminRank", adminRank);
                command.ExecuteNonQuery();
            }
        }
        public static List<CharacterModel> GetCharacterInfoToLogin(string login)
        {
            // Получение данных о персонаже для страницы выбора персонажей.
            List<CharacterModel> characterModel = new List<CharacterModel>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM characterlist WHERE login = @login";
                command.Parameters.AddWithValue("@login", login);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CharacterModel character = new CharacterModel
                        {
                            lvl     = reader.GetInt32("lvl"),
                            xp      = reader.GetInt32("xp"),
                            money   = reader.GetInt32("money"),
                            bank    = reader.GetInt32("bank")
                        };

                        characterModel.Add(character);
                    }
                }
            }

            return characterModel;
        }
    }
}