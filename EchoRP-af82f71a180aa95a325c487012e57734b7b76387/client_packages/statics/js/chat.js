var switcher = document.getElementById("scope-bar");
var output = document.getElementById("output");
var adjustWidth = document.getElementById("adjust-width");

var adjustWidthCap = document.getElementById("adjust-width-caption");
// document.getElementById('tab-switcher-star').checked = true;

adjustWidth.addEventListener("change", function (e) {
  switcher.style.width = adjustWidthCap.textContent = this.value + "px";
}, false);
function SetActiveChatMode(chatMode) {
  switch (chatMode) {
    case "Say":
      document.getElementById("Say").checked = true;
      break;
    case "Yell":
      document.getElementById("Yell").checked = true;
      break;
    case "Whisper":
      document.getElementById("Whisper").checked = true;
      break;
    case "Ooc":
      document.getElementById("Ooc").checked = true;
      break;
    case "me":
      document.getElementById("me").checked = true;
      break;
    case "do":
      document.getElementById("do").checked = true;
      break;
    case "try":
      document.getElementById("try").checked = true;
      break;
  }
}
function ChatModeChanged(chatMode){
  switch (chatMode) {
    case 1:
      mp.events.call('ChatModeChanged', "Say");
      break;
    case 2:
      mp.events.call('ChatModeChanged', "Yell");
      break;
    case 3:
      mp.events.call('ChatModeChanged', "Whisper");
      break;
    case 4:
      mp.events.call('ChatModeChanged', "Ooc");
      break;
    case 5:
      mp.events.call('ChatModeChanged', "me");
      break;
    case 6:
      mp.events.call('ChatModeChanged', "do");
      break;
    case 7:
      mp.events.call('ChatModeChanged', "try");
      break;
  }
}