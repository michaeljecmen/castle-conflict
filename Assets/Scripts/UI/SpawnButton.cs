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

    void Start() {
        // TODO set width and height
    }

    // step 1: make this button a prefab with a constructor which takes in a minion
    // step 2: button onclick sends self (the game object) to the worldmanager, which
    //         looks at the minion that this script controls and spawnLeft()s it
    // step 3: in start/ctor, set button image and text based on minion prefab
    // step 4: do button gray out shit

    // call this method after the prefab has been instantiated
    public void instantiate(Minion m, int i) {
        // first set the minion text and image
        prefab = m;
        GetComponent<Image>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
        GetComponent<Text>().text = prefab.cost.ToString(); // TODO text is child

        // and the index we were given, offset this button's x
        // pos by another WIDTH + buffer units
        RectTransform rect = gameObject.transform as RectTransform;
        Vector2 position = rect.anchoredPosition;
        float startingX = position.x + WIDTH + BUFFER; // TODO does not work
        rect.anchoredPosition = new Vector2(startingX, position.y);

        // set our transform parent to be the canvas
        RectTransform canvasRect = GetComponentInParent<Canvas>().transform as RectTransform;
        transform.SetParent(canvasRect);
    }

    // TODO possibly be notified when tower gains/uses mana so we can 
    // gray out the buttons

    // called when button clicked
    public void spawnMinionForLeftTeam() {

        GameManager.getInstance().getCurrentLevel().spawnLeft(prefab);
    }
}
