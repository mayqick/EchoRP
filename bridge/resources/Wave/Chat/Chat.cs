using System.Linq;
using System;
using GTANetworkAPI;
using Echo.Global;
namespace Echo.Chat
{
    public class Chat : Script
    {
        public Chat()
        {
            NAPI.Server.SetGlobalServerChat(false);
        }
        public static void SendMessageToNearbyPlayers(Client player, string message, int type, float range, bool excludePlayer = false)
        {
            string secondMessage = string.Empty;
            float distanceGap = range / Constants.CHAT_RANGES;

            if (message.Length > Constants.CHAT_LENGTH)
            {
                // Нам нужно две линии для отображения сообщения
                secondMessage = message.Substring(Constants.CHAT_LENGTH, message.Length - Constants.CHAT_LENGTH);
                message = message.Remove(Constants.CHAT_LENGTH, secondMessage.Length);
            }
            
            foreach (Client target in NAPI.Pools.GetAllPlayers())
            {
                if (target.HasSharedData(EntityData.PLAYER_PLAYING) && player.Dimension == target.Dimension)
                {
                    if (player != target || (player == target && !excludePlayer))
                    {
                        float distance = player.Position.DistanceTo(target.Position);

                        if (distance <= range)
                        {
                            // Получаем цвет сообщения
                            string chatMessageColor = GetChatMessageColor(distance, distanceGap);
                            string oocMessageColor = GetOocMessageColor(distance, distanceGap);

                            switch (type)
                            {
                                case Constants.MESSAGE_TALK: // говорит
                                    target.SendChatMessage(secondMessage.Length > 0 ? chatMessageColor + player.GetSharedData(EntityData.PLAYER_NAME) + Messages.GEN_CHAT_SAY + message + "..." : chatMessageColor + player.GetSharedData(EntityData.PLAYER_NAME) + Messages.GEN_CHAT_SAY + message);
                                    if (secondMessage.Length > 0)
                                    {
                                        target.SendChatMessage(chatMessageColor + secondMessage);
                                    }
                                    break;
                                case Constants.MESSAGE_YELL: // кричит
                                    target.SendChatMessage(secondMessage.Length > 0 ? chatMessageColor + player.GetSharedData(EntityData.PLAYER_NAME) + Messages.GEN_CHAT_YELL + message + "..." : chatMessageColor + player.GetSharedData(EntityData.PLAYER_NAME) + Messages.GEN_CHAT_YELL + message + "!");
                                    if (secondMessage.Length > 0)
                                    {
                                        target.SendChatMessage(chatMessageColor + secondMessage + "!");
                                    }
                                    break;
                                case Constants.MESSAGE_WHISPER: // шепчет

                                    target.SendChatMessage(secondMessage.Length > 0 ? chatMessageColor + player.GetSharedData(EntityData.PLAYER_NAME) + Messages.GEN_CHAT_WHISPER + message + "..." : chatMessageColor + player.GetSharedData(EntityData.PLAYER_NAME) + Messages.GEN_CHAT_WHISPER + message);
                                    if (secondMessage.Length > 0)
                                    {
                                        target.SendChatMessage(chatMessageColor + secondMessage);
                                    }
                                    break;
                                case Constants.MESSAGE_ME:
                                    target.SendChatMessage(secondMessage.Length > 0 ? Constants.COLOR_CHAT_ME + player.GetSharedData(EntityData.PLAYER_NAME) + " " + message + "..." : Constants.COLOR_CHAT_ME + player.GetSharedData(EntityData.PLAYER_NAME) + " " + message);
                                    if (secondMessage.Length > 0)
                                    {
                                        target.SendChatMessage(Constants.COLOR_CHAT_ME + secondMessage);
                                    }
                                    break;
                                case Constants.MESSAGE_DO:
                                    target.SendChatMessage(secondMessage.Length > 0 ? Constants.COLOR_CHAT_DO + " " + message + "..." : Constants.COLOR_CHAT_DO + " " + message + $"({player.GetSharedData(EntityData.PLAYER_NAME)})");
                                    if (secondMessage.Length > 0)
                                    {
                                        target.SendChatMessage(Constants.COLOR_CHAT_DO + secondMessage + $" ({player.GetSharedData(EntityData.PLAYER_NAME)})");
                                    }
                                    break;
                                case Constants.MESSAGE_OOC:

                                    target.SendChatMessage(secondMessage.Length > 0 ? oocMessageColor + $"(([{player.Value}] " + player.GetSharedData(EntityData.PLAYER_NAME) + ": " + message + "..." : oocMessageColor + $"(([{player.Value}] " + player.GetSharedData(EntityData.PLAYER_NAME) + ": " + message + "))");
                                    if (secondMessage.Length > 0)
                                    {
                                        target.SendChatMessage(oocMessageColor + secondMessage + "))");
                                    }
                                    break;
                                case Constants.MESSAGE_TRY_TRUE:

                                    target.SendChatMessage(secondMessage.Length > 0 ? Constants.COLOR_CHAT_ME + player.GetSharedData(EntityData.PLAYER_NAME) + " " + message + "..." : Constants.COLOR_CHAT_ME + player.GetSharedData(EntityData.PLAYER_NAME) + " " + message + Constants.COLOR_TRY_POSITIVE + Messages.TRY_POSSITIVE_RESULT);
                                    if (secondMessage.Length > 0)
                                    {
                                        target.SendChatMessage(Constants.COLOR_CHAT_ME + secondMessage + Constants.COLOR_TRY_POSITIVE + Messages.TRY_POSSITIVE_RESULT);
                                    }
                                    break;
                                case Constants.MESSAGE_TRY_FALSE:
                                    target.SendChatMessage(secondMessage.Length > 0 ? Constants.COLOR_CHAT_ME + player.GetSharedData(EntityData.PLAYER_NAME) + " " + message + "..." : Constants.COLOR_CHAT_ME + player.GetSharedData(EntityData.PLAYER_NAME) + " " + message + Constants.COLOR_ERROR + Messages.TRY_NEGATIVE_RESULT);
                                    if (secondMessage.Length > 0)
                                    {
                                        target.SendChatMessage(Constants.COLOR_CHAT_ME + secondMessage + Constants.COLOR_ERROR + Messages.TRY_NEGATIVE_RESULT);
                                    }
                                    break;

                            }
                        }
                    }
                }
            }
        }

