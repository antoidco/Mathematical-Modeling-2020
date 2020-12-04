using System;
using UnityEngine;

namespace AircraftSimulator.Physics.Basic
{
    public class CopterPhysicsModel : PhysicsModel
    {
        private readonly Controller _controller;
        private readonly double _dCoef = 5;
        private readonly double _pCoef = 3;
        private CopterPhysicsModelData _data;
        private double _stabHeight;

        public CopterPhysicsModel(Aircraft aircraft, Vector3 initialVelocity, CopterPhysicsModelData data) : base(
            aircraft, initialVelocity)
        {
            _data = data;
            _stabHeight = Aircraft.Position.z;
            _controller = new Controller(
                new PDControl(2.5, 1.5, 0.002),
                new PDControl(5, 3),
                new PDControl(5, 3),
                new PDControl(5, 3),
                aircraft
            );
        }

        protected override void PerformStep(ControlData control, float deltaTime)
        {
            var P = control.AileronAngle;
            var Q = control.ElevatorAngle;
            var R = control.RudderAngle;

            var U = PreviousState.U;
            var V = PreviousState.V;
            var W = PreviousState.W;
            var m = (float) Aircraft.Mass;
            var desiredSpeed = (control.Power - 0.5f) * 10;
            if (!control.Stabilize)
            {
                _stabHeight = Aircraft.Position.z;
                _controller.Reset();
            }
            else
            {
                desiredSpeed = 0;
            }

            var thrust = _controller.ComputeThrust(desiredSpeed, _stabHeight, CurrentState) / 4;

            // engine control
            var totalPower = new Vector3(0, 0, 0);
            foreach (var component in Aircraft.Components)
                if (component is CopterEngine engine)
                {
                    engine.CurrentPower = Math.Sqrt(thrust / engine.LiftConstant());
                    totalPower += engine.GlobalForceVector(Aircraft.Rotation);
                }

            //linear velocity processing
            var currentSpeed = new Vector3(CurrentState.U, CurrentState.V, CurrentState.W);
            totalPower += (float) -Aircraft.DragConstant * currentSpeed;
            totalPower /= (float) Aircraft.Mass;
            totalPower += new Vector3(0, 0, Simulator.GravityConstant);
            totalPower *= deltaTime;
            // evaluate current state
            // this is not physics!!!
            CurrentState.U += totalPower.x;
            CurrentState.V += totalPower.y;
            CurrentState.W += totalPower.z;
            CurrentState.RollRate = P;
            CurrentState.PitchRate = Q;
            CurrentState.YawRate = R;
        }

        private double _computeThrust(float desiredSpeed, double stabHeight)
        {
            var errP = (stabHeight - Aircraft.Position.z) * _pCoef;
            var errD = (desiredSpeed - CurrentState.W) * _dCoef;
            var thrust = (errD + 9.81 + errP) * Aircraft.Mass / (Math.Cos(Math.PI * Aircraft.Rotation.Roll / 180)
                                                                 * Math.Cos(Math.PI * Aircraft.Rotation.Pitch / 180));
            return Math.Max(0, thrust);
        }
    }
}