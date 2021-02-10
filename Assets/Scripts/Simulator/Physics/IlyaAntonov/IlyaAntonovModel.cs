using UnityEngine;

namespace AircraftSimulator.Physics.IlyaAntonov {
    public class IlyaAntonovModel : PhysicsModel {
        private IlyaAntonovModelData _data;
        public IlyaAntonovModel(Aircraft aircraft, Vector3 initialVelocity) : base(aircraft, initialVelocity) { }

        protected override void PerformStep(ControlData control, float deltaTime) { }
    }
}