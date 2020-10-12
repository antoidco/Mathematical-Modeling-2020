using System.Numerics;

namespace AircraftSimulator {
    public class ConstantWindModel : Model {
        public Vector3 Value { get; set; }
        public ConstantWindModel(Vector3 constantWindValue) : base(ModelType.Wind) {
            Value = constantWindValue;
        }
    }
}