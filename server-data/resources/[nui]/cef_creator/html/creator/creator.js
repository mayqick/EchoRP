var app = new Vue({
  el: '#content',
  data: {
    name: '',
    surname: '',
    tab: 'character',
    range: 0,
    height: 0,
    rotate: 0,
    nameError: false,
    surnameError: false,
    gender: 'male',
    firstHeadShape: 0,
    secondHeadShape: 21,
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
    browModel: 0,
    browOpacity: 0,
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
    borderStyle: '2px solid white',
    currentEyeColor: 0,
    currentHairColor: 0,
    currentBeardColor: 0,
    eye_selected: "eye_selected",
    beardOpacity: 0,
    skinProblem: 0,
    ageingOpacity: 10,
    currentSkinColor: 0,
    render: false,
    heightModel: 0
  },
  methods: {

    changeGender(sex) {
      if (sex == 0) {
        this.gender = 'male';
        post('http://cef_creator/SendCharacterGender', JSON.stringify({
          isMale: true
        })
        );


      }
      else {
        this.gender = 'female';

        post('http://cef_creator/SendCharacterGender', JSON.stringify({
          isMale: false
        })
        );

      }
      setTimeout(function () {
        app.updateTrueFace();
      }, 1000);

    },
    selectEyeColor(color) {
      this.currentEyeColor = color;
      app.updateTrueFace();
    },
    selectHairColor(color) {
      this.currentHairColor = color;
      app.updateTrueFace();
    },
    selectBeardColor(color) {
      this.currentBeardColor = color;
      app.updateTrueFace();
    },
    selectSkinColor(color) {
      this.currentSkinColor = color;
      app.updateTrueFace();
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
      post('http://cef_creator/SaveCharacter', JSON.stringify({
        name: this.name,
        surname: this.surname,
        gender: this.gender,
        firstHeadShape: this.firstHeadShape,
        secondHeadShape: this.secondHeadShape,
        shapeMix: this.headMix,
        blemishesModel: this.blemishesModel,
        ageingModel: this.ageingModel,
        ageingOpacity: this.ageingOpacity,
        frecklesModel: this.frecklesModel,
        currentEyeColor: this.currentEyeColor,
        currentHairColor: this.currentHairColor,
        currentBeardColor: this.currentBeardColor,
        currentSkinColor: this.currentSkinColor,
        hairStyle: this.hairStyle,
        beardModel: this.beardModel,
        beardOpacity: this.beardOpacity,
        browModel: this.browModel,
        browOpacity: this.browOpacity,
        browHeight: this.browHeight,
        browWidth: this.browWidth,
        noseWidth: this.noseWidth,
        noseHeight: this.noseHeight,
        noseLength: this.noseLength,
        noseTip: this.noseTip,
        noseBridge: this.noseBridge,
        noseShift: this.noseShift,
        lips: this.lips,
        cheekboneHeight: this.cheekboneHeight,
        cheekboneWidth: this.cheekboneWidth,
        jawHeight: this.jawHeight,
        jawWidth: this.jawWidth,
        chinLength: this.chinLength,
        chinPosition: this.chinPosition,
        chinWidth: this.chinWidth,
        chinShape: this.chinShape,
        neckWidth: this.neckWidth
      })
      );
    },
    updateHeight() {
      post('http://cef_creator/UpdateCustomizationCamSettings', JSON.stringify({
        range: this.range,
        height: this.height
      })
      );
    },
    updateRotate() {
      post('http://cef_creator/UpdateCustomizationCamSettings', JSON.stringify({
        range: this.range,
        height: this.height
      })
      );
    },
    updateRange(type) {

      if (type == 'plus') {
        if (this.range < 1) this.range += 1;
      }

      else {
        if (this.range > -1) this.range -= 1;
      }
      post('http://cef_creator/UpdateCustomizationCamSettings', JSON.stringify({
        range: this.range,
        height: this.height
      })
      );
    },
    faceSelect(shape, id) {
      if (shape == 0) this.firstHeadShape = id;
      else this.secondHeadShape = id;
      app.updateTrueFace();
    },
    renderCreatorCef(isRender) {
      if (isRender) document.getElementById('content').style.display = "block";
      else document.getElementById('content').style.display = "none";
    },
    updateTrueFace() {
      post('http://cef_creator/SendCharacterSettings', JSON.stringify({
        firstHeadShape: this.firstHeadShape,
        secondHeadShape: this.secondHeadShape,
        shapeMix: this.headMix,
        blemishesModel: this.blemishesModel,
        ageingModel: this.ageingModel,
        ageingOpacity: this.ageingOpacity,
        frecklesModel: this.frecklesModel,
        currentEyeColor: this.currentEyeColor,
        currentHairColor: this.currentHairColor,
        currentBeardColor: this.currentBeardColor,
        currentSkinColor: this.currentSkinColor,
        hairStyle: this.hairStyle,
        beardModel: this.beardModel,
        beardOpacity: this.beardOpacity,
        browModel: this.browModel,
        browOpacity: this.browOpacity,
        browHeight: this.browHeight,
        browWidth: this.browWidth,
        noseWidth: this.noseWidth,
        noseHeight: this.noseHeight,
        noseLength: this.noseLength,
        noseTip: this.noseTip,
        noseBridge: this.noseBridge,
        noseShift: this.noseShift,
        lips: this.lips,
        cheekboneHeight: this.cheekboneHeight,
        cheekboneWidth: this.cheekboneWidth,
        jawHeight: this.jawHeight,
        jawWidth: this.jawWidth,
        chinLength: this.chinLength,
        chinPosition: this.chinPosition,
        chinWidth: this.chinWidth,
        chinShape: this.chinShape,
        neckWidth: this.neckWidth
      })
      );
    }
  }
})
window.addEventListener('message', (event) => {
  if (event.data.type === 'render') {
    app.renderCreatorCef(true);
  }
  else if (event.data.type === 'unrender') {
    app.renderCreatorCef(false);
  }
});
window.post = (url, data) => {
  var request = new XMLHttpRequest();
  request.open('POST', url, true);
  request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
  request.send(data);
}
