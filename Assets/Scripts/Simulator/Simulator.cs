using System;
using UnityEngine;
using AircraftSimulator.Physics;
using AircraftSimulator.Physics.Basic;
using AircraftSimulator.Physics.IgorGruzdev;

namespace AircraftSimulator {
    // todo: refactor this
    public class Simulator {
        public const float GravityConstant = -9.81f;
        private readonly Aircraft _aircraft;
        private Weather _weather;
        private float _globalTime;

        private PhysicsModel _physicsModel;
        public float Time { get; private set; }

        public Simulator(Aircraft aircraft, Weather weather) {
            Time = 0;
            _aircraft = aircraft;
            _weather = weather;
            /*
            _physicsModel = new BasicPhysicsModel(_aircraft, Vector3.zero,
                new BasicPhysicsModelData {
                    ControlRate = 10f, DeadZone = 0.2f, Lerp = 0.03f, MaxTurn = 15.0f, AileronTurnRate = 300f,
                    ElevatorTurnRate = 3f, RudderTurnRate = 100f
                });
            */
            _physicsModel = new IgorGruzdevModel(_aircraft, Vector3.zero, _weather, 0.2f,
                new IgorGruzdevModelData
                {
                    ControlRate = 10f,
                    DeadZone = 0.2f,
                    Lerp = 0.03f,
                    MaxTurn = 15.0f,
                    AileronTurnRate = 300f,
                    ElevatorTurnRate = 3f,
                    RudderTurnRate = 100f
                }) ;
        }

        public void Restart(float height) {
            _aircraft.Position = new Vector3(0, 0, height);
        }

        public void Update(float timeStep, ControlData controlData) {
            Time += timeStep;
            
            _physicsModel.Update(controlData, timeStep);
        }
    }
}