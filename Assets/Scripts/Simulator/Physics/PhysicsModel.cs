using UnityEngine;

namespace AircraftSimulator.Physics {
    public abstract class PhysicsModel : Model {
        protected AircraftState PreviousState;
        protected AircraftState CurrentState;
        protected float DeltaTime;
        
        public PhysicsModel(Aircraft aircraft, Vector3 initialVelocity) : base(ModelType.Physics) {
            CurrentState = new AircraftState();
            CurrentState.U = initialVelocity.x;
            CurrentState.V = initialVelocity.y;
            CurrentState.W = initialVelocity.z;

            CurrentState.Roll = (float) aircraft.Rotation.Roll;
            CurrentState.Yaw = (float) aircraft.Rotation.Yaw;
            CurrentState.Pitch = (float) aircraft.Rotation.Pitch;

            PreviousState = CurrentState;
        }

        public virtual void Evaluate(ControlData control) { }

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