        private static string GetChatMessageColor(float distance, float distanceGap)
        {
            string color = null;
            if (distance < distanceGap)
            {
                color = Constants.COLOR_CHAT_CLOSE;
            }
            else if (distance < distanceGap * 2)
            {
                color = Constants.COLOR_CHAT_NEAR;
            }
            else if (distance < distanceGap * 3)
            {
                color = Constants.COLOR_CHAT_MEDIUM;
            }
            else if (distance < distanceGap * 4)
            {
                color = Constants.COLOR_CHAT_FAR;
            }
            else
            {
                color = Constants.COLOR_CHAT_LIMIT;
            }
            return color;
        }

        private static string GetOocMessageColor(float distance, float distanceGap)
        {
            string color = null;
            if (distance < distanceGap)
            {
                color = Constants.COLOR_OOC_CLOSE;
            }
            else if (distance < distanceGap * 2)
            {
                color = Constants.COLOR_OOC_NEAR;
            }
            else if (distance < distanceGap * 3)
            {
                color = Constants.COLOR_OOC_MEDIUM;
            }
            else if (distance < distanceGap * 4)
            {
                color = Constants.COLOR_OOC_FAR;
            }
            else
            {
                color = Constants.COLOR_OOC_LIMIT;
            }
            return color;
        }

        [RemoteEvent("ChatModeChanged")]
        public void OnChatModeChanged(Client player, string ChatMode)
        {
            player.SetData(EntityData.CHAT_MODE, ChatMode);
        }
        [RemoteEvent("ChatOpen")]
        public void OnChatOpen(Client player)
        {
            player.TriggerEvent("SetActiveChatMode", player.GetData(EntityData.CHAT_MODE));
        }
        [RemoteEvent("ChatModeTabChanged")]
        public void OnChatModeTabChanged(Client player)
        {
            if (player.GetData(EntityData.CHAT_MODE) == "Say") player.SetData(EntityData.CHAT_MODE, "Yell");
            else if (player.GetData(EntityData.CHAT_MODE) == "Yell") player.SetData(EntityData.CHAT_MODE, "Whisper");
            else if (player.GetData(EntityData.CHAT_MODE) == "Whisper") player.SetData(EntityData.CHAT_MODE, "Ooc");
            else if (player.GetData(EntityData.CHAT_MODE) == "Ooc") player.SetData(EntityData.CHAT_MODE, "me");
            else if (player.GetData(EntityData.CHAT_MODE) == "me") player.SetData(EntityData.CHAT_MODE, "do");
            else if (player.GetData(EntityData.CHAT_MODE) == "do") player.SetData(EntityData.CHAT_MODE, "try");
            else if (player.GetData(EntityData.CHAT_MODE) == "try") player.SetData(EntityData.CHAT_MODE, "Say");

            player.TriggerEvent("SetActiveChatMode", player.GetData(EntityData.CHAT_MODE));
        }

