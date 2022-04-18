using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoaderButton : SceneChangeButton {
    // levels don't exist in multiplayer, and
    // other player is always an AI in single player
    public Player ai;
    public override void changeScene() {
        // set the opponent to be the specified AI
        GameManager.getInstance().setRightPlayer(ai);
        base.changeScene();
    }
}
