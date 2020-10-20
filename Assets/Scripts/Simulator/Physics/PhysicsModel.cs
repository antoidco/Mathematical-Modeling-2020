using UnityEngine;

namespace AircraftSimulator.Physics {
    public abstract class PhysicsModel : Model {
        protected AircraftState PreviousState;
        protected AircraftState CurrentState;
        protected Aircraft Aircraft;

        public PhysicsModel(Aircraft aircraft, Vector3 initialVelocity) : base(ModelType.Physics) {
            Aircraft = aircraft;
            CurrentState = new AircraftState();
            CurrentState.U = initialVelocity.x;
            CurrentState.V = initialVelocity.y;
            CurrentState.W = initialVelocity.z;

            CurrentState.RollRate = 0;
            CurrentState.YawRate = 0;
            CurrentState.PitchRate = 0;

            PreviousState = CurrentState;
        }

        protected virtual void PerformStep(ControlData control, float deltaTime) { }

        public void Update(ControlData control, float deltaTime) {
            PerformStep(control, deltaTime);
            UpdateAircraft(deltaTime);

            PreviousState = CurrentState;
        }

        private void UpdateAircraft(float deltaTime) {
            Aircraft.Position += deltaTime * new Vector3(CurrentState.U, CurrentState.V, CurrentState.W);
            var newYaw = CurrentState.YawRate * deltaTime;
            var newPitch = CurrentState.PitchRate * deltaTime;
            var newRoll = CurrentState.RollRate * deltaTime;
            Aircraft.Rotation.Quaternion *= (new Rotation(newYaw, newPitch, newRoll)).Quaternion;
        }
    }
}