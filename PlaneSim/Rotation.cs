namespace PlaneSim.PlaneSim {
    public class Rotation {
        // todo: Replace with Quaternion
        public double Yaw { get; set; }
        public double Pitch { get; set; }
        public double Roll { get; set; }

        public Rotation(double yaw, double pitch, double roll) {
            Yaw = yaw;
            Pitch = pitch;
            Roll = roll;
        }

        public Rotation() {
            Yaw = 0;
            Pitch = 0;
            Roll = 0;
        }
    }
}