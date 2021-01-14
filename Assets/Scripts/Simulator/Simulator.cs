using System;
using UnityEngine;
using AircraftSimulator.Physics;
using AircraftSimulator.Physics.DariaKlochko;

namespace AircraftSimulator {
    // todo: refactor this
    public class Simulator {
        public const float GravityConstant = -9.81f;
        private readonly Aircraft _aircraft;
        private Weather _weather;
        private double _globalTime;

        private PhysicsModel _physicsModel;
        public double Time { get; private set; }

        public Simulator(Aircraft aircraft, Weather weather) {
            Time = 0;
            _aircraft = aircraft;
            _weather = weather;

            var forsage = GameObject.FindObjectOfType<Forsage>(); 
            _physicsModel = new DariaKlochkoModel(_aircraft, Vector3.zero,
                new DariaKlochkoModelData {
                    ControlRate = 10f,
                    DeadZone = 0.2f,
                    Lerp = 0.03f,
                    MaxTurn = 15.0f,
                    AileronTurnRate = 300f,
                    ElevatorTurnRate = 3f,
                    RudderTurnRate = 100f,
                    Forsage = forsage
                });

            /*_physicsModel = new DariaKlochkoModel(_aircraft, Vector3.zero);*/
        }

        public void Restart(float height) {
            _aircraft.Position = new Vector3(0, 0, height);
        }


        public void Update(double timeStep, ControlData controlData) {
            Time += timeStep;

            _physicsModel.Update(controlData, (float) timeStep);
        }
    }
}