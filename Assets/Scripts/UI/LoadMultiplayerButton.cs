using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMultiplayerButton : SceneChangeButton {
    public Player left;
    public Player right;
    public override void changeScene() {
        GameManager.getInstance().setLeftPlayer(left);
        GameManager.getInstance().setRightPlayer(right);
        base.changeScene();
    }
}
