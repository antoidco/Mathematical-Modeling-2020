using AircraftSimulator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatorComponent : MonoBehaviour
{
    public GameObject AircraftInstance;
    private Aircraft _aircraft;
    private Simulator _simulator;
    // Start is called before the first frame update
    void Start()
    {
        _aircraft = new Aircraft(new System.Numerics.Vector3(AircraftInstance.transform.position.x, AircraftInstance.transform.position.y, AircraftInstance.transform.position.z), new Rotation());
        var engine1 = new Engine(10, "Engine 1", System.Numerics.Vector3.Zero);
        _aircraft.Components.Add(engine1);
        engine1.CurrentPower = 400;

        _simulator = new Simulator(_aircraft, new Weather(new Wind(new ConstantWindModel(new System.Numerics.Vector3(0, 0, 0))))); 
    }

    // Update is called once per frame
    void Update()
    {
        float timeStep = Time.deltaTime;
        if (_aircraft.Position.Y > 0)
        {
            _simulator.Update(timeStep * 5);
        }
        AircraftInstance.transform.position = ConvertVector(_aircraft.Position);
        AircraftInstance.transform.rotation = Quaternion.Euler(90 * Mathf.Sin(Time.time), 90 * Mathf.Cos(Time.time), 0);
    }

    private Vector3 ConvertVector(System.Numerics.Vector3 vector)
    {
        return new Vector3(vector.X, vector.Y, vector.Z);
    }
}
