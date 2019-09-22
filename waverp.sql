-- phpMyAdmin SQL Dump
-- version 3.5.1
-- http://www.phpmyadmin.net
--
-- Хост: 127.0.0.1
-- Время создания: Авг 10 2019 г., 16:19
-- Версия сервера: 5.5.25
-- Версия PHP: 5.3.13

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- База данных: `waverp`
--

-- --------------------------------------------------------

--
-- Структура таблицы `accounts`
--

CREATE TABLE IF NOT EXISTS `accounts` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nickName` varchar(32) NOT NULL,
  `socialName` varchar(32) NOT NULL,
  `password` varchar(256) NOT NULL,
  `mail` varchar(64) NOT NULL,
  `status` int(2) NOT NULL DEFAULT '1',
  `donate` int(11) NOT NULL,
  `slot_3` tinyint(1) NOT NULL DEFAULT '0',
  `slot_4` tinyint(1) NOT NULL DEFAULT '0',
  `promo` varchar(32) NOT NULL,
  `regIp` varchar(16) NOT NULL,
  `lastIp` varchar(16) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=11 ;

--
-- Дамп данных таблицы `accounts`
--

INSERT INTO `accounts` (`id`, `nickName`, `socialName`, `password`, `mail`, `status`, `donate`, `slot_3`, `slot_4`, `promo`, `regIp`, `lastIp`) VALUES
(1, 'Oniel', 'fwf2waf', '', '', 1, 0, 0, 0, '', '127.0.0.1', '127.0.0.1'),
(2, 'test', 'Test2', '0d1ea4c256cd50a2a7ccbfd22b3d9959f6fd30bd840b9ff3c7c65ee4e21df06d', '', 1, 0, 0, 0, '', '', ''),
(3, 'Oniel', 'Test', '3cc849279ba298b587a34cabaeffc5ecb3a044bbf97c516fab7ede9d1af77cfa', '', 1, 0, 0, 0, '127.0.0.1', '2222', ''),
(4, 'ForaN', 'THEJokkz', '3cc849279ba298b587a34cabaeffc5ecb3a044bbf97c516fab7ede9d1af77cfa', '', 1, 0, 0, 0, '127.0.0.1', '2222', ''),
(5, 'ForaN2', 'THEJokkz', '96cae35ce8a9b0244178bf28e4966c2ce1b8385723a96a6b838858cdd6ca0a1e', '', 1, 0, 0, 0, '2222', '127.0.0.1', ''),
(6, 'ForaN23', 'THEJokkz', '96cae35ce8a9b0244178bf28e4966c2ce1b8385723a96a6b838858cdd6ca0a1e', '', 1, 0, 0, 0, '222555', '127.0.0.1', ''),
(7, 'Accounttest', 'THEJokkz', '3cc849279ba298b587a34cabaeffc5ecb3a044bbf97c516fab7ede9d1af77cfa', '', 1, 0, 0, 0, '2222', '127.0.0.1', ''),
(8, 'Account', 'THEJokkz', '3cc849279ba298b587a34cabaeffc5ecb3a044bbf97c516fab7ede9d1af77cfa', '', 1, 0, 0, 0, '1122', '127.0.0.1', ''),
(9, 'FuckIt', 'THEJokkz', '3cc849279ba298b587a34cabaeffc5ecb3a044bbf97c516fab7ede9d1af77cfa', '', 1, 0, 0, 0, '', '127.0.0.1', ''),
(10, 'FuckIt22', 'THEJokkz', '96cae35ce8a9b0244178bf28e4966c2ce1b8385723a96a6b838858cdd6ca0a1e', '', 1, 100, 0, 0, '', '127.0.0.1', '127.0.0.1');

-- --------------------------------------------------------

--
-- Структура таблицы `characterlist`
--

CREATE TABLE IF NOT EXISTS `characterlist` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nickName` varchar(32) NOT NULL,
  `characterName` varchar(32) NOT NULL,
  `posX` float NOT NULL DEFAULT '201',
  `posY` float NOT NULL DEFAULT '-932.094',
  `posZ` float NOT NULL DEFAULT '30.6868',
  `rotation` float NOT NULL DEFAULT '0',
  `age` int(5) NOT NULL,
  `sex` int(2) NOT NULL,
  `health` int(5) NOT NULL DEFAULT '100',
  `armor` int(5) NOT NULL DEFAULT '0',
  `money` int(11) NOT NULL DEFAULT '0',
  `bank` int(11) NOT NULL DEFAULT '0',
  `adminRank` int(2) NOT NULL DEFAULT '0',
  `xp` int(11) NOT NULL DEFAULT '0',
  `lvl` int(11) NOT NULL DEFAULT '1',
  `played` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=19 ;

