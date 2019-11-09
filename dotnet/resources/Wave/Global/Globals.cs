using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using GTANetworkAPI;
using Echo.Model;
namespace Echo.Global
{       
    //NAPI.Task.Run(() =>
    //        {

    //        });
    class Globals : Script
    {
        private Timer minuteTimer;
        private Timer secondTimer;

        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            NAPI.Util.ConsoleOutput($"{player.Name} покинул игру.");
        }
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            NAPI.Util.ConsoleOutput("Дака компиляции: {0}", DateTime.Now);
            secondTimer = new Timer(OnSecondSpent, null, 1000, 1000);
            minuteTimer = new Timer(OnMinuteSpent, null, 60000, 60000);
        }
        private void OnSecondSpent(object unused)
        {
            TimeSpan currentTime = TimeSpan.FromTicks(DateTime.Now.Ticks);
            foreach (Client player in NAPI.Pools.GetAllPlayers())
            {
                if (player.HasSharedData(EntityData.PLAYER_PLAYING))
                {
                    if (currentTime.Minutes == 0 && currentTime.Seconds == 0)
                    {
                        if (player.GetData<int>(EntityData.PLAYER_PLAYED) >= 20)
                        {
                            // limit - макс. XP для текущего лвла, nextlimit - макс. XP для след. лвла, previousXP - XP до payday, 
                            // currentxp - xp после paydat, currentlvl - текущий лвл.
                            int previousXP = player.GetData<int>(EntityData.PLAYER_XP);
                            player.SetData(EntityData.PLAYER_XP, previousXP + 1);
                            int currentLVL = player.GetData<int>(EntityData.PLAYER_LVL);
                            int nextlvl = player.GetData<int>(EntityData.PLAYER_LVL) + 1;
                            int limit = 6 + 2 * (nextlvl - 2);
                            if (player.GetData<int>(EntityData.PLAYER_XP) == limit)
                            {
                                player.TriggerEvent("updateRankBar", limit, limit + 2, previousXP, previousXP + 1, currentLVL);
                                player.SetData(EntityData.PLAYER_LVL, currentLVL + 1);
                            }
                            else
                            {
                                player.TriggerEvent("updateRankBar", limit, limit + 2, previousXP, previousXP + 1, currentLVL);
                            }
                            player.SetData(EntityData.PLAYER_PLAYED, 0);
                            int id = player.GetData<int>(EntityData.PLAYER_SQL_ID);
                            int xp = previousXP + 1;
                            int lvl = player.GetData<int>(EntityData.PLAYER_LVL);
                            NAPI.Task.Run(() => Database.Database.PayDayUpdate(lvl, xp, 0, id));
                        }
                        else
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_ERROR + Messages.PAYDAY_ERROR);
                        }
                    }
                }
            }
        }
        private void OnMinuteSpent(object unused)
        {
            // Устанавливаем серверное время.
            TimeSpan currentTime = TimeSpan.FromTicks(DateTime.Now.Ticks);
            NAPI.World.SetTime(currentTime.Hours, currentTime.Minutes, currentTime.Seconds);

            foreach (Client player in NAPI.Pools.GetAllPlayers())
            {
                if (player.HasSharedData(EntityData.PLAYER_PLAYING))
                {
                    player.SetSharedData(EntityData.PLAYER_HUNGER, player.GetSharedData(EntityData.PLAYER_HUNGER) - 0.5f);
                    player.SetSharedData(EntityData.PLAYER_THIRST, player.GetSharedData(EntityData.PLAYER_THIRST) - 1f);
                    player.SetData(EntityData.PLAYER_PLAYED, player.GetData<int>(EntityData.PLAYER_PLAYED) + 1);
                    float hunger = player.GetSharedData(EntityData.PLAYER_HUNGER);
                    float thirst = player.GetSharedData(EntityData.PLAYER_THIRST);

                    // сообщения  о голоде и жажде
                    if (hunger < 50 && hunger % 10 == 0) {
                        if (hunger < 10 && hunger % 5 == 0)
                        {
                            player.TriggerEvent("StaminaMod", 1);
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_INFO + Messages.PLAYER_VERY_HUNGER);
                        }
                        else NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_INFO + Messages.PLAYER_HUNGER);
                    }
                    if (thirst < 50 && thirst % 10 == 0)
                    {
                        if (hunger < 20 && thirst % 5 == 0)
                        {
                            player.TriggerEvent("StaminaMod", 1);
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_INFO + Messages.PLAYER_VERY_THIRST);
                        }
                        else NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_INFO + Messages.PLAYER_THIRST);
                    }
                    ///////////////////////////////
                    
                    CharacterModel character = new CharacterModel
                    {
                        id = player.GetData<int>(EntityData.PLAYER_SQL_ID),
                        armor = player.GetData<int>(EntityData.PLAYER_ARMOR),
                        health = player.GetData<int>(EntityData.PLAYER_HEALTH),
                        posX = player.Position.X,
                        posY = player.Position.Y,
                        posZ = player.Position.Z,
                        played = player.GetData<int>(EntityData.PLAYER_PLAYED),
                        hunger = hunger,
                        thirst = thirst
                    };

                    NAPI.Task.Run(() =>
                    {
                        // Сохранаяем игрока в базе данных.
                        Database.Database.SaveCharacterById(character);
                    });
                }
            }
        }

    }
}
