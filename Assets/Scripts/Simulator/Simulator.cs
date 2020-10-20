using System;
using UnityEngine;
using AircraftSimulator.Physics;
using AircraftSimulator.Physics.Basic;

namespace AircraftSimulator {
    // todo: refactor this
    internal class Simulator {
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
            _physicsModel = new BasicPhysicsModel(_aircraft, Vector3.zero,
                new BasicPhysicsModelData {
                    ControlRate = 10f, DeadZone = 0.1f, Lerp = 0.01f, MaxTurn = 4.5f, AileronTurnRate = 0.8f,
                    ElevatorTurnRate = 1f, RudderTurnRate = 0.1f
                });
        }

        public void Update(double timeStep, ControlData controlData) {
            Time += timeStep;

            _physicsModel.Update(controlData, (float) timeStep);
        }
    }
}