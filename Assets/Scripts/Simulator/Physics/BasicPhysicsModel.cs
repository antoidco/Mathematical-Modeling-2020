using UnityEngine;

namespace AircraftSimulator.Physics {
    public class BasicPhysicsModel : PhysicsModel {
        private Aircraft _aircraft;

        public BasicPhysicsModel(Aircraft aircraft, Vector3 initialVelocity) : base(aircraft, initialVelocity) {
            _aircraft = aircraft;
        }

        public override void Evaluate(ControlData control) {
            float P = PreviousState.Roll;
            float Q = PreviousState.Pitch;
            float R = PreviousState.Yaw;
            float U = PreviousState.U;
            float V = PreviousState.V;
            float W = PreviousState.W;
            float m = (float)_aircraft.Mass;

            float FxG = 0, FxA= 0, FxT = 0, FyA = 0, FyG = 0, FyT = 0, FzG = 0, FzA = 0, FzT = 0;
            FzG = Simulator.GravityConstant;
            FxT = control.AileronAngle;
            
            CurrentState.U = U + DeltaTime * (R * V - Q * W - FxG / m + FxA / m + FxT / m);
            CurrentState.V = V + DeltaTime * (-R * U + P * W + FyG / m + FyA / m + FyT / m);
            CurrentState.W = W + DeltaTime * (Q * U - P * V + FzG / m + FzA / m + FzT / m);
        }
    }
}