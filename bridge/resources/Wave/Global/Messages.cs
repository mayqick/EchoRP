using System;
using System.Collections.Generic;
using System.Text;

namespace Echo.Global
{
    public class Messages
    {
        // Сообщения
        public const string GEN_CHAT_SAY = " говорит: ";
        public const string GEN_CHAT_YELL = " кричит: ";
        public const string GEN_CHAT_WHISPER = " шепчет: ";
        public const string ERR_PLAYER_CANT_CHAT = "Вы не можете отправлять сообщения!";
        public const string TRY_POSSITIVE_RESULT = " [Удачно]";
        public const string TRY_NEGATIVE_RESULT = " [Неудачно]";
        public const string SECURITY_SELF_ERROR = "Ошибка безопасности! Вы не можете производить такие манипуляции с собой.";
        public const string PAYDAY_ERROR = "Вы должны отыграть 20 или более минут, чтобы получить PayDay!";
        public const string PLAYER_HUNGER = "Вы проголодались, вам нужно что-нибудь съесть!";
        public const string PLAYER_VERY_HUNGER = "Вы очень сильно проголодались, вам нужно что-нибудь съесть!";

        public const string PLAYER_THIRST = "Вы хотите пить!";
        public const string PLAYER_VERY_THIRST = "Вас мучает жажда, вам срочно нужно попить!";

        public const string ADMIN_ASSIGNED = "[A] {0} назначил {1} на пост администратора. Поздравляем!";
        public const string ADMIN_LVL_CHANGED = "[A] {0} изменил {1} администартивный уровень на {2}!";
        public const string ADMIN_SET_HP = "[A] Администратор {0} установил {1}HP игроку {2}.";
        public const string ADMIN_VEH_CREATED = "[A] Администратор {0} заспавнил автомобиль {1}.";
        public const string ADMIN_SETWEAPON = "[A] Администратор {0} выдал себе оружие {1}.";
        public const string ADMIN_SET_MODEL = "[A] Администратор {0} установил себе модель {1}.";
        public const string ADMIN_TELEPORTED = "[A] Вы успешно телепортировались к {0}.";

        // Command names
        // Админка:
        public const string COM_MAKEADMIN = "makeadmin";
        public const string COM_GOTO = "goto";
        public const string COM_VEH = "veh";
        public const string COM_WEAPON = "setweapon";
        public const string COM_MODEL = "setmodel";
        public const string COM_SETHP = "sethp";

        // Чат:
        public const string COM_YELL = "s";
        public const string COM_WHISPER = "w";
        public const string COM_ME = "me";
        public const string COM_DO = "do";
        public const string COM_OOC = "ooc";
        public const string COM_TRY = "try";

        // Админка:
        public const string GEN_GOTO_COMMAND = "ИСПОЛЬЗУЙТЕ: /goto [ник]";
        public const string GEN_MAKEADMIN = "ИСПОЛЬЗУЙТЕ: /makeadmin [ник] [уровень]";
        public const string GEN_VEH_COMMAND = "ИСПОЛЬЗУЙТЕ: /veh [модель]";
        public const string GEN_WEAPON_COMMAND = "ИСПОЛЬЗУЙТЕ: /setweapon [название оружия]";
        public const string GEN_MODEL_COMMAND = "ИСПОЛЬЗУЙТЕ: /model [модель]";
        public const string GEN_SETHP_COMMAND = "ИСПОЛЬЗУЙТЕ: /sethp [игрок] [кол-во жизней]";
        // Чат:
        public const string GEN_ME_COMMAND = "ИСПОЛЬЗУЙТЕ: /me [действие]";
        public const string GEN_DO_COMMAND = "ИСПОЛЬЗУЙТЕ: /do [сообщение]";
        public const string GEN_OOC_COMMAND = "ИСПОЛЬЗУЙТЕ: /ooc [сообщение]";
        public const string GEN_WHISPER_COMMAND = "ИСПОЛЬЗУЙТЕ: /w [сообщение]";
        public const string GEN_YELL_COMMAND = "ИСПОЛЬЗУЙТЕ: /s [сообщение]";
        public const string GEN_TRY_COMMAND = "ИСПОЛЬЗУЙТЕ: /try [действие]";
    }
}
