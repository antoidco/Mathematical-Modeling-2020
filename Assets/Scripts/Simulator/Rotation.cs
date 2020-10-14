using UnityEngine;

namespace AircraftSimulator {
    public class Rotation {
        // todo: Replace with Quaternion
        private float _yaw, _pitch, _roll;
        private Quaternion _quaternion;

        public Rotation(float yaw, float pitch, float roll) {
            _yaw = yaw;
            _pitch = pitch;
            _roll = roll;
            _quaternion = Quaternion.Euler(-_pitch,  _yaw,  _roll);
        }
        public Rotation(Quaternion quaternion) {
            Yaw = quaternion.eulerAngles.y;
            Pitch = -quaternion.eulerAngles.x;
            Roll = quaternion.eulerAngles.z;
            _quaternion = quaternion;
        }

        public Rotation() : this(0, 0, 0) { }

        public double Yaw {
            get => _yaw;
            set {
                _yaw = (float) value;
                _quaternion = Quaternion.Euler(-_pitch, _yaw, _roll);
            }
        }

        public double Pitch {
            get => _pitch;
            set {
                _pitch = (float) value;
                _quaternion = Quaternion.Euler(-_pitch, _yaw, _roll);
            }
        }

        public double Roll {
            get => _roll;
            set {
                _roll = (float) value;
                _quaternion = Quaternion.Euler(-_pitch, _yaw, _roll);
            }
        }

        public Quaternion Quaternion {
            get => _quaternion;
            set {
                _quaternion = value;
                _pitch = -_quaternion.eulerAngles.x;
                _yaw = _quaternion.eulerAngles.y;
                _roll = _quaternion.eulerAngles.z;
            }
        }
    }
}