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
        #endregion
        public static async void RegisterAccountAsync(string license, string mail, string regIp)
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

    }

}
