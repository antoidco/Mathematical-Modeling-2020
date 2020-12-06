﻿using System;
using UnityEngine;

//using System.Numerics;

namespace AircraftSimulator {
    public class Wind : VectorQuantity {
        private readonly Model _model;
         public float someParametere;

        public Wind(Model model) : base(model) {
            _model = model;
        }

        public override Vector3 Value(Vector3 position, double time) {
            if (_model is ConstantWindModel constantWindModel) return constantWindModel.Value;
            if (_model is TurbulentWindModel turbulentWindModel) return turbulentWindModel.Value(position, time, someParametere);
            throw new Exception("Invalid wind model");
        }
    }
}