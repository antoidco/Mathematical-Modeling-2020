using UnityEngine;

namespace AircraftSimulator.Physics
{
    public abstract class PhysicsModel : Model
    {
        protected Aircraft Aircraft;
        protected AircraftState CurrentState;
        protected AircraftState PreviousState;

        public PhysicsModel(Aircraft aircraft, Vector3 initialVelocity) : base(ModelType.Physics)
        {
            Aircraft = aircraft;
            Restart(initialVelocity);
        }

        protected virtual void PerformStep(ControlData control, float deltaTime)
        {
        }

        public void Update(ControlData control, float deltaTime)
        {
            PerformStep(control, deltaTime);
            UpdateAircraft(deltaTime);

            PreviousState = CurrentState;
        }

        private void UpdateAircraft(float deltaTime)
        {
            var localVelocity = new Vector3(CurrentState.U, CurrentState.V, CurrentState.W);
            var globalVelocity = Aircraft.Rotation.Quaternion * localVelocity;
            Aircraft.Position += deltaTime * globalVelocity;
            var newYaw = CurrentState.YawRate * deltaTime;
            var newPitch = CurrentState.PitchRate * deltaTime;
            var newRoll = CurrentState.RollRate * deltaTime;
            Aircraft.Rotation.Quaternion *= new Rotation(newYaw, newPitch, newRoll).Quaternion;
        }

        public void Restart(Vector3 initialVelocity)
        {
            CurrentState = new AircraftState();
            CurrentState.U = initialVelocity.x;
            CurrentState.V = initialVelocity.y;
            CurrentState.W = initialVelocity.z;

            CurrentState.RollRate = 0;
            CurrentState.YawRate = 0;
            CurrentState.PitchRate = 0;

            PreviousState = CurrentState;
        }
    }
}