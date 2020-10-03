using System.Numerics;

namespace AircraftSimulator {
    public abstract class VectorQuantity : Quantity {
        public abstract Vector3 Value(Vector3 position, double time);
        protected VectorQuantity(Model model) : base(model) { }
    }
}