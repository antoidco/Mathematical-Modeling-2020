using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlaneSim.PlaneSim
{
    class Simulator
    {
        public const float GravityConstant = -9.81f;
        private Aircraft _aircraft;
        private Vector3 _aircraftVelocity;
        private Vector3 _newAircaftVelocity;
        public Simulator(Aircraft aircraft)
        {
            _aircraft = aircraft;
            _aircraftVelocity = Vector3.Zero;
        }
        public void Update(double timeStep)
        {
            _newAircaftVelocity = _aircraftVelocity;
            GravityProcessing(timeStep);
            EngineProcessing(timeStep);
            _aircraft.Position += _newAircaftVelocity * (float)timeStep;
        }
        private void EngineProcessing(double timeStep)
        {
            var accelerationAmplitude = 0.0;
            foreach(Engine engine in _aircraft.Components)
            {
                accelerationAmplitude += engine.CurrentPower / _aircraft.Mass;
            }
            var acceleration = new Vector3(
                (float) (accelerationAmplitude * Math.Sin(_aircraft.Rotation.Yaw) * Math.Cos(_aircraft.Rotation.Roll)),
                (float) (accelerationAmplitude * Math.Cos(_aircraft.Rotation.Yaw) * Math.Cos(_aircraft.Rotation.Roll)),
                (float) (accelerationAmplitude * Math.Sin(_aircraft.Rotation.Roll)));

            _newAircaftVelocity += acceleration * (float)timeStep;
        }
        private void GravityProcessing(double timeStep)
        {
            _newAircaftVelocity += new Vector3(0, 0, GravityConstant) * (float)timeStep;
        }
    }
}
