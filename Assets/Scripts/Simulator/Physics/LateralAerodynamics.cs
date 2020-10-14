using UnityEngine;

namespace AircraftSimulator.Physics {
    public class LateralAerodynamics {
        public static Vector3 Evaluate(LateralAerodynamicsInputData input) {
            var result = new Vector3();
            var derivativeMatrix = new Matrix3x3();
            
            derivativeMatrix.Value[1][1] = input.LpDot;
            derivativeMatrix.Value[1][2] = input.NpDot;
            var derivativeVector = new Vector3(input.vDot, input.pDot, input.rDot);

            var derivativeResult = EvaluationHelper.Multiply(derivativeMatrix, derivativeVector);

            var mainMatrix = new Matrix3x3();
            mainMatrix.Value[0][0] = input.Yv;
            mainMatrix.Value[0][1] = 0;
            mainMatrix.Value[0][2] = 0;
            mainMatrix.Value[1][0] = input.Lv;
            mainMatrix.Value[1][1] = input.Lp;
            mainMatrix.Value[1][2] = input.Lr;
            mainMatrix.Value[2][0] = input.Nv;
            mainMatrix.Value[2][1] = input.Np;
            mainMatrix.Value[2][2] = input.Nr;
            var mainVector = new Vector3(input.v, input.p, input.r);

            var mainResult = EvaluationHelper.Multiply(mainMatrix, mainVector);

            var additiveMatrix = new Matrix3x2();
            additiveMatrix.Value[0][0] = input.YdA;
            additiveMatrix.Value[0][1] = input.YdR;
            additiveMatrix.Value[1][0] = input.LdA;
            additiveMatrix.Value[1][1] = input.LdR;
            additiveMatrix.Value[2][0] = input.NdA;
            additiveMatrix.Value[2][1] = input.NdR;
            
            var additiveVector = new Vector2(input.deltaA, input.deltaR);

            result = derivativeResult + mainResult + EvaluationHelper.Multiply(additiveMatrix, additiveVector);
            
            return result;
        }

        public struct LateralAerodynamicsInputData {
            public float LpDot, NpDot;
            public float vDot, pDot, rDot;
            public float Yv, Lv, Lp, Lr, Nv, Np, Nr;
            public float v, p, r;
            public float deltaA, deltaR; // Controls !
            public float YdA, YdR, LdA, LdR, NdA, NdR;
        }
    }
}