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

            CurrentState.Roll = (float) aircraft.Rotation.Roll;
            CurrentState.Yaw = (float) aircraft.Rotation.Yaw;
            CurrentState.Pitch = (float) aircraft.Rotation.Pitch;

            PreviousState = CurrentState;
        }

        protected virtual void Evaluate(ControlData control, float deltaTime) { }

        public void Update(ControlData control, float deltaTime) {
            Evaluate(control, deltaTime);
            UpdateAircraft();
        }

        private void UpdateAircraft() {
            Aircraft.Position += new Vector3(CurrentState.U, CurrentState.V, CurrentState.W);
            Aircraft.Rotation.Quaternion *= (new Rotation(CurrentState.Roll, CurrentState.Pitch, CurrentState.Yaw)).Quaternion; 
        }

        public struct AircraftState {
            public float U, V, W;
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