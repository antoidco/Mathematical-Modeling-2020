using UnityEngine;

namespace AircraftSimulator.Physics.Basic
{
    public class MaksimVolginModel : PhysicsModel
    {
        private MaksimVolginModelData _data;

        public MaksimVolginModel(Aircraft aircraft, Vector3 initialVelocity, MaksimVolginModelData data) : base(
            aircraft, initialVelocity)
        {
            _data = data;
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
         
            Vector3 prevSpeed = new Vector3(CurrentState.U, CurrentState.V, CurrentState.W);

            /* add up all the accelerations provided by the model */
            Vector3 accelerated = Vector3.zero;
            accelerated += accelOfGravity();
            accelerated += accelEngine(control);
            accelerated += accelAirResistance(prevSpeed);
            
            CurrentState.U += deltaTime * accelerated.x;
            CurrentState.V += deltaTime * accelerated.y;
            CurrentState.W += deltaTime * accelerated.z;
            CurrentState.RollRate = P;
            CurrentState.PitchRate = Q;
            CurrentState.YawRate = R;
        }

        /**
         * input:
         * Vector3 vec - vector in global coordinate sistem
         * 
         * output:
         * Vector3 - vector in aircraft coordinat system
         */
        Vector3 globalToAircraftSystem(Vector3 vec)
        {
            Quaternion revertRotation = Aircraft.Rotation.Quaternion;
            revertRotation.w *= -1f;

            return revertRotation * vec;
        }

        /**
         * input:
         * Vector3 vec - vector in aircraft coordinat system
         * 
         * output:
         * Vector3 - vector in global coordinate sistem
         */
        Vector3 aircraftToGlobalSistem(Vector3 vec)
        {
            return Aircraft.Rotation.Quaternion * vec;
        }

        /**
         * output:
         * the function returns the gravitational acceleration vector in the plane's coordinate system
         */
        Vector3 accelOfGravity()
        {
            return globalToAircraftSystem(Simulator.GravityConstant * Vector3.forward);
        }

        /**
         * input:
         * ControlData control - controler data
         * 
         * output:
         * accelerated vector from engins in aircraft coordinate system
         */
        Vector3 accelEngine(ControlData control)
        {
            Vector3 result = Vector3.zero;
            float totalPower = 0;
            foreach (var component in Aircraft.Components)
            {
                if (component is Engine engine)
                {
                    engine.CurrentPower = engine.MaxPower * control.Power;
                    totalPower += (float)engine.CurrentPower;
                }
            }
            result.y = totalPower;

            return result;
        }

        /**
         * input:
         * Vector3 speed - previus velosity vector in aircraft coordinate system
         * 
         * output:
         * air resistance forc accelerated vector in aircraft coordinate system
         */
        Vector3 accelAirResistance(Vector3 speed)
        {
            float gain = 
                speed.magnitude *
                _data.aerodynamicDrag *
                _data.frontalResistanceArea *
                _data.airDensity / 2f;
            Vector3 result = gain * -speed;
            return result;
        }
    }
}
