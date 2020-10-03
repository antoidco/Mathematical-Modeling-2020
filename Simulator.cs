using System;
using System.Numerics;

namespace AircraftSimulator {
    // todo: refactor this
    class Simulator {
        public const float GravityConstant = -9.81f;
        private Aircraft _aircraft;
        private Vector3 _aircraftVelocity;
        private Vector3 _newAircraftVelocity;
        private Weather _weather;
        private double _globalTime;
        public double Time => _globalTime;

        public Simulator(Aircraft aircraft, Weather weather) {
            _globalTime = 0;
            _aircraft = aircraft;
            _aircraftVelocity = Vector3.Zero;
            _weather = weather;
        }

        public void Update(double timeStep) {
            _globalTime += timeStep;
            _newAircraftVelocity = _aircraftVelocity;
            GravityProcessing(timeStep);
            EngineProcessing(timeStep);
            WeatherProcessing(timeStep);
            _aircraft.Position += _newAircraftVelocity * (float) timeStep;
        }

        private void EngineProcessing(double timeStep) {
            var accelerationAmplitude = 0.0;
            foreach (var component in _aircraft.Components) {
                if (component is Engine engine)
                    accelerationAmplitude += engine.CurrentPower / _aircraft.Mass;
            }

            var acceleration = new Vector3(
                (float) (accelerationAmplitude * Math.Sin(_aircraft.Rotation.Yaw) * Math.Cos(_aircraft.Rotation.Roll)),
                (float) (accelerationAmplitude * Math.Cos(_aircraft.Rotation.Yaw) * Math.Cos(_aircraft.Rotation.Roll)),
                (float) (accelerationAmplitude * Math.Sin(_aircraft.Rotation.Roll)));

            _newAircraftVelocity += acceleration * (float) timeStep;
        }

        private void GravityProcessing(double timeStep) {
            _newAircraftVelocity += new Vector3(0, 0, GravityConstant) * (float) timeStep;
        }

        private void WeatherProcessing(double timeStep) {
            // todo: influence of wind on the aircraft velocity should be also defined by some model
            double windInfluencePerTimeUnit = 0.1;
            _newAircraftVelocity += _weather.Wind.Value(_aircraft.Position, _globalTime) *
                                    (float) (windInfluencePerTimeUnit * timeStep);
        }
    }
}