using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChangeButton : MonoBehaviour {
    public string targetScene;

    public virtual void changeScene() {
        GameManager.getInstance().changeScene(targetScene);
    }
}
