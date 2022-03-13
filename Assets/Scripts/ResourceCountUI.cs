using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCountUI : MonoBehaviour
{
    public Tower tower;
    private int displayedResourceCount = 0;

    // Start is called before the first frame update

    private void updateDisplay() {
        Text txt = GetComponent<Text>();
        displayedResourceCount = tower.getResourceCount();
        txt.text = displayedResourceCount.ToString();
    }

    void Start() {
        updateDisplay();
    }

    // Update is called once per frame
    void Update() {
        if (displayedResourceCount != tower.getResourceCount()) {
            updateDisplay();
        }
    }
}
