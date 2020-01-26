using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using CitizenFX.Core.Native;
using CitizenFX.Core;
namespace Echo_ServerSide
{

    public class Database : BaseScript
    {
        public Database()
        {
            ConnectionOpen();
        }
        private static string connectionString = "Server=localhost; Database=echorp; Uid=root; Pwd=";
        private static MySqlConnection connection = new MySqlConnection(connectionString);
        private static async void ConnectionOpen()
        {
            await connection.OpenAsync();
        }
        #region playercheck
        public static async Task<bool> CheckRegistrationAsync(string license)
        {
            try
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT EXISTS(SELECT 1 FROM `accounts` WHERE `license` = @license LIMIT 1)";
                command.Parameters.AddWithValue("@license", license);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            return Convert.ToBoolean(reader.GetInt16(0));
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("[EXCEPTION CheckRegistration] " + ex.Message);
                Debug.WriteLine("[EXCEPTION CheckRegistration] " + ex.StackTrace);
            }
            return false;
        }
        public static async Task<bool> CheckPlayerMailAsync(string mail)
        {
            try
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT EXISTS(SELECT 1 FROM `accounts` WHERE `mail` = @mail LIMIT 1)";
                command.Parameters.AddWithValue("@mail", mail);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            return Convert.ToBoolean(reader.GetInt16(0));
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine("[EXCEPTION CheckPlayerMail] " + ex.Message);
                Debug.WriteLine("[EXCEPTION CheckPlayerMail] " + ex.StackTrace);
            }
            return false;
        }
        public static async Task<bool> CheckPlayerCharactersAsync(int accountId)
        {
            try
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT EXISTS(SELECT 1 FROM `character_list` WHERE `accountId` = @accountId LIMIT 1)";
                command.Parameters.AddWithValue("@accountId", accountId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            return Convert.ToBoolean(reader.GetInt16(0));
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine("[EXCEPTION CheckPlayerCharactersAsync] " + ex.Message);
                Debug.WriteLine("[EXCEPTION CheckPlayerCharactersAsync] " + ex.StackTrace);
            }
            return false;
        }
        public static async Task<int> GetAccountIdByLicenseAsync(string license)
        {
            try
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT `id` FROM `accounts` WHERE `license` = @license LIMIT 1";
                command.Parameters.AddWithValue("@license", license);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        return reader.GetInt32(0);
                    }

                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine("[EXCEPTION GetAccountIdByLicenseAsync] " + ex.Message);
                Debug.WriteLine("[EXCEPTION GetAccountIdByLicenseAsync] " + ex.StackTrace);
            }
            return -1;
        }
        #endregion
        public static async Task RegisterAccountAsync(string license, string mail, string regIp)
        {

            try
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO `accounts` (`license`, `mail`, `regIp`) VALUES(@license, @mail, @regIp)";
                command.Parameters.AddWithValue("@license", license);
                command.Parameters.AddWithValue("@mail", mail);
                command.Parameters.AddWithValue("@regIp", regIp);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[EXCEPTION RegisterAccount] " + ex.Message);
                Debug.WriteLine("[EXCEPTION RegisterAccount] " + ex.StackTrace);
            }

        }
        public static async Task SetCharacterSkinAsync(Models.SkinModel skin, int characterId)
        {
            await Delay(0);
            try
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO skins VALUES (@characterId, @firstHeadShape, @secondHeadShape, @firstSkinTone, @secondSkinTone, @headMix, @skinMix, ";
                command.CommandText += "@hairModel, @firstHairColor, @secondHairColor, @beardModel, @beardColor, @beardOpacity, @chestModel, @chestColor, @blemishesModel, @ageingModel, @ageingOpacity,";
                command.CommandText += "@complexionModel, @sundamageModel, @frecklesModel, @noseWidth, @noseHeight, @noseLength, @noseBridge, @noseTip, @noseShift, @browHeight, ";
                command.CommandText += "@browWidth, @cheekboneHeight, @cheekboneWidth, @cheeksWidth, @eyes, @lips, @jawWidth, @jawHeight, @chinLength, @chinPosition, @chinWidth, ";
                command.CommandText += "@chinShape, @neckWidth, @eyesColor, @eyebrowsModel, @eyebrowsColor, @eyebrowsOpacity, @makeupModel, @blushModel, @blushColor, @lipstickModel, @lipstickColor)";
                command.Parameters.AddWithValue("@characterId", characterId);
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
                command.Parameters.AddWithValue("@beardOpacity", skin.beardOpacity);
                command.Parameters.AddWithValue("@chestModel", skin.chestModel);
                command.Parameters.AddWithValue("@chestColor", skin.chestColor);
                command.Parameters.AddWithValue("@blemishesModel", skin.blemishesModel);
                command.Parameters.AddWithValue("@ageingModel", skin.ageingModel);
                command.Parameters.AddWithValue("@ageingOpacity", skin.ageingOpacity);
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

                command.Parameters.AddWithValue("@eyebrowsOpacity", skin.eyebrowsOpacity);

                command.Parameters.AddWithValue("@makeupModel", skin.makeupModel);
                command.Parameters.AddWithValue("@blushModel", skin.blushModel);
                command.Parameters.AddWithValue("@blushColor", skin.blushColor);

                command.Parameters.AddWithValue("@lipstickModel", skin.lipstickModel);
                command.Parameters.AddWithValue("@lipstickColor", skin.lipstickColor);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[EXCEPTION SetCharacterSkin] " + ex.Message);
                Debug.WriteLine("[EXCEPTION SetCharacterSkin] " + ex.StackTrace);
            }
        }
        public static async Task<int> CreateCharacterAsync(Models.CharacterModel character, int accountId)
        {
            int characterId = 0;
            try
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO character_list (characterName, isMale, accountId) VALUES (@characterName, @isMale, @accountId)";
                command.Parameters.AddWithValue("@characterName", character.characterName);
                command.Parameters.AddWithValue("@isMale", character.isMale);
                command.Parameters.AddWithValue("@accountId", accountId);
                await command.ExecuteNonQueryAsync();

                // Получаем ID созданного персонажа
                characterId = (int)command.LastInsertedId;
                return characterId;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[EXCEPTION CreateCharacterAsync] " + ex.Message);
                Debug.WriteLine("[EXCEPTION CreateCharacterAsync] " + ex.StackTrace);
            }

            return -1;
            
        }
    }

}
