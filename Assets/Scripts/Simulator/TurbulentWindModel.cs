using UnityEngine;

namespace AircraftSimulator
{
    public class TurbulentWindModel : Model
    { 
        public TurbulentWindModel() : base(ModelType.Wind) { }

        public Vector3 Value(Vector3 positionOfAircraft, float time) {
            float x = positionOfAircraft.x;
            float y = positionOfAircraft.y;
            float z = positionOfAircraft.z;

            var windValue = Vector3.zero;
            float x0 = 150*Mathf.Cos(2*time/10); //position of peak turbulent wind 
            float y0 = 300*Mathf.Sin(4*time/10);
            float z0 = 50;

            float su = 1; //constant wind value
            float sv = 0.5f;
            float sw = 0.1f;

            var Lu = 30; //influence of turbulent wind
            var Lv = 50;
            var Lw = 20;

            windValue.x = su + su * su * Lu / (Mathf.PI * (1 + Mathf.Pow(Lu * (x-x0), 2)));
            windValue.y = sv + sv * sv * Lv * (1 + 12 * Mathf.Pow(Lv * (y-y0), 2)) / Mathf.Pow(Mathf.PI * (1 + 4 * Mathf.Pow(Lv * (y-y0), 2)), 2);
            windValue.z = sw + sw * sw * Lw * (1 + 12 * Mathf.Pow(Lw * (z-z0), 2)) / Mathf.Pow(Mathf.PI * (1 + 4 * Mathf.Pow(Lw * (z-z0), 2)), 2);

            return windValue;
        }
    }
}