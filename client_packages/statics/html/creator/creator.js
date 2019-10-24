var app = new Vue({
  el: '#app',
  data: {
    name: '',
    surname: '',
    faceId: 0,
    faceColorId: 0,
    check_settings: 'gender',
    sex: 0,
    firstHeadShape: 0,
    secondHeadShape: 0,
    firstSkinTone: 0,
    secondSkinTone: 0,
    headMix: 0.5,
    skinMix: 0.5,
    hairModel: 0,
    hairStyle: 0,
    firstHairColor: 0,
    secondHairColor: 0,
    beardModel: 255,
    beardColor: 0,
    chestModel: 255,
    chestColor: 0,
    blemishesModel: 255,
    ageingModel: 255,
    complexionModel: 255,
    sundamageModel: 255,
    frecklesModel: 255,
    eyesColor: 0,
    eyebrowsModel: 255,
    eyebrowsColor: 0,
    makeupModel: 255,
    blushModel: 255,
    blushColor: 0,
    lipstickModel: 255,
    lipstickColor: 0,
    noseWidth: 0.0,
    noseHeight: 0.0,
    noseLength: 0.0,
    noseBridge: 0.0,
    noseTip: 0.0,
    noseShift: 0.0,
    browHeight: 0.0,
    browWidth: 0.0,
    cheekboneHeight: 0.0,
    cheekboneWidth: 0.0,
    eyes: 0.0,
    lips: 0.0,
    jawWidth: 0.0,
    jawHeight: 0.0,
    chinLength: 0.0,
    chinPosition: 0.0,
    chinWidth: 0.0,
    chinShape: 0.0,
    neckWidth: 0.0,
    noseWidth: 0.0,
    top: 0,
    legs: 0,
    feet: 0
  },
  methods: {
    updateFace() {
      let data = [
        this.blemishesModel,
        this.eyebrowsModel,
        this.eyebrowsColor,
        this.ageingModel,
        this.makeupModel,
        this.blushModel,
        this.blushColor,
        this.complexionModel,
        this.sundamageModel,
        this.lipstickModel,
        this.lipstickColor,
        this.frecklesModel,
        this.chestModel,
        this.chestColor
      ]
      mp.trigger("setFace", JSON.stringify(data));
    },
    updateTrueFace() {
      let data = [
        this.noseWidth,
        this.noseHeight,
        this.noseLength,
        this.noseBridge,
        this.noseTip,
        this.noseShift,
        this.browHeight,
        this.browWidth,
        this.cheekboneHeight,
        this.cheekboneWidth,
        this.eyes,
        this.eyesColor,
        this.lips,
        this.jawWidth,
        this.jawHeight,
        this.chinLength,
        this.chinPosition,
        this.chinWidth,
        this.chinShape,
        this.neckWidth
      ]
      mp.trigger("setTrueFace", JSON.stringify(data));
    },
    updateHair(){
      let data = [
        this.hairStyle,
        this.firstHairColor,
        this.secondHairColor,
        this.beardModel,
        this.beardColor
      ]
      mp.trigger("setHair", JSON.stringify(data));
    },
    updateShape(){
      mp.trigger("setChanged_JSON", this.faceId);
    },
    faceIdPlus() {
      if (this.faceId < 45) {
        this.faceId++;
        this.updateShape();
      }
    },
    faceIdMinus() {
      if (this.faceId > 0) {
        this.faceId--;
        this.updateShape();
      }
    }
  }
})
