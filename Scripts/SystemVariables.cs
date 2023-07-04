using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SystemVariables {
    private static readonly Lazy<SystemVariables> _instance = new Lazy<SystemVariables>(() => new SystemVariables());

    public static SystemVariables Instance { get { return _instance.Value; } }

    private SystemVariables() { }

    public static float speedMultiplier = 1f;
    public static float defaultMultiplier = 1f;
    public static float minSpeedMultiplier = .3f;
    public static float horizontalSensitivityMultiplier = 1f;
    public static float verticalSensitivityMultiplier = 1f;

    public static void ChangeSpeedMultiplier(float speedMultiplier) {
        SystemVariables.speedMultiplier = speedMultiplier;
    }

    public static void ChangeXSensitivity(float xSensitivity) {
        horizontalSensitivityMultiplier = xSensitivity / 5;
    }

    public static void ChangeYSensitivity(float ySensitivity) {
        verticalSensitivityMultiplier = ySensitivity / 5;
    }
}
