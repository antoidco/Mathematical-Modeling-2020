using AircraftSimulator;
using AircraftSimulator.Physics;
using UnityEngine;

public class SimulatorComponent : MonoBehaviour {
    public GameObject AircraftInstance;
    private Aircraft _aircraft;
    private Simulator _simulator;
    public AircraftControllerComponent controller;

    // Start is called before the first frame update
    private void Start() {
        _aircraft = new Aircraft(AircraftInstance.transform.position, AircraftInstance.transform.rotation);
        var engine1 = new Engine(10, "Engine 1", Vector3.zero);
        _aircraft.Components.Add(engine1);
        engine1.CurrentPower = 200;

        _simulator = new Simulator(_aircraft, new Weather(new Wind(new ConstantWindModel(new Vector3(0, 0, 0)))));
    }

    // Update is called once per frame
    private void Update() {
        var timeStep = Time.deltaTime;
        var controlData = controller.GetControlData();
        _simulator.Update(timeStep, controlData);

        AircraftInstance.transform.position = _aircraft.Position;
        AircraftInstance.transform.rotation = _aircraft.Rotation.Quaternion;
    }
}