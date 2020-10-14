using UnityEngine;

namespace AircraftSimulator
{
    public class ConstantWindModel : Model
    {
        public ConstantWindModel(Vector3 constantWindValue) : base(ModelType.Wind)
        {
            Value = constantWindValue;
        }

        public Vector3 Value { get; set; }
    }
}