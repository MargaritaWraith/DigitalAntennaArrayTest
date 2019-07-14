using System;
using System.Collections.Generic;

namespace DigitalAntennaArray
{
    public class RadioScene : List<SpaceSource>
    {
        private const double LightSpeed = 300;

        double TimeDelay(double x, double y, double theta, double phi) => (x * Math.Cos(phi) + y * Math.Sin(phi)) * Math.Sin(theta) / LightSpeed;


        public Func<double, double> GetSignal(double x, double y)
        {
            return t =>
            {
                double result = 0;
                for (int i = 0; i < Count; i++)
                {
                    var space_signal = this[i];
                    result += space_signal.Signal.Value(t - TimeDelay(x, y, space_signal.Theta, space_signal.Phi));
                }

                return result;
            };
        }
    }
}