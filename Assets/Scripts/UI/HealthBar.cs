using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // draw bar as a fraction of max possible HP
    public void updateDisplay(int hp) {
        Slider s = GetComponent<Slider>();
        s.value = (float) hp / GameManager.getInstance().getCurrentLevel().maxTowerHealth;
    }
}
