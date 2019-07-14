using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalAntennaArray
{
    public class Signal
    {
        private readonly Func<double, double> _Func;

        public Signal(Func<double, double> func) => _Func = func;

        public double Value(double t)
        {
            return _Func(t);
        }
    }
}
