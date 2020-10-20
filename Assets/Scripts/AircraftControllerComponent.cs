using System.Collections;
using System.Collections.Generic;
using AircraftSimulator.Physics;
using UnityEngine;

public class AircraftControllerComponent : MonoBehaviour {
    private float _aileronControl;
    private float _elevatorControl;
    private float _rudderControl;
    private float _engineControl;

    private void Start() {
        _aileronControl = 0f;
        _elevatorControl = 0f;
        _rudderControl = 0f;
        _engineControl = 0.5f; // todo: get these values from sliders!!!
    }
    
    public void OnAileronChange(System.Single value) {
        _aileronControl = value;
    }
    
    public void OnElevatorChange(System.Single value) {
        _elevatorControl = value;
    }
    
    public void OnRudderChange(System.Single value) {
        _rudderControl = value;
    }

    public void OnEngineChange(System.Single value) {
        _engineControl = value;
    }

    public ControlData GetControlData() {
        var data = new ControlData();
        data.AileronAngle = _aileronControl;
        data.ElevatorAngle = _elevatorControl;
        data.RudderAngle = _rudderControl;
        data.Power = _engineControl;
        return data;
    }
}
