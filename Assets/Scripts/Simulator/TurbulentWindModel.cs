using UnityEngine;

namespace AircraftSimulator
{
    public class TurbulentWindModel : Model
    { 
        public TurbulentWindModel(Vector3 PositionValue) : base(ModelType.Wind)
        {
            var x = PositionValue.x;
            var y = PositionValue.y;
            var z = PositionValue.z;
            var WindValue = Vector3.zero;

            var su = 2;
            var sv = 1;
            var sw = 3;

            var Lu = 8;
            var Lv = 16;
            var Lw = 4;

            WindValue.x = su * su * Lu / (Mathf.PI * (1 + Mathf.Pow(Lu * x, 2)));
            WindValue.y = sv * sv * Lv * (1 + 12 * Mathf.Pow(Lv * y, 2)) / Mathf.Pow(Mathf.PI * (1 + 4 * Mathf.Pow(Lv * y, 2)), 2);
            WindValue.z = sw * sw * Lw * (1 + 12 * Mathf.Pow(Lw * z, 2)) / Mathf.Pow(Mathf.PI * (1 + 4 * Mathf.Pow(Lw * z, 2)), 2);

            Value = WindValue;
        }

        public Vector3 Value { get; set; }
    }
}