using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalAntennaArray
{
    class Program
    {
        private const double LightSpeed = 300;

        static void Main(string[] args)
        {

            const int Nx = 16;
            const int Ny = 16;

            const double F0 = 3;
            const double lambda = LightSpeed / F0;
            const double dx = lambda / 2;
            const double dy = lambda / 2;

            const double Lx = Nx * dx;
            const double Ly = Ny * dy;

            const double f_d = 4 * F0;
            const int N_d = 1024;

            const double to_rad = Math.PI / 180;

            var radio_scene = new RadioScene();
            radio_scene.Add(new SpaceSource(0 * to_rad, 0, new Cos(1, F0, 0)));

            Signal[,] input_signals = new Signal[Nx, Ny];

            ComputeInputSignals(radio_scene, input_signals, dx, dy);

            var DigitalSignals = DiscretalizeSignal(input_signals, f_d, N_d);

            var powers = GetDigitalPower(DigitalSignals);


        }

        static void ComputeInputSignals(RadioScene RadioScene, Signal[,] InputSignals, double dx, double dy)
        {

            for (int i = 0; i < InputSignals.GetLength(0); i++)
            {
                for (int j = 0; j < InputSignals.GetLength(1); j++)
                {
                    InputSignals[i, j] = new Signal(RadioScene.GetSignal(dx * i, dy * j));
                }

            }
        }

        static double[,][] DiscretalizeSignal(Signal[,] AnalogSignals, double f_d, int N_d)
        {
            double[,][] result = new double[AnalogSignals.GetLength(0),AnalogSignals.GetLength(1)][];
            for (int i = 0; i < AnalogSignals.GetLength(0); i++)
            {
                for (int j = 0; j < AnalogSignals.GetLength(1); j++)
                {
                    var analog_signal = AnalogSignals[i, j];
                    result[i, j] = DiscretalizeSignal(analog_signal, 1 / f_d, N_d);
                }
            }

            return result;
        }

        static double[] DiscretalizeSignal(Signal AnalogSignal, double dt, int N_d)
        {
            double[] result = new double[N_d];

            for (int i = 0; i < N_d; i++)
            {
                result[i] = AnalogSignal.Value(dt * i);
            }

            return result;
        }

        static double[,] GetDigitalPower(double[,][] digital_signals)
        {
            double[,] result = new double[digital_signals.GetLength(0),digital_signals.GetLength(1)];
            for (int i = 0; i < digital_signals.GetLength(0); i++)
            {
                for (int j = 0; j < digital_signals.GetLength(1); j++)
                {
                    var signal = digital_signals[i, j];
                    result[i, j] = Power(signal);
                }
            }

            return result;
        }

        static double Power(double[] signal)
        {
            double power = 0;
            for (int i = 0; i < signal.Length; i++)
            {
                power += signal[i] * signal[i];
            }

            return power / signal.Length;
        }

    }
}
