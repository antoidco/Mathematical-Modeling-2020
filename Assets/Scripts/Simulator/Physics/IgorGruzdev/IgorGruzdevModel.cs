using UnityEngine;

namespace AircraftSimulator.Physics.IgorGruzdev{
    public class IgorGruzdevModel : PhysicsModel{
        private IgorGruzdevModelData _data;
        private Weather _weather;
        private float _resistanceCoefficient;
        public IgorGruzdevModel(Aircraft aircraft, Vector3 initialVelocity, Weather weather, float resistance, IgorGruzdevModelData data) : base(aircraft, initialVelocity) {
            _data = data;
            _weather = weather;
            _resistanceCoefficient = resistance;
        }

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
                    if (engine.CurrentPower < engine.MaxPower * control.Power && control.Power > 0)
                    {
                        engine.CurrentPower += engine.MaxPower * control.Power / 200;
                    }
                    
                    if (engine.CurrentPower > engine.MaxPower * control.Power && control.Power > 0)
                    {
                        engine.CurrentPower -= engine.MaxPower * control.Power / 200;
                    }

                    if (engine.CurrentPower < engine.MaxPower * control.Power && control.Power < 0)
                    {
                        engine.CurrentPower -= engine.MaxPower * control.Power / 200;
                    }
                    
                    if (engine.CurrentPower > engine.MaxPower * control.Power && control.Power < 0)
                    {
                        engine.CurrentPower += engine.MaxPower * control.Power / 200;
                    }

                    totalPower += (float)engine.CurrentPower;
                }
            }

            float time = Time.time;

            float square = 1; //later need to get it from unity 

            float resistanceVelocity = _resistanceCoefficient * Mathf.Pow(CurrentState.V, 2) * 1.3f * square / 2f * deltaTime;


            var velocityOfWind = Vector3.zero;
                //_weather.Wind.Value(position, time); // here I use aircraft position to obtain wind velocity

            CurrentState.U = velocityOfWind.x;
            CurrentState.V = velocityOfWind.y + totalPower / m - resistanceVelocity;
            CurrentState.W = velocityOfWind.z;
            CurrentState.RollRate = P;
            CurrentState.PitchRate = Q;
            CurrentState.YawRate = R;

            Debug.Log(CurrentState.V - resistanceVelocity);
        }
    }
}
