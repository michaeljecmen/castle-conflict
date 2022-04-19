using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class LoadoutButton : MinionButtonGenerator {
    private bool inLoadout = false; // TODO this isn't set correctly in start
    private LoadoutManager manager;
    private float headerSize;

    public void setHeaderSize(float hs) {
        headerSize = hs;
    }

    public void setLoadoutManager(LoadoutManager man) {
        manager = man;

        // check the manager to see if we're in the loadout currently
        inLoadout = manager.currentLoadoutButtons.ContainsKey(prefab.gameObject.name);
    }

    // called when button clicked
    public void toggleMinionInLoadout() {
        bool success = (inLoadout) ? manager.removeFromLoadout(prefab) : manager.addToLoadout(prefab);
        if (success) {
            inLoadout = !inLoadout;
        }
    }

    // we want as many rows as needed now
    // use buttonsPerRow and the screen width to determine
    // spacing of buttons
    public override void setPosition(int i) {
        int buttonsPerRow = (Screen.width - buffer) / (buffer + width);

        // calculate postion based on that
        int row = i / buttonsPerRow;
        int col = i % buttonsPerRow;

        // top left corner, go down buffer over buffer down headersize
        // and that's where the first button should go
        Button button = GetComponent<Button>();
        RectTransform rect = button.transform as RectTransform;
        float x = (buffer + width) * col + buffer + width/2;
        float y = (buffer + height) * row + buffer + height/2 + headerSize;
        rect.anchoredPosition = new Vector2((Screen.width / -2) + x, (Screen.height / 2) - y);
    }
}
