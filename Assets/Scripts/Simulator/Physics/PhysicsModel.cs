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

        protected virtual void Evaluate(ControlData control, float deltaTime) { }

        public void Update(ControlData control, float deltaTime) {
            Evaluate(control, deltaTime);
            UpdateAircraft(deltaTime);

            PreviousState = CurrentState;
        }

        private void UpdateAircraft(float deltaTime) {
            Aircraft.Position += deltaTime * new Vector3(CurrentState.U, CurrentState.V, CurrentState.W);
            CurrentState.Roll = CurrentState.RollRate * deltaTime;
            CurrentState.Pitch = CurrentState.PitchRate * deltaTime;
            CurrentState.Yaw = CurrentState.YawRate * deltaTime;
            Aircraft.Rotation.Quaternion *= (new Rotation(CurrentState.Yaw, CurrentState.Pitch, CurrentState.Roll)).Quaternion;
        }

        public struct AircraftState {
            public float U, V, W;
            public float RollRate, PitchRate, YawRate;

            public float Roll, Pitch, Yaw;
        }

        public struct ControlData {
            public float Power;
            public float AileronAngle;
            public float RudderAngle;
            public float ElevatorAngle;
        }
    }
}