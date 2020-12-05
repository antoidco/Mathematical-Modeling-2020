using System.Collections.Generic;
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

            var desiredRot = new Rotation(0, 0, 0);
            var angularSpeed = new Vector3(0, 0, 0);

            var omega = _controller.ResolveControls(desiredSpeed, _stabHeight, desiredRot,
                angularSpeed, CurrentState);
            // engine control
            _applyEngineControl(omega);
            var linearSpeed = _processLinear(omega, deltaTime);
            // evaluate current state
            // this is not physics!!!
            CurrentState.U += linearSpeed.x;
            CurrentState.V += linearSpeed.y;
            CurrentState.W += linearSpeed.z;
            CurrentState.RollRate = P;
            CurrentState.PitchRate = Q;
            CurrentState.YawRate = R;
        }

        private Vector3 _processLinear(List<double> omega, float deltaTime)
        {
            var totalPower = new Vector3(0, 0, 0);
            var eng = _controller.GetEngines();
            for (var i = 0; i < 4; i++) totalPower += eng[i].GlobalForceVector(Aircraft.Rotation);

            //linear velocity processing
            var currentSpeed = new Vector3(CurrentState.U, CurrentState.V, CurrentState.W);
            totalPower -= (float) Aircraft.DragConstant * currentSpeed;
            totalPower /= (float) Aircraft.Mass;
            totalPower += new Vector3(0, 0, Simulator.GravityConstant);
            totalPower *= deltaTime;
            return totalPower;
        }

        private void _applyEngineControl(List<double> omega)
        {
            var eng = _controller.GetEngines();
            for (var i = 0; i < 4; i++) eng[i].CurrentPower = omega[i];
        }
    }
}