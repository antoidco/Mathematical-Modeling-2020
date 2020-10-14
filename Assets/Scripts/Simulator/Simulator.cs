using System;
using System.Numerics;
using AircraftSimulator.Physics;

namespace AircraftSimulator {
    // todo: refactor this
    class Simulator {
        public const float GravityConstant = -9.81f;
        private Aircraft _aircraft;
        private Vector3 _aircraftVelocity;
        private Vector3 _newAircraftVelocity;
        private Weather _weather;
        private double _globalTime;

        private PhysicsModel _physicsModel;
        public double Time => _globalTime;

        public Simulator(Aircraft aircraft, Weather weather) {
            _globalTime = 0;
            _aircraft = aircraft;
            _aircraftVelocity = Vector3.Zero;
            _weather = weather;
            
            _physicsModel = new BasicPhysicsModel(_aircraft, UnityEngine.Vector3.zero);
        }

        public void Update(double timeStep) {
            _globalTime += timeStep;
            _newAircraftVelocity = _aircraftVelocity;
            
            _physicsModel.Evaluate(new PhysicsModel.ControlData());
            // to do: add change of aircraft state to physics model 
            
            WeatherProcessing(timeStep);
            _aircraft.Position += _newAircraftVelocity * (float) timeStep;
        }



        private void WeatherProcessing(double timeStep) {
            // todo: influence of wind on the aircraft velocity should be also defined by some model
            double windInfluencePerTimeUnit = 0.1;
            _newAircraftVelocity += _weather.Wind.Value(_aircraft.Position, _globalTime) *
                                    (float) (windInfluencePerTimeUnit * timeStep);
        }
    }
}