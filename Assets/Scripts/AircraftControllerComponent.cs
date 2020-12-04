using AircraftSimulator.Physics;
using UnityEngine;
using UnityEngine.UI;

public class AircraftControllerComponent : MonoBehaviour
{
    public Slider aileronSlider;
    public Slider elevatorSlider;
    public Slider rudderSlider;
    public Slider engineSlider;
    public Button stabButton;
    private float _aileronControl;
    private float _elevatorControl;
    private float _engineControl;
    private float _rudderControl;
    private bool _stabilize = true;

    private void Start()
    {
        _aileronControl = aileronSlider.value;
        _elevatorControl = elevatorSlider.value;
        _rudderControl = rudderSlider.value;
        _engineControl = engineSlider.value; // todo: get these values from sliders!!!

        aileronSlider.onValueChanged.AddListener(OnAileronChange);
        elevatorSlider.onValueChanged.AddListener(OnElevatorChange);
        rudderSlider.onValueChanged.AddListener(OnRudderChange);
        engineSlider.onValueChanged.AddListener(OnEngineChange);
        stabButton.onClick.AddListener(OnStabPressed);
    }

    public void OnAileronChange(float value)
    {
        _aileronControl = value;
    }

    public void OnElevatorChange(float value)
    {
        _elevatorControl = value;
    }

    public void OnStabPressed()
    {
        _stabilize = !_stabilize;
    }

    public void OnRudderChange(float value)
    {
        _rudderControl = value;
    }

    public void OnEngineChange(float value)
    {
        _engineControl = value;
    }

    public ControlData GetControlData()
    {
        var data = new ControlData();
        data.AileronAngle = _aileronControl;
        data.ElevatorAngle = _elevatorControl;
        data.RudderAngle = _rudderControl;
        data.Power = _engineControl;
        data.Stabilize = _stabilize;
        return data;
    }
}