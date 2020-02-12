using System;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using CitizenFX.Core.Native;
using CitizenFX.Core;
using Newtonsoft.Json;
namespace Echo_ServerSide
{
    class Auth : BaseScript
    {
        public Auth()
        {
            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
            EventHandlers.Add("onPlayerSpawned", new Action<Player>(OnPlayerSpawned));
            EventHandlers.Add("onPlayerRegistration", new Action<Player, string>(OnPlayerRegistration));
            EventHandlers.Add("onPlayerSaveCharacterInformation", new Action<Player, string, string>(OnPlayerSaveCharacterInformation));
            EventHandlers.Add("onPlayerConnected", new Action<Player>(OnPlayerConnected));
        }

        // Получаем данные кастомизации, сохраняем скин и создаем нового персонада
        private async void OnPlayerSaveCharacterInformation([FromSource]Player player, string skin, string character)
        {

            // Преобразуем JSON к типам. А чо, звучит хайпово
            var skinModel = JsonConvert.DeserializeObject<Models.SkinModel>(skin);
            var characterModel = JsonConvert.DeserializeObject<Models.CharacterModel>(character);

            // получаем ИД аккаунта в БД чтобы создать персонажа с привязкой по этому ИД.
            var accountId = await Database.GetAccountIdByLicenseAsync(player.Identifiers["license"]);
   
            // Создаем персонажа и получаем его ИД
            var characterId = await Database.CreateCharacterAsync(characterModel, accountId);

            // Сохраняем настройки кастомизации игрока
            Database.SetCharacterSkinAsync(skinModel, characterId);

            TriggerClientEvent(player, "onPlayerFinishedCharacterCustomizing");
        }
        // 
        private static async void OnPlayerRegistration([FromSource]Player player, string mail)
        {

            Random random = new Random();
            string code = random.Next(10).ToString() + random.Next(10).ToString() + random.Next(10).ToString() + random.Next(10).ToString();
            Debug.WriteLine(mail);
            if (await Database.CheckPlayerMailAsync(mail))
            {
                // todo: license не совпадает, но введенный mail есть в базе
                // todo: send mail
                /*Mail.SendEmailAsync(mail, "Echo Role Play", "Echo Role Play - код подтверждения", $"Ваш код подтверждения входа в аккаунт: {code}");*/
            }
            else
            {

                /*Mail.SendEmailAsync(mail, "Echo Role Play", "Echo Role Play - код подтверждения", $"Ваш код подтверждения регистрации аккаунта: {code}");*/
                // todo: если код совпадает, то создаем аккаунт
                Database.RegisterAccountAsync(player.Identifiers["license"], mail, player.EndPoint);
                TriggerClientEvent(player, "onPlayerCharacterCreating");
                /*TriggerClientEvent("setPlayerRegisterMailCode", code);*/
            }

        }
        private async void OnPlayerConnected([FromSource]Player player)
        {

            var licenseIdentifier = player.Identifiers["license"];
            if (await Database.CheckRegistrationAsync(licenseIdentifier))
            {

                int accountId = await Database.GetAccountIdByLicenseAsync(licenseIdentifier);
                // Проверяем, есть ли у аккаунта персонажи. Если ни одного, то создаем сразу, если есть, то переходим к выбору
                if (await Database.CheckPlayerCharactersAsync(accountId))
                {
                    TriggerClientEvent(player, "onPlayerCharacterChoice");
                    // если у аккаунта есть персонажи, то открываем их выбор
                }
                else
                {

                    TriggerClientEvent(player, "onPlayerCharacterCreating");
                    // у аккаунта нет ни одного персонажа. Открываем окно создания персонажа.
                }


            }
            else
            {
                // Аккаунта с данным license идентефикатором нет. Открываем страницу регистрации (ввод mail)
                TriggerClientEvent(player, "onPlayerStartRegistation");
            }
        }
        private async void OnPlayerSpawned([FromSource]Player player)
        {

        }
        // Ивент вызывается при присоединении к серверу (до загрузки, в самом клиенте FiveM)
        private static async void OnPlayerConnecting([FromSource]Player player, string playerName, dynamic setKickReason, dynamic deferrals)
        {
            deferrals.defer();

            // mandatory wait!
            await Delay(0);

            var licenseIdentifier = player.Identifiers["license"];

            Debug.WriteLine($"A player with the name {playerName} (Identifier: [{licenseIdentifier}]) is connecting to the server.");

            deferrals.update($"Hello {playerName}, your license [{licenseIdentifier}] is being checked");


            deferrals.done();
        }
    }
}
