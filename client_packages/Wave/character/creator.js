const PlayerModel = require('./Wave/model/playerModel.js');
let playerData;

let sceneryCamera;
mp.events.add('createNewChar', () => {
    mp.events.callRemote('charCreate');
});
mp.events.add('showCreatorPage', () => {
    sceneryCamera = mp.cameras.new('default', new mp.Vector3(152.6008, -1003.25, -98), new mp.Vector3(-20.0, 0.0, 0.0), 90);
    //sceneryCamera.pointAtCoord(-813.5496, 174.5891, 76.74077); 
    sceneryCamera.setActive(true);
    mp.game.cam.renderScriptCams(true, false, 0, true, false);
    mp.events.call('destroyBrowser');

    playerData = new PlayerModel();
	applyPlayerModelChanges();

    mp.events.callRemote('setCharacterIntoCreator');
    mp.game.ui.displayRadar(false);
	mp.game.ui.displayHud(false);
	mp.gui.chat.activate(false);
	mp.gui.chat.show(false);

    mp.events.call('createBrowser', ['package://statics/html/character_creator.html']);
});
mp.events.add('cameraPointTo', (arg) => {
    let id = arg;
    switch (id) {
        case 1:
            sceneryCamera.setCoord(152.3708, -1001.75, -98.45);
            break;
        case 2:
            sceneryCamera.setCoord(152.6008, -1003.25, -98);
            break;
    }
});
mp.events.add('cef_setGender', (sex) => {
    playerData.sex = sex;

	// Меняем скин
	mp.players.local.model = sex === 0 ? mp.game.joaat('mp_m_freemode_01') : mp.game.joaat('mp_f_freemode_01');
	applyPlayerModelChanges();

});
mp.events.add('setChanged_JSON', (shape) => {
    let data = JSON.parse(shape);

    playerData.firstHeadShape = parseInt(data[0])
    playerData.secondHeadShape = parseInt(data[1])
    playerData.firstSkinTone = parseInt(data[2])
    playerData.secondSkinTone = parseInt(data[3])
    playerData.headMix = parseFloat(data[4])
    playerData.skinMix = parseFloat(data[5])
   
    applyPlayerModelChanges();
});
mp.events.add('setFace', (face) => {
    let data = JSON.parse(face);
    parseInt(data[0]) === 0 ? playerData.blemishesModel = 255 : playerData.blemishesModel = parseInt(data[0]);
    parseInt(data[1]) === 0 ? playerData.eyebrowsModel = 255 : playerData.eyebrowsModel = parseInt(data[1]);
    playerData.eyebrowsColor = parseInt(data[2]);
    parseInt(data[3]) === 0 ? playerData.ageingModel = 255 : playerData.ageingModel = parseInt(data[3]);
    parseInt(data[4]) === 0 ? playerData.makeupModel = 255 : playerData.makeupModel = parseInt(data[4]);
    parseInt(data[5]) === 0 ? playerData.blushModel = 255 : playerData.blushModel = parseInt(data[5]);
    playerData.blushColor = parseInt(data[6]);
    parseInt(data[7]) === 0 ? playerData.complexionModel = 255 : playerData.complexionModel = parseInt(data[7]);
    parseInt(data[8]) === 0 ? playerData.sundamageModel = 255 : playerData.sundamageModel = parseInt(data[8]);
    parseInt(data[9]) === 0 ? playerData.lipstickModel = 255 : playerData.lipstickModel = parseInt(data[9]);
    playerData.lipstickColor = parseInt(data[10]);
    parseInt(data[11]) === 0 ? playerData.frecklesModel = 255 : playerData.frecklesModel = parseInt(data[11]);
    parseInt(data[12]) === 0 ? playerData.chestModel = 255 : playerData.chestModel = parseInt(data[12]);
    playerData.chestColor = parseInt(data[13]);

    applyPlayerModelChanges();
});
mp.events.add('setHair', (hair) => {
    let data = JSON.parse(hair);

    playerData.hairModel = parseInt(data[0]);
    playerData.firstHairColor = parseInt(data[1]);
    playerData.secondHairColor = parseInt(data[2]);

    parseInt(data[3]) === 0 ? playerData.beardModel = 255 : playerData.beardModel = parseInt(data[3]);
    playerData.beardColor = parseInt(data[4]);

    applyPlayerModelChanges();

});
mp.events.add('setTrueFace', (trueFace) => {

    let set = JSON.parse(trueFace);
    playerData.neckWidth = parseFloat(set[0]);
    playerData.noseHeight = parseFloat(set[1]);
    playerData.noseLength = parseFloat(set[2]);
    playerData.noseBridge = parseFloat(set[3]);
    playerData.noseTip = parseFloat(set[4]);
    playerData.noseShift = parseFloat(set[5]);
    playerData.browHeight = parseFloat(set[6]);
    playerData.browWidth = parseFloat(set[7]);
    playerData.cheekboneHeight = parseFloat(set[8]);
    playerData.cheekboneWidth = parseFloat(set[9]);
    playerData.cheeksWidth = parseFloat(set[10]);
    playerData.eyes = parseFloat(set[11]);
    playerData.eyesColor = parseInt(set[12]);
    playerData.lips = parseFloat(set[13]);
    playerData.jawWidth = parseFloat(set[14]);
    playerData.jawHeight = parseFloat(set[15]);
    playerData.chinLength = parseFloat(set[16]);
    playerData.chinPosition = parseFloat(set[17]);
    playerData.chinWidth = parseFloat(set[18]);
    playerData.chinShape = parseFloat(set[19]);
    playerData.neckWidth = parseFloat(set[20]);

    applyPlayerModelChanges();
});
mp.events.add('setClothes', (clothes) => {
    mp.events.callRemote('setClothes', clothes);
});
mp.events.add('saveCharacter', (name, surname, gender, age, clothes) => {
    
	mp.game.ui.displayRadar(true);
	mp.game.ui.displayHud(true);
	mp.gui.chat.activate(true);
	mp.gui.chat.show(true);
    
    sceneryCamera.destroy(true);
    mp.game.cam.renderScriptCams(false, false, 0, true, false);
    mp.events.call('destroyBrowser');
    let playerInfo = [
        playerData.firstHeadShape,
        playerData.secondHeadShape,
        playerData.firstSkinTone,
        playerData.secondSkinTone,
        playerData.headMix,
        playerData.skinMix,
        playerData.hairModel,
        playerData.firstHairColor,
        playerData.secondHairColor,
        playerData.beardModel,
        playerData.beardColor,
        playerData.chestModel,
        playerData.chestColor,
        playerData.blemishesModel,
        playerData.ageingModel,
        playerData.complexionModel,
        playerData.sundamageModel,
        playerData.frecklesModel,
        playerData.eyesColor,
        playerData.eyebrowsModel,
        playerData.eyebrowsColor,
        playerData.makeupModel,
        playerData.blushModel,
        playerData.blushColor,
        playerData.lipstickModel,
        playerData.lipstickColor,
        playerData.noseWidth,
        playerData.noseHeight,
        playerData.noseLength,
        playerData.noseBridge,
        playerData.noseTip,
        playerData.noseShift,
        playerData.browHeight,
        playerData.browWidth,
        playerData.cheekboneHeight,
        playerData.cheekboneWidth,
        playerData.eyes,
        playerData.lips,
        playerData.jawWidth,
        playerData.jawHeight,
        playerData.chinLength,
        playerData.chinPosition,
        playerData.chinWidth,
        playerData.chinShape,
        playerData.neckWidth,
        playerData.noseWidth
    ]
    let playerJsonInfo = JSON.stringify(playerInfo);
    mp.events.callRemote('saveCustomization', playerJsonInfo, name, surname, gender, age, clothes);
});

