namespace AircraftSimulator.Physics.Basic
{
    public struct PDControl
    {
        private readonly double _dCoef;
        private readonly double _pCoef;
        private readonly double _iCoef;

        public PDControl(double dCoef, double pCoef, double iCoef = 0)
        {
            _dCoef = dCoef;
            _pCoef = pCoef;
            _iCoef = iCoef;
            D = 0;
            P = 0;
            I = 0;
        }

        public double D { get; private set; }

        public double P { get; private set; }

        public double I { get; private set; }

        public void Update(double current, double desired, double currentD, double desiredD)
        {
            P = (desired - current) * _pCoef;
            D = (desiredD - currentD) * _dCoef;
            I += (desired - current) * _iCoef;
        }

        public void ResetI()
        {
            I = 0;
        }
    }
}