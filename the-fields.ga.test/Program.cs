using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GA.Test {
    static class Program {
        
        private static bool EvaluatePopulation(IEnumerable<TestMember> population)
        {
            var testMembers = population as TestMember[] ?? population.ToArray();
            Parallel.ForEach(testMembers, member =>
            {
                member.Run(null);
            });

            return testMembers.Max(m => m.Fitness) == 16777215;
        }

        private static void Main() {

            var ga = new GA<TestMember>(EvaluatePopulation);

            var s = System.IO.File.OpenWrite(@$"C:\Users\peter578\OneDrive - Willis Towers Watson\Documents\TestData\data.csv");
            var textWr = new System.IO.StreamWriter(s);

            var header = ",";

            var pM_min = 0d;
            var pM_max = 1;
            var pM_step = 0.01;
            
            var pX_min = 0.0;
            var pX_max = 1;
            var pX_step = 0.01;

            for (var h = pM_min; h <= pM_max; h += pM_step)
            {
                header += $"{h:0.000} , ";
            }

            textWr.WriteLine($"{header}");

            for (var pX = pX_min; pX <= pX_max; pX += pX_step)
            {
                var row = $"{pX:0.000} , ";

                for (var pM = pM_min; pM <= pM_max; pM += pM_step)
                {
                    var n = Enumerable.Range(0,10).Average(x => ga.Evolve(pX, pM, 100));
                    row += $"{n:0} , ";
                    Console.SetCursorPosition(0,0);
                    Console.WriteLine($"{pX:0.000} {pM:0.0000} - {n:0}        ");
                }
                textWr.WriteLine(row);
            }
            s.Close();

        }
    }
}
