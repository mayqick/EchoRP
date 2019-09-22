using System;
using System.Collections.Generic;
using System.Text;

namespace Wave.Global
{
    struct Constants
    {
        // colors for chat
        public const string COLOR_CHAT_ME = "!{#C2A2DA}";
        public const string COLOR_CHAT_DO = "!{#4682b4}";
        public const string COLOR_SUCCESS = "!{#33B517}";
        public const string COLOR_ERROR   = "!{#A80707}";
        public const string COLOR_INFO    = "!{#FDFE8B}";
        public const string COLOR_HELP    = "!{#FFFFFF}";
        public const string COLOR_NEWS    = "!{#805CC9}";
        public const string COLOR_CHAT_CLOSE = "!{#E6E6E6}";
        public const string COLOR_CHAT_NEAR = "!{#C8C8C8}";
        public const string COLOR_CHAT_MEDIUM = "!{#AAAAAA}";
        public const string COLOR_CHAT_FAR = "!{#8C8C8C}";
        public const string COLOR_CHAT_LIMIT = "!{#6E6E6E}";
        public const string COLOR_CHAT_FACTION = "!{#27F7C8}";
        public const string COLOR_CHAT_PHONE = "!{#27F7C8}";
        public const string COLOR_OOC_CLOSE = "!{#4C9E9E}";
        public const string COLOR_OOC_NEAR = "!{#438C8C}";
        public const string COLOR_OOC_MEDIUM = "!{#2E8787}";
        public const string COLOR_OOC_FAR = "!{#187373}";
        public const string COLOR_OOC_LIMIT = "!{#0A5555}";
        public const string COLOR_ADMIN_CHAT = "!{#99CC00FF}";
        public const string COLOR_ADMIN_INFO = "!{#00FCFF}";
        public const string COLOR_ADMIN_NEWS = "!{#F93131}";
        public const string COLOR_ADMIN_MP = "!{#F93131}";
        public const string COLOR_TRY_POSITIVE = "!{#1d8c45}";
        public const string COLOR_RADIO = "!{#1598C4}";
        public const string COLOR_RADIO_POLICE = "!{#4169E1}";
        public const string COLOR_RADIO_EMERGENCY = "!{#FF9F0F}";
        // Chat message types
        public const int MESSAGE_TALK = 0;
        public const int MESSAGE_YELL = 1;
        public const int MESSAGE_WHISPER = 2;
        public const int MESSAGE_ME = 3;
        public const int MESSAGE_DO = 4;
        public const int MESSAGE_OOC = 5;
        public const int MESSAGE_TRY_TRUE = 6;
        public const int MESSAGE_TRY_FALSE = 7;
        public const int MESSAGE_NEWS = 8;
        public const int MESSAGE_PHONE = 9;
        public const int MESSAGE_DISCONNECT = 10;
        public const int MESSAGE_MEGAPHONE = 11;
        public const int MESSAGE_RADIO = 12;

        public const int MAX_HEAD_OVERLAYS = 11;
        // Sex
        public const int SEX_NONE = -1;
        public const int SEX_MALE = 0;
        public const int SEX_FEMALE = 1;

        // connectionString for MySqlConnection
        public const string connectionString = "Server=localhost; Database=waverp; Uid=root; Pwd=";

        // Chat
        public const int CHAT_LENGTH = 85;
        public const int CHAT_RANGES = 5;
        // level system
        public const int XP_START = 6; // for 2 level
        public const int LVL_NEXT = 2; // iterator for next level; lvl = lvl_start + lvl_next * (lvl_next - 2)
    }
}
