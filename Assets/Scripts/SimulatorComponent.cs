using AircraftSimulator;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SimulatorComponent : MonoBehaviour {
    public GameObject AircraftInstance;
    private Aircraft _aircraft;
    private Simulator _simulator;
    public AircraftControllerComponent controller;

    public Text info;

    private Wind _wind;

    private void Awake() {
        _aircraft = new Aircraft(AircraftInstance.transform.position, AircraftInstance.transform.rotation);
        var engine1 = new Engine(10, "Engine 1", Vector3.zero);
        _aircraft.Components.Add(engine1);
        engine1.CurrentPower = 20;

        _wind = new Wind(new TurbulentWindModel());

        //_simulator = new Simulator(_aircraft, new Weather(new Wind(new ConstantWindModel(new Vector3(0, 0, 0)))));
        // instead of ConstantWindModel we now use TurbulentWindModel
        _simulator = new Simulator(_aircraft, new Weather(_wind));
    }

    public void OnWindChanged(Single value) {
        _wind.someParametere = value;
    }

    private void Update() {
        if (_aircraft.Position.z > 0) {
            var timeStep = Time.deltaTime;
            var controlData = controller.GetControlData();
            _simulator.Update(timeStep, controlData);

            AircraftInstance.transform.position = _aircraft.Position;
            AircraftInstance.transform.rotation = _aircraft.Rotation.Quaternion;
        }

        info.text = _aircraft.Position.ToString();
    }

    public Simulator GetSimulator() {
        return _simulator;
    }
}