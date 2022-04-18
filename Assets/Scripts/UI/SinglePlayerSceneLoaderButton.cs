using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerSceneLoaderButton : SceneChangeButton {
    public Player humanPlayer;
    public override void changeScene() {
        GameManager.getInstance().setLeftPlayer(humanPlayer);
        base.changeScene();
    }
}
