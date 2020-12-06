using UnityEngine;

namespace AircraftSimulator {
    public abstract class VectorQuantity : Quantity {
        protected VectorQuantity(Model model) : base(model) { }

        public abstract Vector3 Value(Vector3 position, float time);
    }
}