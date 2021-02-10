using System;
using System.Collections.Generic;
using UnityEngine;

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

        public Vector3 ComputeTorques(Rotation desiredRot, Vector3 desiredSpeed, AircraftState currentState)
        {
            _phi.Update(_aircraft.Rotation.Roll, desiredRot.Roll,
                currentState.RollRate, desiredSpeed.x);
            _theta.Update(_aircraft.Rotation.Pitch, desiredRot.Pitch,
                currentState.PitchRate, desiredSpeed.y);
            _psi.Update(_aircraft.Rotation.Yaw, desiredRot.Yaw,
                currentState.YawRate, desiredSpeed.z);

            var torques = Vector3.zero;
            torques.x = (float) (_phi.P + _phi.D + _phi.I) * _aircraft.Inertia.x;
            torques.y = (float) (_theta.P + _theta.D + _theta.I) * _aircraft.Inertia.y;
            torques.z = (float) (_psi.P + _psi.D + _psi.I) * _aircraft.Inertia.z;
            return torques;
        }

        public List<double> ResolveControls(float desiredSpeed, double stabHeight, Rotation desiredRot,
            Vector3 desiredSpeedA, AircraftState currentState)
        {
            var thrust = ComputeThrust(desiredSpeed, stabHeight, currentState);
            var torques = ComputeTorques(desiredRot, desiredSpeedA, currentState);
            var omega = new List<double>();
            var eng = GetEngines();
            //assuming that it is quadcopter
            for (var i = 0; i < 4; i++)
            {
                var k = eng[i].LiftConstant;
                var b = eng[i].PropDrag;
                var L = eng[i].RelativePosition.magnitude;
                int coef;
                if (i < 2) coef = -1;
                else coef = 1;
                float Torq12 = 0;
                if (Mathf.Pow(-1, i + 1) < 0) Torq12 = coef * (torques.y / (2 * k * L));
                else Torq12 = coef * (torques.x / (2 * k * L));
                var omegaSq = thrust / (4 * k) + Torq12 + Mathf.Pow(-1, i + 1) * (torques.z / (4 * b));
                omega.Add(Mathf.Sqrt(Mathf.Max((float) omegaSq, 0)));
            }

            return omega;
        }

        public List<CopterEngine> GetEngines()
        {
            var eng = new List<CopterEngine>();
            foreach (var comp in _aircraft.Components)
                if (comp is CopterEngine engine)
                    eng.Add(engine);

            return eng;
        }

        public void Reset()
        {
            _height.ResetI();
        }
    }
}