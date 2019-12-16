let pol = 0;

function show(id) {
    let heritage = document.getElementById('heritage');
    let person = document.getElementById('person');
    let hair = document.getElementById('hair');
    let face = document.getElementById('face');
    let clothes = document.getElementById('clothes');

    let heritage_enter = document.getElementById('enter_Heritage');
    let person_enter = document.getElementById('enter_Person');
    let hair_enter = document.getElementById('enter_Hair');
    let face_enter = document.getElementById('enter_Face');
    let clothes_enter = document.getElementById('enter_Clothes');

    let info_text = document.getElementById('info_text');
    
    document.getElementById('sidebar_right_text').style.visibility = 'visible';
    document.getElementById('settings_form').style.visibility = 'visible';

    heritage_enter.style.position = "absolute";
    person_enter.style.position = "absolute";
    hair_enter.style.position = "absolute";
    face_enter.style.position = "absolute";
    clothes_enter.style.position = "absolute";

    heritage_enter.style.visibility = "hidden";
    person_enter.style.visibility = "hidden";
    hair_enter.style.visibility = "hidden";
    face_enter.style.visibility = "hidden";
    clothes_enter.style.visibility = "hidden";

    info_text.style.visibility = "visible";

    heritage.setAttribute("style", "width: 390px; background: linear-gradient(270deg, #343434 0%, rgba(52, 52, 52, 0) 100%); box-shadow: inset -4px 0px 0px #FFFFFF;");
    person.setAttribute("style", "width: 390px; background: linear-gradient(270deg, #343434 0%, rgba(52, 52, 52, 0) 100%); box-shadow: inset -4px 0px 0px #FFFFFF;");
    hair.setAttribute("style", "width: 390px; background: linear-gradient(270deg, #343434 0%, rgba(52, 52, 52, 0) 100%); box-shadow: inset -4px 0px 0px #FFFFFF;");
    face.setAttribute("style", "width: 390px; background: linear-gradient(270deg, #343434 0%, rgba(52, 52, 52, 0) 100%); box-shadow: inset -4px 0px 0px #FFFFFF;");
    clothes.setAttribute("style", "width: 390px; background: linear-gradient(270deg, #343434 0%, rgba(52, 52, 52, 0) 100%); box-shadow: inset -4px 0px 0px #FFFFFF;");
    if(id == 1){
        info_text.innerText = "В этом разделе вы можете настроить свою наследственность и схожесть с родителем.";
        heritage.setAttribute("style", "background: linear-gradient(270deg, #61136C 0%, rgba(97, 19, 108, 0) 98.45%); border-radius: 2px; width: 420px; box-shadow: none;");
        heritage_enter.style.position = "static";
        heritage_enter.style.visibility = "visible";
        //face //4
		    mp.trigger('cameraPointTo', 1);
      }
    else if(id == 2){
      info_text.innerText = "В этом разделе вы можете настроить свой внешний вид.";
      person.setAttribute("style", "background: linear-gradient(270deg, #61136C 0%, rgba(97, 19, 108, 0) 98.45%); border-radius: 2px; width: 420px; box-shadow: none;");
      person_enter.style.position = "static";
      person_enter.style.visibility = "visible";
      mp.trigger('cameraPointTo', 1); //6
    }
    else if(id == 3){
      info_text.innerText = "В этом разделе вы свою прическу, цвет волос и прочую растительность на теле.";
      hair.setAttribute("style", "background: linear-gradient(270deg, #61136C 0%, rgba(97, 19, 108, 0) 98.45%); border-radius: 2px; width: 420px; box-shadow: none;");
      hair_enter.style.position = "static";
      hair_enter.style.visibility = "visible";

      mp.trigger('cameraPointTo', 1); //6
    }
    else if(id == 4){
      info_text.innerText = "В этом разделе вы можете настроить основные черты лица.";
      face.setAttribute("style", "background: linear-gradient(270deg, #61136C 0%, rgba(97, 19, 108, 0) 98.45%); border-radius: 2px; width: 420px; box-shadow: none;");
      face_enter.style.position = "static";
      face_enter.style.visibility = "visible";
      mp.trigger('cameraPointTo', 1); //5
    }
    else if(id == 5){
      info_text.innerText = "В этом разделе вы можете выбрать начальную одежду для своего персонажа.";
      clothes.setAttribute("style", "background: linear-gradient(270deg, #61136C 0%, rgba(97, 19, 108, 0) 98.45%); border-radius: 2px; width: 420px; box-shadow: none;");
      clothes_enter.style.position = "static";
      clothes_enter.style.visibility = "visible";
      mp.trigger('cameraPointTo', 2);
		//body //7
    }

}

