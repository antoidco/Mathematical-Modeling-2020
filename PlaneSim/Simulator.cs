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
        public Simulator(Aircraft aircraft)
        {
            _aircraft = aircraft;
            _aircraftVelocity = Vector3.Zero;
        }
        public void Update(double timeStep)
        {
            var newVelocity = _aircraftVelocity + new Vector3(0, 0, GravityConstant) * (float)timeStep;
            _aircraft.Position += newVelocity * (float)timeStep;
        }
        private void EngineProcessing()
        {

        }
    }
}
