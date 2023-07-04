using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour {
    [SerializeField] PlayerAbilities playerAbilities;
    [SerializeField] Image manaBar;

    private void Update() {
        manaBar.fillAmount = playerAbilities.GetStaminaBar() / playerAbilities.GetStaminaBarMax();
    }
}
