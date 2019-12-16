$('.inventory-conteiner_item .item-content').draggable({
  revert:true
});

$('.inventory-conteiner_side-item').click(function() {
  // $('.inventory-conteiner_side-item').removeAttr('item-selected');
  // $(this).attr('item-selected', '');
});

let currentCellMenuSelected = "none";
let currentCellIndex = -1;
let currentBCellIndex = -1;

//updated to last style ++
$.contextMenu({//context menu constructor
  selector: '.inventory-conteiner_item .item-content', 

  build: function($triggerElement, e){ //here we check if slot is empty, no need to open menu
      let uid = $triggerElement[0].id;
      let cell = GetCellByItemUid(uid);
      let info = cell.children[0].children[0].id;

      if(!CheckCellToExist(cell.id))
          return false;

      currentCellMenuSelected = cell.id;
      var detailInfo = "";
      switch(info){
        case "gun":
        detailInfo = "Стоковая автоматическая винтовка на базе AR-16 ресивера, применялась армией США во времена вьетнамской войны, да и сейчас впрочем сойдет.";
        break
        case "ganja":
        detailInfo = "Хапка травы, употреблять только в медицинский целях!";
           break;
        case "fomka":
        detailInfo = "Обычный лом, физики ядерщики отчего то относятся к нему с особым трепетом.";
        break;
        case "robber_mask":
        detailInfo = "Маска грабителя, ходят слухи что была разработана местными криминалететами Кировского района.";
        break;
        case "black_camo":
        detailInfo = "Джихадка, носить осторожно, провоцирует подрыв.";
        break;
      }

      return {

        items: {
          "Info": {
            name: detailInfo, 
            disabled: function(key, opt) {    
              return !this.data('cutDisabled');           
            }
          }
        }
    };
    },
  items: {
      "edit": {
          name: "Использовать", 
          icon: "edit", 
          callback: MenuCallback
      },
      "cut": {
          name: "Разделить", 
          icon: "cut", 
          callback: MenuCallback
      },
      "drop": {
        name: "Выбросить", 
        icon: "delete", 
        callback: MenuCallback
    }
  }
});

//updated to last style ++
function MenuCallback(key, option){ //menu invoke callback
  switch(key){
    case "edit":
    mp.trigger("editMenuCallback", currentCellMenuSelected);
    break
    case "drop":
    RemoveItemFromCell(currentCellMenuSelected);
    mp.trigger("dropMenuCallback", currentCellMenuSelected);
    break;
    case "cut":
    mp.trigger("cutMenuCallback", currentCellMenuSelected);
    break;
  }

  currentCellMenuSelected = "none";
}
 //updated to last style ++
$('.inventory-conteiner_item').droppable({drop:function(event, ui){ //slot move action
    var to = event.target.id;

    if(CheckCellToExist(to))//if to slot already used, stackable todo!
    {     
      var from;
      if(currentBCellIndex != -1)
        from = 'bcell' + currentBCellIndex; 
      if(currentCellIndex != -1)
        from = 'cell' + currentCellIndex;

      currentBCellIndex = -1;
      currentCellIndex = -1;    
      return;
    }
    else 
    {
      ui.offset = 0;
      $(this).append($(ui.draggable)); 

      if(currentBCellIndex != -1){
        //console.log("bcell" + currentBCellIndex, to);
        mp.trigger("inventoryItemMove", "bcell" + currentBCellIndex, to);
     }
  
     if(currentCellIndex != -1){
       //console.log("cell" + currentCellIndex, to);
       mp.trigger("inventoryItemMove", "cell" + currentCellIndex, to);
     }
    }

    currentBCellIndex = -1;
    currentCellIndex = -1;  
 }});

 //updated to last style ++
 function GetCellByItemUid(itemUid){
  for(let i = 0; i < 11; i++){
      var cellName = 'cell' + i;
      var cell = document.getElementById(cellName);   
      if(cell.childElementCount > 0){
        if(cell.children[0].id == itemUid)
           return cell;
      }
  }

  for(let i = 0; i < 11; i++){
    var bCellName = 'bcell' + i;
    var bCell = document.getElementById(bCellName);
    if(bCell.childElementCount > 0){
      if(bCell.children[0].id == itemUid)
         return bCell;
    }
  } 
}

