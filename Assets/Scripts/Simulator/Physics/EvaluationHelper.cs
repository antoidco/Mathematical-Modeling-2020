using System;
using UnityEngine;

namespace AircraftSimulator.Physics {
    public class EvaluationHelper {
        public static Vector3 Multiply<T>(T matrix, Vector3 vector) where T : Matrix {
            var result = new Vector3();
            for (int i = 0; i < matrix.Value.Length; ++i) {
                for (int j = 0; j < matrix.Value[i].Length; ++j) {
                    result[i] += matrix.Value[i][j] * result[j];
                }
            }
            return result;
        }
        public static float ClampByModule(float value, float module) {
            if (Math.Abs(value) > module) {
                return Math.Sign(value) * module;
            }
            else {
                return value;
            }
        }
    }

    public interface Matrix {
        float[][] Value { get; set; }
    }
    public class Matrix3x3 : Matrix {
        public float[][] Value { get; set; }

        public Matrix3x3() {
            Value = new float[3][];
            for (int i = 0; i < 3; ++i) {
                Value[i] = new float[3];
            }
        }
    }
    public class Matrix3x2 : Matrix {
        public float[][] Value { get; set; }

        public Matrix3x2() {
            Value = new float[3][];
            for (int i = 0; i < 3; ++i) {
                Value[i] = new float[2];
            }
        }
    }
}