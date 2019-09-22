var char_name = [];
function showChars(jsonChars, slot_3, slot_4, donate, playerName, charactersInfo) {
    let data = JSON.parse(jsonChars);
    let charsInformation = JSON.parse(charactersInfo);

    locked_3 = document.getElementById("locked_3");
    locked_4 = document.getElementById("locked_4");

    not_created1 = document.getElementById("not_created1");
    not_created2 = document.getElementById("not_created2");
    not_created3 = document.getElementById("not_created3");
    not_created4 = document.getElementById("not_created4");

    not_created3.style.display = "none";
    not_created4.style.display = "none";


    document.getElementById("player_nickname").textContent = playerName;
    document.getElementById("donate_money").textContent = donate;
    if (slot_3 == true){
        not_created3.style.display = "block";
        locked_3.style.display = "none";
    }
    if (slot_4 == true){
        not_created4.style.display = "block";
        locked_4.style.display = "none";
    }
    for(let i = 0; i < charsInformation.length; i++){
        document.getElementById("lvl_" + i).textContent = charsInformation[i].lvl + " LVL / " + charsInformation[i].xp + " XP";
        document.getElementById("money_" + i).textContent = charsInformation[i].money;
        document.getElementById("bank_" + i).textContent = charsInformation[i].bank;
    }

    for(let i = 0; i < data.length; i++){
        let element = document.getElementById('name' + (i+1));
        document.getElementById('select_' + (i+1)).style.display = "block";
        document.getElementById('not_created' + (i+1)).style.display = "none";
        element.textContent = data[i];
        
    
        char_name[i] = data[i];
        //document.getElementById('btn_select' + (i+1)).setAttribute('onclick', "selectChar("+ char_name +")");
    }
}
function selectChar(char_id){ 
    mp.trigger("characterSelected", char_name[char_id]) 
}


function showCreator(){
    mp.trigger("createNewChar");
}