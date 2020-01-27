-- phpMyAdmin SQL Dump
-- version 3.5.1
-- http://www.phpmyadmin.net
--
-- Хост: 127.0.0.1
-- Время создания: Янв 28 2020 г., 00:46
-- Версия сервера: 5.5.25
-- Версия PHP: 5.3.13

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- База данных: `echorp`
--

-- --------------------------------------------------------

--
-- Структура таблицы `accounts`
--

CREATE TABLE IF NOT EXISTS `accounts` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `license` varchar(40) NOT NULL,
  `socialClub` varchar(32) NOT NULL,
  `mail` varchar(64) NOT NULL,
  `status` int(2) NOT NULL DEFAULT '1',
  `donate` int(11) NOT NULL,
  `promo` varchar(32) NOT NULL,
  `regIp` varchar(16) NOT NULL,
  `lastIp` varchar(16) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=36 ;

-- --------------------------------------------------------

--
-- Структура таблицы `character_list`
--

CREATE TABLE IF NOT EXISTS `character_list` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `accountId` varchar(32) NOT NULL,
  `characterName` varchar(32) NOT NULL,
  `posX` float NOT NULL DEFAULT '201',
  `posY` float NOT NULL DEFAULT '-932.094',
  `posZ` float NOT NULL DEFAULT '30.6868',
  `rotation` float NOT NULL DEFAULT '0',
  `age` int(5) NOT NULL,
  `isMale` tinyint(1) NOT NULL,
  `health` int(5) NOT NULL DEFAULT '100',
  `armor` int(5) NOT NULL DEFAULT '0',
  `money` int(11) NOT NULL DEFAULT '0',
  `bank` int(11) NOT NULL DEFAULT '0',
  `adminRank` int(2) NOT NULL DEFAULT '0',
  `xp` int(11) NOT NULL DEFAULT '0',
  `lvl` int(11) NOT NULL DEFAULT '1',
  `played` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=15 ;

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
  `beardOpacity` int(11) NOT NULL,
  `chestModel` int(10) NOT NULL,
  `chestColor` int(10) NOT NULL,
  `blemishesModel` int(10) NOT NULL,
  `ageingModel` int(10) NOT NULL,
  `ageingOpacity` int(11) NOT NULL,
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
  `eyerbrowsOpacity` int(11) NOT NULL,
  `makeupModel` int(11) NOT NULL,
  `blushModel` int(11) NOT NULL,
  `blushColor` int(11) NOT NULL,
  `lipstickModel` int(11) NOT NULL,
  `lipstickColor` int(11) NOT NULL,
  PRIMARY KEY (`characterId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