--
-- Дамп данных таблицы `characterlist`
--

INSERT INTO `characterlist` (`id`, `nickName`, `characterName`, `posX`, `posY`, `posZ`, `rotation`, `age`, `sex`, `health`, `armor`, `money`, `bank`, `adminRank`, `xp`, `lvl`, `played`) VALUES
(1, 'ForaN', '', 201, -932.094, 30.6868, 0, 0, 0, 0, 0, 110, 0, 0, 0, 1, 0),
(2, 'ForaN', '', 201, -932.094, 30.6868, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0),
(3, 'FuckIt', 'Sergey_Dmitriev', 201, -932.094, 30.6868, 0, 24, 1, 0, 0, 0, 0, 0, 0, 1, 0),
(7, 'FuckIt', 'Sasha_Grey', 201, -932.094, 30.6868, 0, 25, 1, 0, 0, 0, 0, 6, 0, 1, 0),
(11, 'FuckIt', 'Agaga_Wawa', 201, -932.094, 30.6868, 0, 23, 1, 0, 0, 0, 0, 0, 0, 1, 0),
(13, 'FuckIt22', 'Andrey_Pavlov', -427.519, 1116.45, 326.783, 0, 25, 1, 0, 0, 0, 0, 6, 3, 1, 27),
(14, 'FuckIt22', 'Charles_Williamsoni', 201, -932.094, 30.6868, 0, 24, 0, 100, 0, 0, 0, 0, 0, 1, 0),
(17, 'FuckIt22', 'Dmitriy_Medvedev', 201, -932.094, 30.6868, 0, 26, 0, 100, 0, 0, 0, 0, 0, 1, 0),
(18, 'FuckIt22', 'Vasya_Pupkin', -428.858, 1142.77, 325.326, 0, 28, 0, 100, 0, 0, 0, 7, 0, 1, 22);

-- --------------------------------------------------------

--
-- Структура таблицы `clothes`
--

CREATE TABLE IF NOT EXISTS `clothes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `characterid` int(11) NOT NULL DEFAULT '0',
  `slot` int(11) NOT NULL DEFAULT '0',
  `drawable` int(11) NOT NULL DEFAULT '0',
  `texture` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=13 ;

--
-- Дамп данных таблицы `clothes`
--

INSERT INTO `clothes` (`id`, `characterid`, `slot`, `drawable`, `texture`) VALUES
(7, 17, 6, 1, 0),
(8, 17, 6, 1, 0),
(9, 17, 6, 1, 0),
(10, 18, 11, 7, 0),
(11, 18, 4, 1, 0),
(12, 18, 6, 27, 0);

-- --------------------------------------------------------

--
-- Структура таблицы `inventory_items`
--

CREATE TABLE IF NOT EXISTS `inventory_items` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(128) NOT NULL,
  `description` text NOT NULL,
  `weight` float NOT NULL,
  `model` varchar(128) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Дамп данных таблицы `inventory_items`
--

INSERT INTO `inventory_items` (`id`, `name`, `description`, `weight`, `model`) VALUES
(1, 'Beretta 92', 'Огнестрельное оружие. Страшная вещь.', 2, 'sns_pistol_mk2'),
(2, 'М16', 'Винтовка революционеров.', 6, 'carabine_rifle');

-- --------------------------------------------------------

--
-- Структура таблицы `inventory_players`
--

CREATE TABLE IF NOT EXISTS `inventory_players` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `characterid` int(11) NOT NULL,
  `itemid` varchar(11) NOT NULL,
  `slot` varchar(11) NOT NULL,
  `count` varchar(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Дамп данных таблицы `inventory_players`
--

INSERT INTO `inventory_players` (`id`, `characterid`, `itemid`, `slot`, `count`) VALUES
(1, 13, 'gun', 'cell10', '1'),
(2, 13, 'gun', 'cell9', '5');

-- --------------------------------------------------------

--
-- Структура таблицы `skins`
--

CREATE TABLE IF NOT EXISTS `skins` (
  `characterId` int(11) NOT NULL,
  `firstHeadShape` int(11) NOT NULL,
  `secondHeadShape` int(11) NOT NULL,
  `firstSkinTone` int(11) NOT NULL,
  `secondSkinTone` int(11) NOT NULL,
  `headMix` float NOT NULL,
  `skinMix` float NOT NULL,
  `hairModel` int(10) NOT NULL,
  `firstHairColor` int(10) NOT NULL,
  `secondHairColor` int(10) NOT NULL,
  `beardModel` int(10) NOT NULL,
  `beardColor` int(10) NOT NULL,
  `chestModel` int(10) NOT NULL,
  `chestColor` int(10) NOT NULL,
  `blemishesModel` int(10) NOT NULL,
  `ageingModel` int(10) NOT NULL,
  `complexionModel` int(10) NOT NULL,
  `sundamageModel` int(10) NOT NULL,
  `frecklesModel` int(10) NOT NULL,
  `noseWidth` float NOT NULL,
  `noseHeight` float NOT NULL,
  `noseLength` float NOT NULL,
  `noseBridge` float NOT NULL,
  `noseTip` float NOT NULL,
  `noseShift` float NOT NULL,
  `browHeight` float NOT NULL,
  `browWidth` float NOT NULL,
  `cheekboneHeight` float NOT NULL,
  `cheekboneWidth` float NOT NULL,
  `cheeksWidth` float NOT NULL,
  `eyes` float NOT NULL,
  `lips` float NOT NULL,
  `jawWidth` float NOT NULL,
  `jawHeight` float NOT NULL,
  `chinLength` float NOT NULL,
  `chinPosition` float NOT NULL,
  `chinWidth` float NOT NULL,
  `chinShape` float NOT NULL,
  `neckWidth` float NOT NULL,
  `eyesColor` int(11) NOT NULL,
  `eyebrowsModel` int(11) NOT NULL,
  `eyebrowsColor` int(11) NOT NULL,
  `makeupModel` int(11) NOT NULL,
  `blushModel` int(11) NOT NULL,
  `blushColor` int(11) NOT NULL,
  `lipstickModel` int(11) NOT NULL,
  `lipstickColor` int(11) NOT NULL,
  PRIMARY KEY (`characterId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `skins`
