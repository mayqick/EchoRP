var app = new Vue({
  el: '#app',
  data: {
    name: '',
    surname: '',
    faceId: 0,
    faceColorId: 0,
    check_settings: 'gender',
    sex: 'male',
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
    beardModel: 0,
    beardColor: 0,
    chestModel: 0,
    chestColor: 0,
    blemishesModel: 0,
    ageingModel: 0,
    complexionModel: 0,
    sundamageModel: 0,
    frecklesModel: 0,
    eyesColor: 0,
    eyebrowsModel: 0,
    eyebrowsColor: 0,
    makeupModel: 0,
    blushModel: 0,
    blushColor: 0,
    lipstickModel: 0,
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
    feet: 0,
    nameError: false,
    surnameError: false,
    height: 50,
    range: 50
  },
  methods: {
    changeHeight() {
      switch (this.height) {
        case 0:
          mp.trigger("cameraHeightTo", -0.5);
          break;
        case 10:
          mp.trigger("cameraHeightTo", -0.4);
          break;
        case 20:
          mp.trigger("cameraHeightTo", -0.3);
          break;
        case 30:
          mp.trigger("cameraHeightTo", -0.2);
          break;
        case 40:
          mp.trigger("cameraHeightTo", -0.1);
          break;
        case 50:
          mp.trigger("cameraHeightTo", 0.0);
          break;
        case 60:
          mp.trigger("cameraHeightTo", 0.1);
          break;
        case 70:
          mp.trigger("cameraHeightTo", 0.2);
          break;
        case 80:
          mp.trigger("cameraHeightTo", 0.3);
          break;
        case 90:
          mp.trigger("cameraHeightTo", 0.4);
          break;
        case 100:
          mp.trigger("cameraHeightTo", 0.5);
          break;
      }
    },
    changeRange(arg) {
      if(arg == 0){
        if(this.range < 100){
          this.range += 10;
        }
      }
      else if(arg == 1){
        if(this.range > 0){
          this.range -= 10;
        }
      }
      switch (this.range) {
        case 0:
          mp.trigger("cameraRangetTo", 0.5);
          break;
        case 10:
          mp.trigger("cameraRangetTo", 0.4);
          break;
        case 20:
          mp.trigger("cameraRangetTo", 0.3);
          break;
        case 30:
          mp.trigger("cameraRangetTo", 0.2);
          break;
        case 40:
          mp.trigger("cameraRangetTo", 0.1);
          break;
        case 50:
          mp.trigger("cameraRangetTo", 0.0);
          break;
        case 60:
          mp.trigger("cameraRangetTo", -0.1);
          break;
        case 70:
          mp.trigger("cameraRangetTo", -0.2);
          break;
        case 80:
          mp.trigger("cameraRangetTo", -0.3);
          break;
        case 90:
          mp.trigger("cameraRangetTo", -0.4);
          break;
        case 100:
          mp.trigger("cameraRangetTo", -0.5);
          break;
      }
    },
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
    updateHair() {
      let data = [
        this.hairStyle,
        this.firstHairColor,
        this.secondHairColor,
        this.beardModel,
        this.beardColor
      ]
      mp.trigger("setHair", JSON.stringify(data));
    },
    updateShape() {
      mp.trigger("setChanged_JSON", this.faceId);
    },
    changeGender(sex) {
      if (sex == 1) {
        this.sex = "male";
      }
      else if (sex == 2) {
        this.sex = "female";
      }
    },
    createCharacter() {
      let name_check = /[A-Z]{1}[a-z]{1,10}/;
      if (!name_check.test(this.name.toString())) {
        this.nameError = true;
        return;
      } else {
        this.nameError = false;
      }
      if (!name_check.test(this.surname.toString())) {
        this.surnameError = true;
        return;
      } else {
        this.surnameError = false;

      }
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
