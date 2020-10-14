using UnityEngine;

namespace AircraftSimulator {
    // todo: refactor this
    internal class Simulator {
        public const float GravityConstant = -9.81f;
        private readonly Aircraft _aircraft;
        private readonly Vector3 _aircraftVelocity;
        private readonly Weather _weather;
        private Vector3 _newAircraftVelocity;

        public Simulator(Aircraft aircraft, Weather weather) {
            Time = 0;
            _aircraft = aircraft;
            _aircraftVelocity = Vector3.zero;
            _weather = weather;
        }

        public double Time { get; private set; }

        public void Update(double timeStep) {
            Time += timeStep;
            _newAircraftVelocity = _aircraftVelocity;
            GravityProcessing(timeStep);
            EngineProcessing(timeStep);
            WeatherProcessing(timeStep);
            _aircraft.Position += _newAircraftVelocity * (float) timeStep;
        }

        private void EngineProcessing(double timeStep) {
            var accelerationAmplitude = 0.0;
            foreach (var component in _aircraft.Components)
                if (component is Engine engine)
                    accelerationAmplitude += engine.CurrentPower / _aircraft.Mass;

            var acceleration = new Vector3(0, 0, (float) accelerationAmplitude);
            acceleration = _aircraft.Rotation.RQuat * acceleration;

            _newAircraftVelocity += acceleration * (float) timeStep;
        }

        private void GravityProcessing(double timeStep) {
            _newAircraftVelocity += new Vector3(0, GravityConstant, 0) * (float) timeStep;
        }

        private void WeatherProcessing(double timeStep) {
            // todo: influence of wind on the aircraft velocity should be also defined by some model
            var windInfluencePerTimeUnit = 0.1;
            _newAircraftVelocity += _weather.Wind.Value(_aircraft.Position, Time) *
                                    (float) (windInfluencePerTimeUnit * timeStep);
        }
    }
}