function applyPlayerModelChanges() {
    // Получение текущего игрока
	let player = mp.players.local;

    // Изменения для текущего игрока
    player.setHeadBlendData(playerData.firstHeadShape, playerData.secondHeadShape, 0, playerData.firstSkinTone, playerData.secondSkinTone, 0, playerData.headMix, playerData.skinMix, 0, false);
	player.setComponentVariation(2, playerData.hairModel, 0, 0);
	player.setHairColor(playerData.firstHairColor, playerData.secondHairColor);
	player.setEyeColor(playerData.eyesColor);
	player.setHeadOverlay(1, playerData.beardModel, 1.0, playerData.beardColor, 0);
	player.setHeadOverlay(10, playerData.chestModel, 1.0, playerData.chestColor, 0);
	player.setHeadOverlay(2, playerData.eyebrowsModel, 1.0, playerData.eyebrowsColor, 0);
	player.setHeadOverlay(5, playerData.blushModel, 1.0, playerData.blushColor, 0);
	player.setHeadOverlay(8, playerData.lipstickModel, 1.0, playerData.lipstickColor, 0);
	player.setHeadOverlay(0, playerData.blemishesModel, 1.0, 0, 0);
	player.setHeadOverlay(3, playerData.ageingModel, 1.0, 0, 0);
	player.setHeadOverlay(6, playerData.complexionModel, 1.0, 0, 0);
	player.setHeadOverlay(7, playerData.sundamageModel, 1.0, 0, 0);
	player.setHeadOverlay(9, playerData.frecklesModel, 1.0, 0, 0);
	player.setHeadOverlay(4, playerData.makeupModel, 1.0, 0, 0);
	player.setFaceFeature(0, playerData.noseWidth);
	player.setFaceFeature(1, playerData.noseHeight);
	player.setFaceFeature(2, playerData.noseLength);
	player.setFaceFeature(3, playerData.noseBridge);
	player.setFaceFeature(4, playerData.noseTip);
	player.setFaceFeature(5, playerData.noseShift);
	player.setFaceFeature(6, playerData.browHeight);
	player.setFaceFeature(7, playerData.browWidth);
	player.setFaceFeature(8, playerData.cheekboneHeight);
	player.setFaceFeature(9, playerData.cheekboneWidth);
	player.setFaceFeature(10, playerData.cheeksWidth);
	player.setFaceFeature(11, playerData.eyes);
	player.setFaceFeature(12, playerData.lips);
	player.setFaceFeature(13, playerData.jawWidth);
	player.setFaceFeature(14, playerData.jawHeight);
	player.setFaceFeature(15, playerData.chinLength);
	player.setFaceFeature(16, playerData.chinPosition);
	player.setFaceFeature(17, playerData.chinWidth);
	player.setFaceFeature(18, playerData.chinShape);
	player.setFaceFeature(19, playerData.neckWidth);
}
