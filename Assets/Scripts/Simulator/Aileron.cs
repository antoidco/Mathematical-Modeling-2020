using UnityEngine;

namespace AircraftSimulator {
    public class Aileron : Component {
        public Quaternion Direction { get; private set; }
        public Aileron(Quaternion direction, float mass) {
            Mass = mass;
            Direction = direction;
        }
    }
}