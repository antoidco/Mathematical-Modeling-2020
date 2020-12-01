using UnityEngine;

namespace AircraftSimulator.Physics.Basic
{
    public class CopterPhysicsModel : PhysicsModel
    {
        private CopterPhysicsModelData _data;

        public CopterPhysicsModel(Aircraft aircraft, Vector3 initialVelocity, CopterPhysicsModelData data) : base(
            aircraft, initialVelocity)
        {
            _data = data;
        }

        protected override void PerformStep(ControlData control, float deltaTime, Rotation currentRot)
        {
            var P = Mathf.Lerp(-Mathf.PI, Mathf.PI, control.AileronAngle);
            var Q = control.ElevatorAngle;
            var R = control.RudderAngle;

            var U = PreviousState.U;
            var V = PreviousState.V;
            var W = PreviousState.W;
            var m = (float) Aircraft.Mass;

            // engine control
            float totalPower = 0;
            foreach (var component in Aircraft.Components)
                if (component is Engine engine)
                {
                    engine.CurrentPower = engine.MaxPower * control.Power;
                    totalPower += (float) engine.CurrentPower;
                }

            // evaluate current state
            // this is not physics!!!
            CurrentState.U = 0;
            CurrentState.V = totalPower;
            CurrentState.W += deltaTime * Simulator.GravityConstant * 0.1f;
            CurrentState.RollRate = P;
            CurrentState.PitchRate = Q;
            CurrentState.YawRate = R;
        }
    }
}