function setGender(gender){
  var male = document.getElementById("fa-male");
  var female = document.getElementById("fa-female");
  if(gender == 0){
    document.getElementById("male").setAttribute("style", "border: 3px solid #f7f7f7;");
    document.getElementById("female").setAttribute("style", "border: 0px solid #f7f7f7;");
    female.setAttribute("style", "font-size: 50px; color: #000000;");
    male.setAttribute("style", "color: #9335d9; font-size: 50px;");
  }
  else if(gender == 1){
    document.getElementById("female").setAttribute("style", "border: 3px solid #f7f7f7;");
    document.getElementById("male").setAttribute("style", "border: 0px solid #f7f7f7;");
    female.setAttribute("style", "color: #9335d9; font-size: 50px;");
    male.setAttribute("style", "font-size: 50px; color: #000000;");
  }

    pol = gender;
    mp.trigger("cef_setGender", gender);
}

function updateShape(){
    mp.trigger("setChanged_JSON", getShape());
}
function rotateCharacter() {
    var rotation = parseFloat(document.getElementById('character-slider').value);
	mp.trigger('rotateCharacter', rotation);
}
function getShape(){
  let shapeFather = document.getElementById('shapeFather').value;
  let shapeMather = document.getElementById('shapeMather').value;
  let skinFather = document.getElementById('skinFather').value;
  let skinMather = document.getElementById('skinMather').value;
  let shapeMix = document.getElementById('shapeMix').value;
  let skinMix = document.getElementById('skinMix').value;

  return JSON.stringify([shapeFather, shapeMather, skinFather, skinMather, shapeMix, skinMix]);
}

function updateFace(){
    mp.trigger("setFace", getFace());
}
function updateHair(){
    mp.trigger("setHair", getHair());
}
function getHair(){
  let data = [
    document.getElementById('Hair_Style').value,
    document.getElementById('Hair_Color1').value,
    document.getElementById('Hair_Color2').value,

    document.getElementById('Beard_Model').value,
    document.getElementById('Beard_Color').value
  ]
  return JSON.stringify(data);
}
function getFace(){
  let data = [
      document.getElementById('Blemishes').value,
      document.getElementById('Eyebrows').value,
      document.getElementById('Eyebrows_Color').value,
      document.getElementById('Ageing').value,
      document.getElementById('Makeup').value,
      document.getElementById('Blush').value,
      document.getElementById('Blush_Color').value,
      document.getElementById('Complexion').value,
      document.getElementById('Sun_Damage').value,
      document.getElementById('Lipstick').value,
      document.getElementById('Lipstick_Color').value,
      document.getElementById('Moles').value,
      document.getElementById('Chest_Hair').value,
      document.getElementById('Chest_Hair_Color').value
    
  ]

  return JSON.stringify(data);
}

function update_trueFace(){
    mp.trigger("setTrueFace", getTrueFace());
}

function getTrueFace(){
  let data = [
      document.getElementById('Nose_width').value,
      document.getElementById('Nose_height').value,
      document.getElementById('Nose_length').value,
      document.getElementById('Nose_bridge').value,
      document.getElementById('Nose_tip').value,
      document.getElementById('Nose_bridge_shift').value,
      document.getElementById('Brow_height').value,
      document.getElementById('Brow_width').value,
      document.getElementById('Cheekbone_height').value,
      document.getElementById('Cheekbone_width').value,
      document.getElementById('Cheeks_width').value,
      document.getElementById('Eyes').value,
      document.getElementById('Eye_Color').value,
      document.getElementById('Lips').value,
      document.getElementById('Jaw_width').value,
      document.getElementById('Jaw_height').value,
      document.getElementById('Chin_length').value,
      document.getElementById('Chin_position').value,
      document.getElementById('Chin_width').value,
      document.getElementById('Chin_shape').value,
      document.getElementById('Neck_width').value
  ]

  return JSON.stringify(data);
}

function updateClothes(){
    mp.trigger("setClothes", getClothes());
}

