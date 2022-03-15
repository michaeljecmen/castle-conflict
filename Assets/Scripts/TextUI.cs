using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    public void updateDisplay(int amount) {
        Text txt = GetComponent<Text>();
        txt.text = amount.ToString();
    }
}
