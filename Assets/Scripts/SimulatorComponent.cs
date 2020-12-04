using AircraftSimulator;
using AircraftSimulator.Physics;
using UnityEngine;
using UnityEngine.UI;

public class SimulatorComponent : MonoBehaviour
{
    public GameObject AircraftInstance;
    public AircraftControllerComponent controller;

    public Text info;
    private Aircraft _aircraft;
    private Simulator _simulator;

    private void Awake()
    {
        _aircraft = new Aircraft(AircraftInstance.transform.position, AircraftInstance.transform.rotation,
            1.14 * Mathf.Pow(10, -7),
            new Vector3(0.0625f, 0.0625f, 0.125f));
        var engine1 = new CopterEngine(3, "Engine 1", new Vector3(1, 0, 0), 4000);
        var engine2 = new CopterEngine(3, "Engine 2", new Vector3(0, -1, 0), 4000);
        var engine3 = new CopterEngine(3, "Engine 3", new Vector3(-1, 0, 0), 4000);
        var engine4 = new CopterEngine(3, "Engine 4", new Vector3(0, 1, 0), 4000);
        _aircraft.Components.Add(engine1);
        _aircraft.Components.Add(engine2);
        _aircraft.Components.Add(engine3);
        _aircraft.Components.Add(engine4);
        engine1.CurrentPower = 20;
        foreach (var component in _aircraft.Components)
            if (component is CopterEngine engine)
                engine.CurrentPower = 20;
        _simulator = new Simulator(_aircraft, new Weather(new Wind(new ConstantWindModel(new Vector3(0, 0, 0)))));
    }

    private void Update()
    {
        if (_aircraft.Position.z > 0)
        {
            var timeStep = Time.deltaTime;
            var controlData = controller.GetControlData();
            _simulator.Update(timeStep, controlData);

            AircraftInstance.transform.position = _aircraft.Position;
            AircraftInstance.transform.rotation = _aircraft.Rotation.Quaternion;
        }

        info.text = _aircraft.Position.ToString();
    }

    public Simulator GetSimulator()
    {
        return _simulator;
    }
}