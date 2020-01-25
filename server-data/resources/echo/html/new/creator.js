var app = new Vue({
    el: '#content',
    data: {
      name: '',
      surname: '',
     tab: 'character',
     nameError: false,
     surnameError: false,
     gender: 'male',
     shapeMix: 0,
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
    },
    methods: {
      changeGender(sex){
        if (sex == 0){
            this.gender = 'male';
            document.getElementById("male").style.background = "#FFE55C"; 
            document.getElementById("female").style.background = "rgba(255, 255, 255, 0.07)";
            document.getElementById("svg_female").style.fill = "white"
            document.getElementById("svg_male").style.fill = "black"
        }
        else{
            this.gender = 'female';
            document.getElementById("male").style.background = "rgba(255, 255, 255, 0.07)"; 
            document.getElementById("female").style.background = "#FFE55C";
            document.getElementById("svg_female").style.fill = "black"
            document.getElementById("svg_male").style.fill = "white"
        }
      }
    }
  })
  