function getClothes(){
  var top_true = document.getElementById('top').value;
  var legs_true = document.getElementById('legs').value;
  var feet_true = document.getElementById('feet').value;

  var top = 0;
  var legs = 0;
  var feet = 0;

  if(pol == 0){
    if(top_true == 1) top = 1;
    else if(top_true == 2) top = 7;
    else if(top_true == 3) top = 12;

    if(legs_true == 1) legs = 1;
    else if(legs_true == 2) legs = 5;
    else if(legs_true == 3) legs = 6;

    if(feet_true == 1) feet = 5;
    else if(feet_true == 2) feet = 1;
    else if(feet_true == 3) feet = 27;
  }
  else{
    if(top_true == 1) top = 0;
    else if(top_true == 2) top = 3;
    else if(top_true == 3) top = 31;

    if(legs_true == 1) legs = 4;
    else if(legs_true == 2) legs = 7;
    else if(legs_true == 3) legs = 14;

    if(feet_true == 1) feet = 0;
    else if(feet_true == 2) feet = 1;
    else if(feet_true == 3) feet = 26;
  }

  let data = [
      top,
      legs,
      feet
  ]

  return JSON.stringify(data);
}
function get_current_age(date) {
  var d = date.split('/');
  if( typeof d[2] !== "undefined" ){
      date = d[2]+'.'+d[1]+'.'+d[0];
      return ((new Date().getTime() - new Date(date)) / (24 * 3600 * 365.25 * 1000)) | 0;
  }
  return 0;
  }
function cancel(){
  mp.trigger("cancelCreator");
}
function beforeSave() {
  let data = [ 
    document.getElementById('shapeFather').value,
    document.getElementById('shapeMather').value,
    document.getElementById('skinFather').value,
    document.getElementById('skinMather').value,
    document.getElementById('shapeMix').value,
    document.getElementById('skinMix').value,

    document.getElementById('Nose_width').value,
    document.getElementById('Nose_height').value,
    document.getElementById('Nose_length').value,
    document.getElementById('Nose_tip').value,
    document.getElementById('Nose_bridge_shift').value,
    document.getElementById('Brow_height').value,
    document.getElementById('Brow_width').value,
    document.getElementById('Cheekbone_height').value,
    document.getElementById('Cheekbone_width').value,
    document.getElementById('Cheeks_width').value,
    document.getElementById('Eyes').value,
    document.getElementById('Lips').value,
    document.getElementById('Jaw_width').value,
    document.getElementById('Jaw_height').value,
    document.getElementById('Chin_length').value,
    document.getElementById('Chin_position').value,
    document.getElementById('Chin_width').value,
    document.getElementById('Chin_shape').value,
    document.getElementById('Neck_width').value,

    document.getElementById('Blemishes').value,
    document.getElementById('Facial_Hair').value,
    document.getElementById('Eyebrows').value,
    document.getElementById('Ageing').value,
    document.getElementById('Makeup').value,
    document.getElementById('Blush').value,
    document.getElementById('Complexion').value,
    document.getElementById('Sun_Damage').value,
    document.getElementById('Lipstick').value,
    document.getElementById('Moles').value,
    document.getElementById('Chest_Hair').value,
    document.getElementById('Body_Blemishe').value,
    document.getElementById('Add_Body_Blemishes').value,
    document.getElementById('Hair_Style').value,
    document.getElementById('Hair_Color').value,
    document.getElementById('MakeUp_Color').value
  ]
  return JSON.stringify(data);
}
function save(){
    let n = document.getElementById("b_date").value;
    let age = get_current_age(n);

    let name = document.getElementById("name_row").value;
    let surname = document.getElementById("surname_row").value;
    
    let clothes = getClothes();

    let name_check = /[A-Z]{1}[a-z]{1,10}/;

    let ttt = document.getElementById('ttt');

    if(!name_check.test(name)) {
      ttt.innerHTML = "Неверное имя персонажа";
      return;
    }

    if(!name_check.test(surname)) {
      ttt.innerHTML = "Неверная фамилия персонажа";
      return;
    }

    if(age < 18 || age > 45) {
      ttt.innerHTML = "Неверный возраст персонажа";
      return;
    }
    
    mp.trigger("saveCharacter", 
	  name,
      surname,
      pol,
      age,
      clothes);
}
