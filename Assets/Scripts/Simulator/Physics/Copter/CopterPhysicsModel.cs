using System.Collections.Generic;
using UnityEngine;

namespace AircraftSimulator.Physics.Basic
{
    public class CopterPhysicsModel : PhysicsModel
    {
        private readonly Controller _controller;
        private CopterPhysicsModelData _data;
        private double _stabHeight;

        public CopterPhysicsModel(Aircraft aircraft, Vector3 initialVelocity, CopterPhysicsModelData data) : base(
            aircraft, initialVelocity)
        {
            _data = data;
            _stabHeight = Aircraft.Position.z;
            _controller = new Controller(
                new PDControl(2.5, 1.5, 0.002),
                new PDControl(1.75, 6, 0.002),
                new PDControl(1.75, 6, 0.001),
                new PDControl(1.75, 6, 0.001),
                aircraft
            );
        }

        protected override void PerformStep(ControlData control, float deltaTime)
        {
            // Yaw angular velocity
            var P = control.AileronAngle * 10;
            // Pitch angle
            var Q = control.ElevatorAngle * 15;
            // Roll angle
            var R = control.RudderAngle * 15;

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

            var desiredRot = new Rotation((float) Aircraft.Rotation.Yaw, Q, R);
            var angularSpeedD = new Vector3(0, 0, P);

            var omega = _controller.ResolveControls(desiredSpeed, _stabHeight, desiredRot,
                angularSpeedD, CurrentState);
            // engine control
            _applyEngineControl(omega);
            var linearSpeed = _processLinear(deltaTime);
            var angularSpeed = _processAngular(omega, deltaTime);
            // evaluate current state
            // this is not physics!!!
            CurrentState.U += linearSpeed.x;
            CurrentState.V += linearSpeed.y;
            CurrentState.W += linearSpeed.z;
            CurrentState.RollRate = angularSpeed.x;
            CurrentState.PitchRate = angularSpeed.y;
            CurrentState.YawRate = angularSpeed.z;
        }

        private Vector3 _processLinear(float deltaTime)
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

        private Vector3 _processAngular(List<double> omega, float deltaTime)
        {
            var eng = _controller.GetEngines();
            //assuming that all engine params are equal
            var k = eng[0].LiftConstant;
            var b = eng[0].PropDrag;
            var L = eng[0].RelativePosition.magnitude;
            var torq = _torques(omega, L, b, k);
            //assuming Ir is equals to zero
            var angular = Vector3.zero;
            angular.x = torq.x - Mathf.Pow(CurrentState.RollRate, 2) * Aircraft.Inertia.x;
            angular.y = torq.y - Mathf.Pow(CurrentState.PitchRate, 2) * Aircraft.Inertia.y;
            angular.z = torq.z - Mathf.Pow(CurrentState.YawRate, 2) * Aircraft.Inertia.z;
            return angular;
        }

        private Vector3 _torques(List<double> omega, float L, float b, float k)
        {
            var torq = new Vector3();
            torq.x = L * k * (Mathf.Pow((float) omega[3], 2) - Mathf.Pow((float) omega[1], 2));
            torq.y = L * k * (Mathf.Pow((float) omega[2], 2) - Mathf.Pow((float) omega[0], 2));
            torq.z = b * (Mathf.Pow((float) omega[3], 2) - Mathf.Pow((float) omega[2], 2) +
                Mathf.Pow((float) omega[1], 2) - Mathf.Pow((float) omega[0], 2));
            return torq;
        }
    }
}