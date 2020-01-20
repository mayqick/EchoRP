using System;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using CitizenFX.Core.Native;
using CitizenFX.Core;
namespace Echo_ServerSide
{
    class Auth : BaseScript
    {
        public Auth()
        {
            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
            EventHandlers.Add("onPlayerSpawned", new Action<Player>(OnPlayerSpawned));
            EventHandlers.Add("onPlayerRegistration", new Action<Player, string>(OnPlayerRegistration));
        }

        private async void OnPlayerRegistration([FromSource]Player player, string mail)
        {
            await Delay(0);
            Debug.WriteLine("true!");
            Debug.WriteLine(mail);
        }
        private async void OnPlayerSpawned([FromSource]Player player)
        {
            Debug.WriteLine("Player spawned!");
            var licenseIdentifier = player.Identifiers["license"];
            if (await Database.CheckRegistrationAsync(licenseIdentifier))
            {
                // todo: если аккаунт в базе
            }
            else
            {
                TriggerClientEvent("onPlayerStartRegistation");

            }
        }
        private async void OnPlayerConnecting([FromSource]Player player, string playerName, dynamic setKickReason, dynamic deferrals)
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
