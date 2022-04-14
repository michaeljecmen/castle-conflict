using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRefHub : MonoBehaviour {
    [SerializeField]
    private Canvas canvas;
    public Canvas getCanvas() { return canvas; }

    private static CanvasRefHub instance;
    public static CanvasRefHub getInstance() {
        return instance;
    }
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }
}
