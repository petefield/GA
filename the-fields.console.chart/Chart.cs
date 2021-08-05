using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleEx.Chart {
    public class Chart {
        private readonly double _min;
        private readonly double _max;
        private readonly double _tick;

        private Dictionary<double, double?> _results;

        public Chart(double min, double max, double tick)
        {
            _min = min;
            _max = max;
            _tick = tick;
            _results = new Dictionary<double, double?>();

            for (var px = _min; px <= _max + _tick; px+= _tick)
            {
                _results.Add(px, null);
            }

            Render();
        }

        public void AddResult(double d, double v) => _results[d] = v;

        public IEnumerable<double> Ticks => _results.Keys;

        public void Render()
        {
            Console.Clear();
            var maxValue = _results.Max(x => x.Value);

            var scale = (Console.WindowWidth - 50) / maxValue;

            foreach (var (key, value) in _results.OrderByDescending(x => x.Key))
            {
                var o = Console.ForegroundColor;

                if (value is null)
                {
                    Console.WriteLine($"{key:0.000} |");
                }
                else
                {
                    if (value == _results.Min(x => x.Value))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    Console.WriteLine($"{key:0.000} | {new string('█', Convert.ToInt32(value * scale))} ({value:0.0})");
                }

                Console.ForegroundColor = o;

            }

            Console.WriteLine(new string('_',Console.WindowWidth  - 42));
        }
    }
}
