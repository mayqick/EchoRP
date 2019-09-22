using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
namespace Wave.Account
{
    class Register : Script
    {
        // Создаем аккаунт пользователя.
        [RemoteEvent("createAccount")]
        public void CreatePlayerAccount(Client player, string login, string password, string promo)
        {
            NAPI.Task.Run(() =>
            {
                if (Database.Database.RegisterAccount(login, player.SocialClubName, password, promo, player.Address))
                {
                    //если аккаунта не существует, то продолжаем
                    player.TriggerEvent("destroyBrowser");
                    Console.WriteLine("Зарегистрировался новый пользователь - {0}.", login);
                    player.TriggerEvent("showAuthPage", player.Name);
                }
                else player.TriggerEvent("regError");
            });
        }
    }
}
