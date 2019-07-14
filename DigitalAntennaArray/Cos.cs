using System;

namespace DigitalAntennaArray
{
    public class Cos : Signal
    {
        public Cos(double A0, double f0, double phi0) : base(t => A0 * Math.Cos(2 * Math.PI * t * f0 + phi0)) { }
    }
}