using UnityEngine;

namespace AircraftSimulator.Physics.IgorGruzdev{
    public class IgorGruzdevModel : PhysicsModel{
        private IgorGruzdevModelData _data;
        public IgorGruzdevModel(Aircraft aircraft, Vector3 initialVelocity, Weather weather, IgorGruzdevModelData data) : base(aircraft, initialVelocity) {
            _data = data;
        }

        public Vector3 Position { get; private set; }

        protected override void PerformStep(ControlData control, float deltaTime)
        {
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
            float m = (float)Aircraft.Mass;

            Position = Aircraft.Position;

            float totalPower = 0;
            foreach (var component in Aircraft.Components)
            {
                if (component is Engine engine)
                {
                    if (engine.CurrentPower < engine.MaxPower * control.Power && control.Power > 0)
                    {
                        engine.CurrentPower += engine.MaxPower * control.Power / 100;
                    }

                    if (engine.CurrentPower > engine.MaxPower * control.Power && control.Power > 0)
                    {
                        engine.CurrentPower -= engine.MaxPower * control.Power / 100;
                    }

                    if (engine.CurrentPower < engine.MaxPower * control.Power && control.Power < 0)
                    {
                        engine.CurrentPower -= engine.MaxPower * control.Power / 100;
                    }

                    if (engine.CurrentPower > engine.MaxPower * control.Power && control.Power < 0)
                    {
                        engine.CurrentPower += engine.MaxPower * control.Power / 100;
                    }

                    totalPower += (float)engine.CurrentPower;
                }
            }

            CurrentState.U = 0;
            CurrentState.V = 0; // totalPower / m;
            CurrentState.W = 0;
            CurrentState.RollRate = P;
            CurrentState.PitchRate = Q;
            CurrentState.YawRate = R;

            var vel = (CurrentState.V - V)/deltaTime;
            Debug.Log(vel.ToString()); ;
        }
    }
}
