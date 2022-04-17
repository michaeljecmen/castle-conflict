using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoaderButton : SceneChangeButton {
    public override void changeScene() {
        GameManager gman = GameManager.getInstance();
        if (gman.getLoadout() == null) {
            gman.setLoadout(gman.defaultLoadout);
        }
        base.changeScene();
    }
}
