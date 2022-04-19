using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftChangeLoadoutButton : SceneChangeButton {
    public override void changeScene() {
        GameManager.getInstance().currentLoadoutSelector = GameManager.getInstance().getLeftPlayer();
        base.changeScene();
    }
}
