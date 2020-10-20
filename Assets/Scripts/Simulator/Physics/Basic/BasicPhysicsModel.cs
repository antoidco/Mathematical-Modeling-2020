using UnityEngine;

namespace AircraftSimulator.Physics.Basic {
    public class BasicPhysicsModel : PhysicsModel {
        private BasicPhysicsModelData _data;

        public BasicPhysicsModel(Aircraft aircraft, Vector3 initialVelocity, BasicPhysicsModelData data) : base(
            aircraft, initialVelocity) {
            _data = data;
        }

        protected override void PerformStep(ControlData control, float deltaTime) {
            var rRate = control.AileronAngle - _data.DeadZone;
            var controlR = Mathf.Abs(control.AileronAngle) > _data.DeadZone
                ? Mathf.Abs(rRate) * Mathf.Sign(rRate) * _data.ControlRate
                : 0f;
            var pRate = control.ElevatorAngle - _data.DeadZone;
            var controlP = Mathf.Abs(control.ElevatorAngle) > _data.DeadZone
                ? Mathf.Abs(pRate) * Mathf.Sign(pRate) * _data.ControlRate
                : 0f;
            var yRate = control.RudderAngle - _data.DeadZone;
            var controlY = Mathf.Abs(control.RudderAngle) > _data.DeadZone
                ? Mathf.Abs(yRate) * Mathf.Sign(yRate) * _data.ControlRate
                : 0f;

            float P = Mathf.Lerp(PreviousState.RollRate, controlR, _data.Lerp);
            float Q = Mathf.Lerp(PreviousState.PitchRate, controlP, _data.Lerp);
            float R = Mathf.Lerp(PreviousState.YawRate, controlY, _data.Lerp);

            float U = PreviousState.U;
            float V = PreviousState.V;
            float W = PreviousState.W;
            float m = (float) Aircraft.Mass;

            // engine control
            float totalPower = 0;
            foreach (var component in Aircraft.Components) {
                if (component is Engine engine) {
                    engine.CurrentPower = engine.MaxPower * control.Power;
                    totalPower += (float) engine.CurrentPower;
                }
            }

            // evaluate current state
            Debug.Log((float) Aircraft.Rotation.Roll);
            CurrentState.U = Mathf.Min(30f,
                deltaTime * EvaluationHelper.ClampByModule((float) Aircraft.Rotation.Roll * _data.AileronTurnRate,
                    _data.MaxTurn)
                + deltaTime * EvaluationHelper.ClampByModule((float) Aircraft.Rotation.Yaw * _data.RudderTurnRate,
                    _data.MaxTurn));
            CurrentState.V = V + deltaTime * totalPower / m - deltaTime * Mathf.Abs(control.AileronAngle) * _data.AileronTurnRate;
            CurrentState.W = Mathf.Min(10f,
                W + deltaTime * Simulator.GravityConstant +
                deltaTime * control.ElevatorAngle * _data.ElevatorTurnRate * totalPower);
            CurrentState.RollRate = P;
            CurrentState.PitchRate = Q;
            CurrentState.YawRate = R;
        }
    }
}