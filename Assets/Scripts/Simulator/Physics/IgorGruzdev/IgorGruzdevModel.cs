using UnityEngine;

namespace AircraftSimulator.Physics.IgorGruzdev{
    public class IgorGruzdevModel : PhysicsModel{
        private IgorGruzdevModelData _data;
        public IgorGruzdevModel(Aircraft aircraft, Vector3 initalVelocity) : base(aircraft, initalVelocity) { }
        protected override void PerformStep(ControlData control, float deltaTime) { }
    }
}
