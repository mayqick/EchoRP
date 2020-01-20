using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core.Native;
using CitizenFX.Core;

namespace Echo_ServerSide
{
    public class Main : BaseScript
    {
        public Main()
        {
            EventHandlers["onResourceStart"] += new Action<string>(OnResourceStart);
        }


        private async void OnResourceStart(string resourceName)
        {
            await Task.Run(() =>
            {
                if (API.GetCurrentResourceName() != resourceName) return;

                Debug.WriteLine($"The resource {resourceName} has been started.");
            });

        }

    }
}
