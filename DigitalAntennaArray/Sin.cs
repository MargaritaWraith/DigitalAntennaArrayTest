using System;

namespace DigitalAntennaArray
{
    public class Sin : Signal
    {
        public Sin(double A0, double f0, double phi0) : base(t => A0 * Math.Sin(2 * Math.PI * t * f0 + phi0)){}
    }
}