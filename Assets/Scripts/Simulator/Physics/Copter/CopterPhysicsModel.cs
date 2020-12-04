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

        protected override void PerformStep(ControlData control, float deltaTime)
        {
            var P = control.AileronAngle;
            var Q = control.ElevatorAngle;
            var R = control.RudderAngle;

            var U = PreviousState.U;
            var V = PreviousState.V;
            var W = PreviousState.W;
            var m = (float) Aircraft.Mass;

            // engine control
            var totalPower = new Vector3(0, 0, 0);
            foreach (var component in Aircraft.Components)
                if (component is CopterEngine engine)
                {
                    engine.CurrentPower = engine.MaxPower * control.Power;
                    totalPower += engine.GlobalForceVector(Aircraft.Rotation);
                }

            //linear velocity processing
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
    }
}