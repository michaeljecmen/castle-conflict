using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{   
    // gap in pixels from the edges and other buttons
    public const int BUFFER = 50;
    public const int WIDTH = 200;
    public const int HEIGHT = 200;

    // the minion prefab this button will spawn
    public Minion prefab;

    // step 1: make this button a prefab with a constructor which takes in a minion
    // step 2: button onclick sends self (the game object) to the worldmanager, which
    //         looks at the minion that this script controls and spawnLeft()s it
    // step 3: in start/ctor, set button image and text based on minion prefab
    // step 4: do button gray out shit

    // call this method after the prefab has been instantiated
    public void initialize(Minion m, int i) {
        prefab = m;

        // set the minion image and text
        Button button = GetComponent<Button>();
        button.GetComponentInChildren<Image>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;

        Text text = GetComponentInChildren<Text>();
        text.text = prefab.cost.ToString();

        // set our transform parent to be the canvas
        Canvas canvas = WorldManager.getInstance().getCanvas();
        gameObject.transform.SetParent(canvas.transform, false);

        RectTransform rect = button.transform as RectTransform;
        rect.sizeDelta = new Vector2(WIDTH, HEIGHT);

        // and the index we were given, offset this button's x
        // pos by another WIDTH + buffer units
        Vector2 buttonPos = rect.anchoredPosition;
        float startingX = buttonPos.x + i*(WIDTH + BUFFER);
        rect.anchoredPosition = new Vector2(startingX, buttonPos.y);
    }

    // called when button clicked
    public void spawnMinionForLeftTeam() {
        WorldManager.getInstance().spawnLeft(prefab);
    }
}
