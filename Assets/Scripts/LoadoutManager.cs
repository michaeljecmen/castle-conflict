using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutManager : MonoBehaviour {
    public float loadoutScreenHeaderSize;
    public GameObject loadoutMinionButton;
    public Button backButton;
    private int currentLoadoutSize = 0;
    private Minion[] loadout;

    void Start() {
        loadout = GameManager.getInstance().getLoadout();

        // first, puts all minion buttons on screen
        // next, puts "current loadout" box on screen
        // with actual current loadout, if it exists
        Minion[] masterList = GameManager.getInstance().getMasterMinionList();
        for (int i = 0; i < masterList.Length; i++) {
            GameObject button = Instantiate(loadoutMinionButton.gameObject, loadoutMinionButton.gameObject.transform.position, Quaternion.identity);
            LoadoutButton lb = button.GetComponent<LoadoutButton>();
            lb.setHeaderSize(loadoutScreenHeaderSize);
            lb.setLoadoutManager(this);
            lb.initialize(masterList[i]);
            lb.setPosition(i);
        }
    }

    // put in first null location
    public void addToLoadout(Minion minion) {
        for (int i = 0; i < GameManager.getInstance().getLoadoutSize(); ++i) {
            // TODO try with loadout[i] == minion
            if (loadout[i] == null) {
                loadout[i] = minion;
                currentLoadoutSize++;
                updateBackButtonStatus();
                return;
            }
        }
    }

    public void removeFromLoadout(Minion minion) {
        for (int i = 0; i < GameManager.getInstance().getLoadoutSize(); ++i) {
            // TODO try with loadout[i] == minion
            if (loadout[i].gameObject.name == minion.gameObject.name) {
                loadout[i] = null;
                currentLoadoutSize--;
                updateBackButtonStatus();
                return;
            }
        }
    }

    private void updateBackButtonStatus() {
        backButton.interactable = currentLoadoutSize == GameManager.getInstance().getLoadoutSize();
    }

    public void setLoadoutAndGoToLevelSelect() {
        GameManager.getInstance().setLoadout(loadout);
        GameManager.getInstance().changeScene("LevelSelect");
    }
}
