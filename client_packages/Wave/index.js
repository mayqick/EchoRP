require('./Wave/globals/browser.js');
require('./Wave/globals/keys.js');

require('./Wave/character/creator.js');
require('./Wave/character/levels.js');
require('./Wave/character/inventory.js');

require('./Wave/account/login.js');
require('./Wave/account/register.js');

mp.gui.execute("window.location = 'package://Wave/globals/chat/index.html'");
require('./Wave/globals/chat/chat.js');
require('./Wave/globals/chat/voice.js');
