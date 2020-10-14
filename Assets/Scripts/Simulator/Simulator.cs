using System;
using UnityEngine;
using AircraftSimulator.Physics;

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
            _physicsModel = new BasicPhysicsModel(_aircraft, UnityEngine.Vector3.zero);
        }

        public void Update(double timeStep) {
            Time += timeStep;

            _physicsModel.Update(new PhysicsModel.ControlData(), (float) timeStep);
        }
    }
}