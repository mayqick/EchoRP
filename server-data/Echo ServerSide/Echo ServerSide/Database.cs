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
        private static string connectionString = "Server=localhost; Database=echorp; Uid=root; Pwd=";
        private static MySqlConnection connection = new MySqlConnection(connectionString);

        public static async Task<bool> CheckRegistrationAsync(string license)
        {
            try
            {
                await connection.OpenAsync();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT EXISTS(SELECT 1 FROM `accounts` WHERE `license` = @license LIMIT 1)";
                command.Parameters.AddWithValue("@license", license);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            if (reader.GetInt16(0) == 0)
                                return false;
                            return true;
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
        public static async Task RegisterAccountAsync(string socialName, string token, string hwid, string regIp, string mail)
        {
            try
            {
                await connection.OpenAsync();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO `accounts` (`socialName` , `token`, `serial`, `regIp`, `mail`) VALUES(@socialName, @token, @serial, @regIp, @mail)";
                command.Parameters.AddWithValue("@socialName", socialName);
                command.Parameters.AddWithValue("@token", token);
                command.Parameters.AddWithValue("@serial", hwid);
                command.Parameters.AddWithValue("@regIp", regIp);
                command.Parameters.AddWithValue("@mail", mail);
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[EXCEPTION RegisterAccount] " + ex.Message);
                Debug.WriteLine("[EXCEPTION RegisterAccount] " + ex.StackTrace);
            }
        }

    }

}
