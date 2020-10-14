using System.Collections.Generic;
using UnityEngine;

namespace AircraftSimulator {
    public class Wing : Component {
        public Wing(Vector3 relativePosition, Quaternion rotation, double mass, Vector3 drag) {
            RelativePosition = relativePosition;
            Rotation = rotation;
            Drag = drag;
        }

        public Quaternion Rotation { get; set; }
        public Vector3 Drag { get; set; }
        public List<Aileron> Elerons { get; set; }
    }
}