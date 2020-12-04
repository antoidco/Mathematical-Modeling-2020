using System;

namespace AircraftSimulator.Physics.Basic
{
    public class Controller
    {
        private readonly Aircraft _aircraft;
        private PDControl _height;
        private PDControl _phi;
        private PDControl _psi;
        private PDControl _theta;

        public Controller(PDControl height, PDControl theta, PDControl psi, PDControl phi, Aircraft aircraft)
        {
            _height = height;
            _theta = theta;
            _phi = phi;
            _psi = psi;
            _aircraft = aircraft;
        }

        public double ComputeThrust(float desiredSpeed, double stabHeight, AircraftState currentState)
        {
            _height.Update(_aircraft.Position.z, stabHeight, currentState.W, desiredSpeed);
            var thrust = (_height.P - Simulator.GravityConstant + _height.D + _height.I) * _aircraft.Mass /
                         (Math.Cos(Math.PI * _aircraft.Rotation.Roll / 180)
                          * Math.Cos(Math.PI * _aircraft.Rotation.Pitch / 180));
            return Math.Max(0, thrust);
        }

        public void Reset()
        {
            _height.ResetI();
        }
    }
}