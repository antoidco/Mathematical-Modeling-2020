using UnityEngine;

namespace AircraftSimulator.Physics.MariaRybakonenko
{
    public class MariaRybakonenkoModel : PhysicsModel{
        private MariaRybakonenkoModelData _data;
        private Weather _weather; // lets cache weather to use it later !
        public MariaRybakonenkoModel(Aircraft aircraft, Vector3 initialVelocity, Weather weather, MariaRybakonenkoModelData data) : base(aircraft, initialVelocity) {
            _data = data;
            _weather = weather;
        }

        //public Vector3 Position { get; private set; } // bad! 

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

            var position = Aircraft.Position; // good! you only need this position locally (not globally)

            float totalPower = 0;
            foreach (var component in Aircraft.Components)
            {
                if (component is Engine engine)
                {
                    engine.CurrentPower = engine.MaxPower * control.Power;
                    totalPower += (float)engine.CurrentPower;
                }
            }

            var globalTime = 0; // suddenly, we do not know global time, I need to implement it later...
            // however, your Turbulent Wind Model is also not using global time for now, so let it be zero
            
            var velocityOfWind =
                _weather.Wind.Value(position, globalTime); // here I use aircraft position to obtain wind velocity

            CurrentState.U = velocityOfWind.x;
            CurrentState.V = velocityOfWind.y + totalPower / m;
            CurrentState.W = velocityOfWind.z;
            CurrentState.RollRate = P;
            CurrentState.PitchRate = Q;
            CurrentState.YawRate = R;
        }
    }
}
