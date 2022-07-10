using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutManager : MonoBehaviour {
    public float loadoutScreenHeaderSize;
    public GameObject loadoutMinionButton;
    public Button backButton;
    private Minion[] loadout;
    public Hashtable currentLoadoutButtons = new Hashtable();

    void Start() {
        GameManager gman = GameManager.getInstance();

        // get temp current player loadout
        Minion[] currentPlayerLoadout = gman.currentLoadoutSelector.getLoadout();

        // set up our loadout container
        int maxLoadoutSize = gman.getLoadoutSize();
        loadout = new Minion[maxLoadoutSize];
        for (int i = 0; i < maxLoadoutSize; ++i) {
            loadout[i] = null;
        }

        // initialize our member variables, remembering to always
        // use addToLoadout when adding stuff
        for (int i = 0; i < currentPlayerLoadout.Length; ++i) {
            if (currentPlayerLoadout[i] != null) {
                addToLoadout(currentPlayerLoadout[i]);
            }
        }

        // first, puts all minion buttons on screen
        // next, puts "current loadout" box on screen
        // with actual current loadout, if it exists
        Minion[] masterList = gman.getMasterMinionList();
        for (int i = 0; i < masterList.Length; i++) {
            GameObject button = Instantiate(loadoutMinionButton.gameObject, loadoutMinionButton.gameObject.transform.position, Quaternion.identity);
            LoadoutButton lb = button.GetComponent<LoadoutButton>();
            lb.setHeaderSize(loadoutScreenHeaderSize);
            lb.initialize(masterList[i]);
            lb.setLoadoutManager(this);
            lb.setPosition(i);
        }
    }

    private int getCurrentLoadoutSize() {
        return currentLoadoutButtons.Count;
    }

    private GameObject createCurrentLoadoutButton(Minion m, int i) {
        GameObject button = Instantiate(loadoutMinionButton.gameObject, loadoutMinionButton.gameObject.transform.position, Quaternion.identity);
        LoadoutButton lb = button.GetComponent<LoadoutButton>();

        // no header for the current loadout
        lb.setHeaderSize(0);
        lb.initialize(m);
        lb.setLoadoutManager(this);
        lb.setPosition(i);

        // these aren't real buttons
        button.GetComponent<Button>().interactable = false;

        return button;
    }

    // put minion in first null location we find
    // return true if add was successful
    public bool addToLoadout(Minion minion) {
        for (int i = 0; i < GameManager.getInstance().getLoadoutSize(); ++i) {
            if (loadout[i] == null) {
                loadout[i] = minion;
                currentLoadoutButtons[minion.gameObject.name] = createCurrentLoadoutButton(minion, i);

                updateBackButtonStatus();
                return true;
            }
        }
        return false;
    }

    public bool removeFromLoadout(Minion minion) {
        for (int i = 0; i < GameManager.getInstance().getLoadoutSize(); ++i) {
            // TODO try with loadout[i] == minion
            if (loadout[i] != null && loadout[i].gameObject.name == minion.gameObject.name) {
                loadout[i] = null;

                // delete the key from the hashtable and delete the button object itself
                Destroy((GameObject)currentLoadoutButtons[minion.gameObject.name]);
                currentLoadoutButtons.Remove(minion.gameObject.name);

                updateBackButtonStatus();
                return true;
            }
        }
        return false;
    }

    private void updateBackButtonStatus() {
        backButton.interactable = getCurrentLoadoutSize() == GameManager.getInstance().getLoadoutSize();
    }

    public void setLoadoutAndGoToLevelSelect() {
        GameManager.getInstance().currentLoadoutSelector.setLoadout(loadout);
        GameManager.getInstance().goBack();
    }
}
