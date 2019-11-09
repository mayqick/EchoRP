using GTANetworkAPI;
using System;
using Echo.Global;
namespace Echo.Commands
{
    class Admin : Script
    {
        [Flags]
        public enum AnimationFlags
        {
            Loop = 1 << 0,
            StopOnLastFrame = 1 << 1,
            OnlyAnimateUpperBody = 1 << 4,
            AllowPlayerControl = 1 << 5,
            Cancellable = 1 << 7
        }

        [Command(Messages.COM_MAKEADMIN, Messages.GEN_MAKEADMIN)]
        public void CMD_MakeAdmin(Client player, string toPlayer, int adminValue)
        {
            if (player.GetData<int>(EntityData.PLAYER_ADMIN_RANK) < 5) return;

            Client toAdmin = NAPI.Player.GetPlayerFromName(toPlayer);
            if (toAdmin.Name == player.Name)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Messages.SECURITY_SELF_ERROR);
                return;
            }
            Database.Database.SetAdminRank(toAdmin.GetData<int>(EntityData.PLAYER_ADMIN_RANK), adminValue);
            foreach (Client client in NAPI.Pools.GetAllPlayers())
            {
                if (client.GetData<int>(EntityData.PLAYER_ADMIN_RANK) >= 1)
                {
                    if (!toAdmin.HasData(EntityData.PLAYER_ADMIN_RANK)) NAPI.Chat.SendChatMessageToPlayer(client, Constants.COLOR_ADMIN_INFO, string.Format(Messages.ADMIN_ASSIGNED, player.GetSharedData(EntityData.PLAYER_NAME), NAPI.Player.GetPlayerName(toAdmin)));
                    else NAPI.Chat.SendChatMessageToPlayer(client, Constants.COLOR_ADMIN_INFO, string.Format(Messages.ADMIN_LVL_CHANGED, player.GetSharedData(EntityData.PLAYER_NAME), NAPI.Player.GetPlayerName(toAdmin), adminValue));
                }
            }
            toAdmin.SetData(EntityData.PLAYER_ADMIN_RANK, adminValue);
        }
        [Command(Messages.COM_GOTO, Messages.GEN_GOTO_COMMAND)]
        public void CMD_Goto(Client player, string player_name)
        {
            if (player.GetData<int>(EntityData.PLAYER_ADMIN_RANK) < 2) return;

            Vector3 toPlayer = NAPI.Player.GetPlayerFromName(player_name).Position;
            NAPI.Player.SpawnPlayer(player, toPlayer);
            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_ADMIN_INFO, string.Format(Messages.ADMIN_TELEPORTED, player_name));
        }
        [Command("int")]
        public void CMD_Int(Client player, int type)
        {
            if (player.GetData<int>(EntityData.PLAYER_ADMIN_RANK) < 2) return;
            switch (type) {
                case 5:
                    NAPI.Player.PlayPlayerAnimation(player, (int)(AnimationFlags.Loop | AnimationFlags.OnlyAnimateUpperBody | AnimationFlags.AllowPlayerControl), "move_m@tired", "walk");
                    break;
            }
            
        }
        [Command(Messages.COM_VEH, Messages.GEN_VEH_COMMAND)]
        public void CMD_CreateVehicle(Client player, string vehicle_name)
        {
            if (player.GetData<int>(EntityData.PLAYER_ADMIN_RANK) == 0) return;

            VehicleHash vehicle = NAPI.Util.VehicleNameToModel(vehicle_name);
            NAPI.Vehicle.CreateVehicle(vehicle, player.Position, 0f, new Color(0, 255, 100), new Color(0, 255, 100));
            foreach (Client client in NAPI.Pools.GetAllPlayers())
            {
                if (client.GetData<int>(EntityData.PLAYER_ADMIN_RANK) >= 1)
                {
                    NAPI.Chat.SendChatMessageToPlayer(client, Constants.COLOR_ADMIN_INFO, string.Format(Messages.ADMIN_VEH_CREATED, player.GetSharedData(EntityData.PLAYER_NAME), vehicle_name));
                }
            }
        }
        [Command(Messages.COM_WEAPON, Messages.GEN_WEAPON_COMMAND)]
        public void CMD_GiveWeapon(Client player, string weaponName)
        {
            if (player.GetData<int>(EntityData.PLAYER_ADMIN_RANK) == 0) return;

            WeaponHash weapon = NAPI.Util.WeaponNameToModel(weaponName);
            player.GiveWeapon(weapon, 99999999);

            foreach (Client client in NAPI.Pools.GetAllPlayers())
            {
                if (client.GetData<int>(EntityData.PLAYER_ADMIN_RANK) >= 1)
                {
                    NAPI.Chat.SendChatMessageToPlayer(client, Constants.COLOR_ADMIN_INFO, string.Format(Messages.ADMIN_SETWEAPON, player.GetSharedData(EntityData.PLAYER_NAME), weaponName));
                }
            }
        }

        [Command(Messages.COM_MODEL, Messages.GEN_MODEL_COMMAND)]
        public void CMD_Model(Client player, string model)
        {
            if (player.GetData<int>(EntityData.PLAYER_ADMIN_RANK) == 0) return;

            PedHash ped = NAPI.Util.PedNameToModel(model);
            NAPI.Player.SetPlayerSkin(player, ped);
            foreach (Client client in NAPI.Pools.GetAllPlayers())
            {
                if (client.GetData<int>(EntityData.PLAYER_ADMIN_RANK) >= 1)
                {
                    NAPI.Chat.SendChatMessageToPlayer(client, Constants.COLOR_ADMIN_INFO, string.Format(Messages.ADMIN_SET_MODEL, player.GetSharedData(EntityData.PLAYER_NAME), model));
                }
            }
        }

        [Command(Messages.COM_SETHP, Messages.GEN_SETHP_COMMAND)]
        public void CMD_SetHp(Client player, string playerName, int HP)
        {
            if (player.GetData<int>(EntityData.PLAYER_ADMIN_RANK) == 0) return;
            Client toPlayer = NAPI.Player.GetPlayerFromName(playerName);

            toPlayer.Health = HP;
            foreach (Client client in NAPI.Pools.GetAllPlayers())
            {
                if (client.GetData<int>(EntityData.PLAYER_ADMIN_RANK) >= 1)
                {
                    NAPI.Chat.SendChatMessageToPlayer(client, Constants.COLOR_ADMIN_INFO, string.Format(Messages.ADMIN_SET_HP, player.GetSharedData(EntityData.PLAYER_NAME), HP, NAPI.Player.GetPlayerName(toPlayer)));
                }
            }
        }

    }
}
