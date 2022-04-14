using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// by default, creates one row of buttons at wherever the button prefab is situated
// then goes L->R across screen
public class MinionButtonGenerator : MonoBehaviour {
    // gap in pixels from the edges and other buttons
    public int buffer = 50;
    public int width = 200;
    public int height = 200;

    public Minion prefab;

    // step 1: make this button a prefab with a constructor which takes in a minion
    // step 2: button onclick sends self (the game object) to the worldmanager, which
    //         looks at the minion that this script controls and spawnLeft()s it
    // step 3: in start/ctor, set button image and text based on minion prefab
    // call this method after the prefab has been instantiated
    public virtual void initialize(Minion m) {
        prefab = m;

        // set the minion image and text
        Button button = GetComponent<Button>();
        button.GetComponentInChildren<Image>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;

        Text text = GetComponentInChildren<Text>();
        text.text = prefab.cost.ToString();

        // set our transform parent to be the canvas
        Canvas canvas = CanvasRefHub.getInstance().getCanvas();
        gameObject.transform.SetParent(canvas.transform, false);

        RectTransform rect = button.transform as RectTransform;
        rect.sizeDelta = new Vector2(width, height);
    }

    public virtual void setPosition(int i) {
        Button button = GetComponent<Button>();
        RectTransform rect = button.transform as RectTransform;
        // and the index we were given, offset this button's x
        // pos by another WIDTH + buffer units
        Vector2 buttonPos = rect.anchoredPosition;
        float startingX = buttonPos.x + i*(width + buffer);
        rect.anchoredPosition = new Vector2(startingX, buttonPos.y);
    }
}
