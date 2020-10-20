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
            _quaternion = Quaternion.Euler(_pitch,  -_roll,  _yaw);
        }
        public Rotation(Quaternion quaternion) {
            Yaw = quaternion.eulerAngles.y;
            Pitch = -quaternion.eulerAngles.x;
            Roll = quaternion.eulerAngles.z;
            _quaternion = quaternion;
        }

        public Rotation() : this(0, 0, 0) { }

        public double Yaw {
            get => Clamp180(_yaw);
            set {
                _yaw = (float) value;
                _quaternion = Quaternion.Euler(_pitch, -_roll, _yaw);
            }
        }

        public double Pitch {
            get => Clamp180(_pitch);
            set {
                _pitch = (float) value;
                _quaternion = Quaternion.Euler(_pitch, -_roll, _yaw);
            }
        }

        public double Roll {
            get => Clamp180(_roll);
            set {
                _roll = (float) value;
                _quaternion = Quaternion.Euler(_pitch, -_roll, _yaw);
            }
        }

        public Quaternion Quaternion {
            get => _quaternion;
            set {
                _quaternion = value;
                _pitch = _quaternion.eulerAngles.x;
                _roll = -_quaternion.eulerAngles.y;
                _yaw = _quaternion.eulerAngles.z;
            }
        }

        private float Clamp180(float value) {
            if (Mathf.Abs(value) > 180f) {
                if (value < 0) return value + 360f;
                return value - 360f;
            }
            return value;
        }
    }
}