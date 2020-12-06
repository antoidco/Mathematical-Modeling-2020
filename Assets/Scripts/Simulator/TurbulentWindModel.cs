using UnityEngine;

namespace AircraftSimulator
{
    public class TurbulentWindModel : Model
    {

        public TurbulentWindModel() : base(ModelType.Wind) {
        }

        public Vector3 Value(Vector3 position, double time, float someParametere) {
            var windValue = Vector3.zero;
            
            windValue.x = 0;
            windValue.y = 100*someParametere;
            windValue.z = 0;

            return windValue;
        }
    }
}