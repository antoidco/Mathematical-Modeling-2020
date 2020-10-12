using System;

namespace AircraftSimulator {
    public abstract class Model {
        private ModelType _modelType;
        public ModelType Type => _modelType;

        public Model(ModelType modelType) {
            _modelType = modelType;
        }
    }
}