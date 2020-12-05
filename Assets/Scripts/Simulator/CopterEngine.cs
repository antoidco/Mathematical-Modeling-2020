using System;
using UnityEngine;

namespace AircraftSimulator.Physics
{
    public class CopterEngine : Engine
    {
        private float _omega;

        public CopterEngine(double mass, string name, Vector3 relativePosition,
            double maxPower = 10000, float propDrag = 0.0000001f, float liftConstant = (float) (1.85 * 0.00001)) : base(
            mass, name, relativePosition,
            maxPower)
        {
            LiftConstant = liftConstant;
            PropDrag = propDrag;
        }

        public override double CurrentPower
        {
            get => LiftConstant * Math.Pow(_omega, 2);
            set => _omega = (float) Math.Max(Math.Min(MaxPower, value), 0);
        }

        public double Omega => _omega;

        public float PropDrag { get; set; }

        public float LiftConstant { get; }

        public Vector3 GlobalForceVector(Rotation currentRot)
        {
            //return global force vector of engine
            var quat = currentRot.Quaternion;
            var localForce = new Vector3(0, 0, (float) CurrentPower);
            return quat * localForce;
        }
    }
}