--

INSERT INTO `skins` (`characterId`, `firstHeadShape`, `secondHeadShape`, `firstSkinTone`, `secondSkinTone`, `headMix`, `skinMix`, `hairModel`, `firstHairColor`, `secondHairColor`, `beardModel`, `beardColor`, `chestModel`, `chestColor`, `blemishesModel`, `ageingModel`, `complexionModel`, `sundamageModel`, `frecklesModel`, `noseWidth`, `noseHeight`, `noseLength`, `noseBridge`, `noseTip`, `noseShift`, `browHeight`, `browWidth`, `cheekboneHeight`, `cheekboneWidth`, `cheeksWidth`, `eyes`, `lips`, `jawWidth`, `jawHeight`, `chinLength`, `chinPosition`, `chinWidth`, `chinShape`, `neckWidth`, `eyesColor`, `eyebrowsModel`, `eyebrowsColor`, `makeupModel`, `blushModel`, `blushColor`, `lipstickModel`, `lipstickColor`) VALUES
(1, 3, 0, 4, 3, 0.5, 0.52, 4, 3, 3, 255, 0, 255, 0, 255, 255, 255, 255, 255, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 3, 2, 255, 255, 0, 255, 0),
(12, 0, 0, 0, 0, 0.5, 0.5, 51, 69, 98, 255, 0, 255, 20, 255, 255, 255, 255, 255, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 255, 0, 255, 255, 0, 255, 0),
(13, 26, 31, 4, 12, 0.6, 0.6, 10, 0, 0, 255, 0, 255, 0, 255, 255, 7, 3, 255, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 13, 22, 31, 3, 39, 5, 100),
(14, 0, 0, 0, 0, 0.5, 0.5, 0, 0, 0, 255, 0, 255, 0, 255, 255, 255, 255, 255, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 255, 0, 255, 255, 0, 255, 0),
(15, 0, 0, 0, 0, 0.5, 0.5, 0, 0, 0, 255, 0, 255, 0, 255, 255, 255, 255, 255, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 255, 0, 255, 255, 0, 255, 0),
(16, 0, 0, 0, 0, 0.5, 0.5, 0, 0, 0, 255, 0, 255, 0, 255, 255, 255, 255, 255, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 255, 0, 255, 255, 0, 255, 0),
(17, 0, 0, 0, 0, 0.5, 0.5, 0, 0, 0, 255, 0, 255, 0, 255, 255, 255, 255, 255, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 255, 0, 255, 255, 0, 255, 0),
(18, 0, 0, 0, 0, 0.5, 0.5, 0, 0, 0, 255, 0, 255, 0, 255, 255, 255, 255, 255, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 255, 0, 255, 255, 0, 255, 0);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
