using System.Numerics;

namespace AircraftSimulator {
    public abstract class ScalarQuantity : Quantity {
        public abstract float Value(Vector3 position, float time);
        protected ScalarQuantity(Model model) : base(model) { }
    }
}