using System.Collections;
using System.Collections.Generic;
using AircraftSimulator.Physics;
using UnityEngine;

public class AircraftControllerComponent : MonoBehaviour {
    private float _aileronControl;
    private float _elevatorControl;
    private float _rudderControl;

    private void Start() {
        _aileronControl = 0f;
        _elevatorControl = 0f;
        _rudderControl = 0f;
    }
    
    public void OnAileronChange(System.Single value) {
        _aileronControl = value;
        Debug.Log("Aileron angle changed: " + value);
    }
    
    public void OnElevatorChange(System.Single value) {
        _elevatorControl = value;
    }
    
    public void OnRudderChange(System.Single value) {
        _rudderControl = value;
    }

    public PhysicsModel.ControlData GetControlData() {
        var data = new PhysicsModel.ControlData();
        data.AileronAngle = _aileronControl;
        data.ElevatorAngle = _elevatorControl;
        data.RudderAngle = _rudderControl;
        return data;
    }
}
