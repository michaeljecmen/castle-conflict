using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightChangeLoadoutButton : SceneChangeButton {
    public override void changeScene() {
        GameManager.getInstance().currentLoadoutSelector = GameManager.getInstance().getRightPlayer();
        base.changeScene();
    }
}
