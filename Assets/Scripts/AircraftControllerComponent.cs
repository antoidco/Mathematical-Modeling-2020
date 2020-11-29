using AircraftSimulator.Physics;
using UnityEngine;
using UnityEngine.UI;

public class AircraftControllerComponent : MonoBehaviour {
    private float _aileronControl;
    private float _elevatorControl;
    private float _rudderControl;
    private float _engineControl;
    private float _windControl;

    public Slider aileronSlider;
    public Slider elevatorSlider;
    public Slider rudderSlider;
    public Slider engineSlider;
    public Slider windSlider;

    private void Start() {
        _aileronControl = aileronSlider.value;
        _elevatorControl = elevatorSlider.value;
        _rudderControl = rudderSlider.value;
        _engineControl = engineSlider.value; // todo: get these values from sliders!!!
        _windControl = windSlider.value; // todo: get these values from sliders!!!

        aileronSlider.onValueChanged.AddListener(OnAileronChange);
        elevatorSlider.onValueChanged.AddListener(OnElevatorChange);
        rudderSlider.onValueChanged.AddListener(OnRudderChange);
        engineSlider.onValueChanged.AddListener(OnEngineChange);
        windSlider.onValueChanged.AddListener(OnWindChange);
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

    public void OnWindChange(System.Single value)
    {
        _windControl = value;
    }

    public ControlData GetControlData() {
        var data = new ControlData();
        data.AileronAngle = _aileronControl;
        data.ElevatorAngle = _elevatorControl;
        data.RudderAngle = _rudderControl;
        data.Power = _engineControl;
        data.Wind = _windControl;
        return data;
    }
}
