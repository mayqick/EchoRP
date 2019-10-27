using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
namespace Echo.Chat
{
    class Voice : Script
    {
        [RemoteEvent("AddVoiceListener")]
        public void OnAddListener(Client player, Client target)
        {
            player.EnableVoiceTo(target);
        }
        [RemoteEvent("RemoveVoiceListener")]
        public void OnRemoveListener(Client player, Client target)
        {
            player.DisableVoiceTo(target);
        }
    }
}