        [ServerEvent(Event.ChatMessage)]
        public void OnChatMessage(Client player, string message)
        {
            if (player.HasSharedData(EntityData.PLAYER_PLAYING) == false)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_CANT_CHAT);
            }
            else
            {
                if (!player.HasData(EntityData.CHAT_MODE))
                {
                    player.SetData(EntityData.CHAT_MODE, "Say");
                    SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_TALK, player.Dimension > 0 ? 7.5f : 10.0f);
                }
                else
                {
                    if (player.GetData(EntityData.CHAT_MODE) == "Say") SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_TALK, player.Dimension > 0 ? 7.5f : 10.0f);
                    else if (player.GetData(EntityData.CHAT_MODE) == "Yell") SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_YELL, 45.0f);
                    else if (player.GetData(EntityData.CHAT_MODE) == "Whisper") SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_WHISPER, 3.0f);
                    else if (player.GetData(EntityData.CHAT_MODE) == "Ooc") SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_OOC, player.Dimension > 0 ? 5.0f : 10.0f);
                    else if (player.GetData(EntityData.CHAT_MODE) == "me") SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_ME, player.Dimension > 0 ? 7.5f : 20.0f);
                    else if (player.GetData(EntityData.CHAT_MODE) == "do") SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_DO, player.Dimension > 0 ? 7.5f : 20.0f);
                    else if (player.GetData(EntityData.CHAT_MODE) == "try")
                    {
                        Random random = new Random();
                        int messageType = random.Next(0, 2) > 0 ? Constants.MESSAGE_TRY_TRUE : Constants.MESSAGE_TRY_FALSE;
                        SendMessageToNearbyPlayers(player, message, messageType, player.Dimension > 0 ? 7.5f : 20.0f);
                    }
                }
            }
        }

        [Command(Messages.COM_YELL, Messages.GEN_YELL_COMMAND, GreedyArg = true)]
        public void YellCMD(Client player, string message)
        {
            SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_YELL, 45.0f);
        }

        [Command(Messages.COM_WHISPER, Messages.GEN_WHISPER_COMMAND, GreedyArg = true)]
        public void WhisperCMD(Client player, string message)
        {
            SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_WHISPER, 3.0f);
        }

        [Command(Messages.COM_ME, Messages.GEN_ME_COMMAND, GreedyArg = true)]
        public void MeCMD(Client player, string message)
        {
            SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_ME, player.Dimension > 0 ? 7.5f : 20.0f);
        }

        [Command(Messages.COM_DO, Messages.GEN_DO_COMMAND, GreedyArg = true)]
        public void DoCMD(Client player, string message)
        {
            SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_DO, player.Dimension > 0 ? 7.5f : 20.0f);
        }

        [Command(Messages.COM_OOC, Messages.GEN_OOC_COMMAND, GreedyArg = true)]
        public void OocCMD(Client player, string message)
        {
            SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_OOC, player.Dimension > 0 ? 5.0f : 10.0f);
        }
        [Command(Messages.COM_TRY, Messages.GEN_TRY_COMMAND, GreedyArg = true)]
        public void SuCommand(Client player, string message)
        {
            Random random = new Random();
            int messageType = random.Next(0, 2) > 0 ? Constants.MESSAGE_TRY_TRUE : Constants.MESSAGE_TRY_FALSE;
            SendMessageToNearbyPlayers(player, message, messageType, player.Dimension > 0 ? 7.5f : 20.0f);

        }
    }

}