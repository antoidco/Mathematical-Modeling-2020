using AircraftSimulator;
using UnityEngine;
using UnityEngine.UI;

public enum ModelEnum
{
    Basic,
    IgorGruzdev,
    IlyaAntonov
}

public class SimulatorComponent : MonoBehaviour {
    public GameObject AircraftInstance;
    private Aircraft _aircraft;
    private Simulator _simulator;
    public AircraftControllerComponent controller;
    public Text info;
    public ModelEnum modelEnum;

    private void Awake() {
        _aircraft = new Aircraft(AircraftInstance.transform.position, AircraftInstance.transform.rotation);
        var engine1 = new Engine(10, "Engine 1", Vector3.zero);
        _aircraft.Components.Add(engine1);
        engine1.CurrentPower = 20;

        var weather = new Weather(new Wind(new ConstantWindModel(new Vector3(1, 0, 0))));
        if (modelEnum == ModelEnum.IgorGruzdev) weather = new Weather(new Wind(new TurbulentWindModel()));
        _simulator = new Simulator(_aircraft, weather, modelEnum);
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