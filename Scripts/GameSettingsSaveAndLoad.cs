using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsSaveAndLoad : MonoBehaviour {
    private const string PLAYER_PREFS_XSENSITIVITY = "xSensitivity";
    private const string PLAYER_PREFS_YSENSITIVITY = "ySensitivity";
    private void OnEnable() {
        SystemVariables.horizontalSensitivityMultiplier = PlayerPrefs.GetFloat(PLAYER_PREFS_XSENSITIVITY, 1f);
        SystemVariables.verticalSensitivityMultiplier = PlayerPrefs.GetFloat(PLAYER_PREFS_YSENSITIVITY, 1f);
    }

    private void OnDisable() {
        PlayerPrefs.SetFloat(PLAYER_PREFS_XSENSITIVITY, SystemVariables.horizontalSensitivityMultiplier);
        PlayerPrefs.SetFloat(PLAYER_PREFS_YSENSITIVITY, SystemVariables.verticalSensitivityMultiplier);
        PlayerPrefs.Save();
    }
}
