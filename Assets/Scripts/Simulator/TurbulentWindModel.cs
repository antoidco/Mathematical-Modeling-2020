using UnityEngine;

namespace AircraftSimulator
{
    public class TurbulentWindModel : Model
    { 
        public TurbulentWindModel() : base(ModelType.Wind) { }

        public Vector3 Value(Vector3 position, double time) {
            var x = position.x;
            var y = position.y;
            var z = position.z;
            var windValue = Vector3.zero;

            var su = 2;
            var sv = 1;
            var sw = 3;

            var Lu = 8;
            var Lv = 16;
            var Lw = 4;

            windValue.x = su * su * Lu / (Mathf.PI * (1 + Mathf.Pow(Lu * x, 2)));
            windValue.y = sv * sv * Lv * (1 + 12 * Mathf.Pow(Lv * y, 2)) / Mathf.Pow(Mathf.PI * (1 + 4 * Mathf.Pow(Lv * y, 2)), 2);
            windValue.z = sw * sw * Lw * (1 + 12 * Mathf.Pow(Lw * z, 2)) / Mathf.Pow(Mathf.PI * (1 + 4 * Mathf.Pow(Lw * z, 2)), 2);

            return windValue;
        }
    }
}