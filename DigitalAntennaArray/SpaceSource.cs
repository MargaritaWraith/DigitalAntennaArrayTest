namespace DigitalAntennaArray
{
    public class SpaceSource
    {
        private readonly double _Theta;
        private readonly double _Phi;
        private readonly Signal _Signal;

        public Signal Signal => _Signal;
        public double Theta => _Theta;
        public double Phi => _Phi;

        public SpaceSource(double theta, double phi, Signal signal)
        {
            _Theta = theta;
            _Phi = phi;
            _Signal = signal;
        }
    }
}