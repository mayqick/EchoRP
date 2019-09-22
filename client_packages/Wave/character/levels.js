mp.events.add("updateRankBar", (limit, nextLimit, previousXP, currentXP, currentlvl) => {
    if (!mp.game.graphics.hasHudScaleformLoaded(19)) {
        mp.game.graphics.requestHudScaleform(19);
        while (!mp.game.graphics.hasHudScaleformLoaded(19)) mp.game.wait(0);

        mp.game.graphics.pushScaleformMovieFunctionFromHudComponent(19, "SET_COLOUR");
        mp.game.graphics.pushScaleformMovieFunctionParameterInt(116); //Active bar color
        mp.game.graphics.pushScaleformMovieFunctionParameterInt(123); //Background bar color
        mp.game.graphics.popScaleformMovieFunctionVoid();

        mp.game.ui.setHudColour(116, 171, 51, 42, 255);// HUD_COLOUR_FREEMODE - pattern color
        mp.game.ui.setHudColour(140, 56, 36, 35, 255);// HUD_COLOUR_INGAME_BG - circle background color
        mp.game.graphics.pushScaleformMovieFunctionParameterInt(16);
        mp.game.graphics.popScaleformMovieFunctionVoid();
    }

    mp.game.graphics.pushScaleformMovieFunctionFromHudComponent(19, "SET_RANK_SCORES");
    mp.game.graphics.pushScaleformMovieFunctionParameterInt(limit);
    mp.game.graphics.pushScaleformMovieFunctionParameterInt(nextLimit);
    mp.game.graphics.pushScaleformMovieFunctionParameterInt(previousXP);
    mp.game.graphics.pushScaleformMovieFunctionParameterInt(currentXP);
    mp.game.graphics.pushScaleformMovieFunctionParameterInt(currentlvl);
    mp.game.graphics.popScaleformMovieFunctionVoid();
});