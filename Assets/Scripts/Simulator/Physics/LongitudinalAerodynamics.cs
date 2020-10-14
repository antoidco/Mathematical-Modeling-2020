using UnityEngine;

namespace AircraftSimulator.Physics {
    public class LongitudinalAerodynamics {
        public static Vector3 Evaluate(LongitudinalAerodynamicsInputData input) {
            var result = new Vector3();
            var derivativeMatrix = new Matrix3x3();
            derivativeMatrix.Value[0][2] = input.XqDot;
            derivativeMatrix.Value[1][2] = input.ZqDot;
            derivativeMatrix.Value[2][1] = input.MqDot;
            derivativeMatrix.Value[2][2] = input.MwDot;
            var derivativeVector = new Vector3(input.uDot, input.wDot, input.qDot);

            var derivativeResult = EvaluationHelper.Multiply(derivativeMatrix, derivativeVector);

            var mainMatrix = new Matrix3x3();
            mainMatrix.Value[0][0] = input.Xu;
            mainMatrix.Value[0][1] = input.Xw;
            mainMatrix.Value[0][2] = 0;
            mainMatrix.Value[1][0] = input.Zu;
            mainMatrix.Value[1][1] = input.Zw;
            mainMatrix.Value[1][2] = input.Zq;
            mainMatrix.Value[2][0] = input.Mu;
            mainMatrix.Value[2][1] = input.Mw;
            mainMatrix.Value[2][2] = input.Mq;
            var mainVector = new Vector3(input.u, input.w, input.q);

            var mainResult = EvaluationHelper.Multiply(mainMatrix, mainVector);

            var additiveResult = input.deltaE * new Vector3(input.XdE, input.ZdE, input.MdE);

            result = derivativeResult + mainResult + additiveResult;
            return result;
        }

        public struct LongitudinalAerodynamicsInputData {
            public float XqDot, ZqDot, MqDot, MwDot;
            public float uDot, wDot, qDot;
            public float Xu, Xw, Zu, Zw, Zq, Mu, Mw, Mq;
            public float u, w, q;
            public float deltaE; // Controls !
            public float XdE, ZdE, MdE;
        }
    }
}