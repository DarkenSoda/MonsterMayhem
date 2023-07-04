using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MouseSensitivity : MonoBehaviour {

    [SerializeField] CinemachineFreeLook cinemachineFreeLook;
    [SerializeField] float horizontalSensitivity;
    [SerializeField] float verticalSensitivity;

    private float xSpeed;
    private float ySpeed;

    private void Start() {
        cinemachineFreeLook.m_XAxis.m_MaxSpeed *= horizontalSensitivity;
        cinemachineFreeLook.m_YAxis.m_MaxSpeed *= verticalSensitivity;
        xSpeed = cinemachineFreeLook.m_XAxis.m_MaxSpeed;
        ySpeed = cinemachineFreeLook.m_YAxis.m_MaxSpeed;
    }

    private void Update() {
        cinemachineFreeLook.m_XAxis.m_MaxSpeed = xSpeed * SystemVariables.speedMultiplier * SystemVariables.horizontalSensitivityMultiplier * Time.timeScale;
        cinemachineFreeLook.m_YAxis.m_MaxSpeed = ySpeed * SystemVariables.speedMultiplier * SystemVariables.verticalSensitivityMultiplier * Time.timeScale;
    }
}