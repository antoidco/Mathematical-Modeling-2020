using UnityEngine;

namespace AircraftSimulator
{
    public class Rotation
    {
        // todo: Replace with Quaternion
        private float _yaw, _pitch, _roll;

        public Rotation(double yaw, double pitch, double roll)
        {
            Yaw = yaw;
            Pitch = pitch;
            Roll = roll;
            RQuat = Quaternion.Euler(-(float) Pitch, (float) Yaw, (float) Roll);
        }

        public Rotation()
        {
            Yaw = 0;
            Pitch = 0;
            Roll = 0;
            RQuat = Quaternion.Euler(-(float) Pitch, (float) Yaw, (float) Roll);
        }

        public double Yaw
        {
            get => _yaw;
            set
            {
                _yaw = (float) value;
                RQuat = Quaternion.Euler(-_pitch, _yaw, _roll);
            }
        }

        public double Pitch
        {
            get => _pitch;
            set
            {
                _pitch = (float) value;
                RQuat = Quaternion.Euler(-_pitch, _yaw, _roll);
            }
        }

        public double Roll
        {
            get => _roll;
            set
            {
                _roll = (float) value;
                RQuat = Quaternion.Euler(-_pitch, _yaw, _roll);
            }
        }

        public Quaternion RQuat { get; set; }
    }
}