//updated to last style ++
 function IntantiateCell(jsonData){//create new item on cell
  var data = JSON.parse(jsonData);
  for(let i = 0; i < data.length; i++){
    var itemUid = data[i].id;
    var cell = document.getElementById(data[i].slot);
    var count = data[i].count;
    console.log(itemUid, cell, count);
    switch (itemUid) {
      case "gun":
        var imgPath = "./images/icons/gun.png";
        break;
    
      default:
        break;
    }
    var itemContent = document.createElement("div");
    itemContent.classList.add("item-content");
    itemContent.id = itemUid;
  
    var itemSpan = document.createElement("span");
    itemSpan.textContent = count;
    
    var img = document.createElement("Img");
    img.src = imgPath;
    img.id = itemUid;
    itemContent.appendChild(img);
    itemContent.appendChild(itemSpan);
    cell.appendChild(itemContent);
    console.log(cell);
   }

  DeployCell();
 }

// Создание предмета в инвентаре
 function IntantiateCellNotJson(cellName, uid, pathToImg, count){
  let cell = document.getElementById(cellName)
  let itemUid = uid;
  let imgPath = pathToImg;
  var itemContent = document.createElement("div");
  itemContent.classList.add("item-content");
  itemContent.id = itemUid;
  var itemCount = count;
  var itemSpan = document.createElement("span");
  itemSpan.textContent = itemCount;
  
  var img = document.createElement("Img");
  img.src = imgPath;
  img.id = itemUid;
  itemContent.appendChild(img);
  itemContent.appendChild(itemSpan);
  cell.appendChild(itemContent);
  DeployCell();
 }

 //updated to last style ++
 function ReplaceItem(documentName, to){
  let fromCell = document.getElementById(documentName);
  let toCell = document.getElementById(to);
  if(toCell == null)
    return;
  
  if(CheckCellToExist(documentName)){
    let fromItem = fromCell.children[0];
    fromCell.removeChild(fromItem);
    if(!CheckCellToExist(toCell)){
      toCell.appendChild(fromItem);
      DeployCell();    
    }
  }
 }

 function UpdateStack(cellName, newCount){
   if(CheckCellToExist(cellName)){
    let cell = document.getElementById(cellName);
    if(cell.childElementCount > 0)
    if(cell.children[0].childElementCount > 0)
    cell.children[0].children[1].textContent = newCount;
   }
 }

 // Удаление предмета из инвентаря
 function RemoveItemFromCell(cellName){
  var fromCell = document.getElementById(cellName);
  if(fromCell != null){
    let result = CheckCellToExist(cellName);
    if(result) {
        let toDelete = fromCell.children[0];
        fromCell.removeChild(toDelete);
      }
  }
}

//configure new items to draggable action
 function DeployCell(){
  $('.inventory-conteiner_item .item-content').draggable({
    revert:true
  });
 }

//updated to last style ++
 function CheckCellToExist(cellName){//check cell to empty
   let cell = document.getElementById(cellName);
   if(cell == null)
      return false;

   if(cell.hasChildNodes() && cell.childElementCount > 0){
      if(cell.children[0].children[0].nodeName == "IMG" && cell.children[0].id != "")
         return true;
   }
  
   return false;
 }
function SetInventoryInformation(name, money, bank){
  document.getElementById("characterName").textContent = name;
  document.getElementById("moneyCount").textContent = money + "$";
  document.getElementById("cardMoneyCount").textContent = bank + "$";
}
 function SetName(name){
  document.getElementById("characterName").textContent = name;
 }
 function SetMoney(count){
  document.getElementById("moneyCount").textContent = count;
 }
 function SetCardMoney(count){
  document.getElementById("cardMoneyCount").textContent = count;
 }
 function SetMainWeight(weight){
  document.getElementById("mainWeight").textContent = weight;
 }
 function SetBackpackWeight(weight){
  document.getElementById("backpackWeight").textContent = weight;
 }
 function dragStartCell(index){
  currentCellIndex = index;
 }
 function dragStartBCell(index){
  currentBCellIndex = index;
 }