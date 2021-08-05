using System;
using System.Collections;
using System.Linq;

namespace GA.Test
{
    public class TestMember : Member<bool>
    {
        public static int GenomeLength = 24;

        public TestMember()
        {
            this.Genome = new bool[GenomeLength];
        }

        protected override bool Mutate()
        {
            var rd = new Random();
            return rd.Next(0, 2) == 0;
        }

        public override string ToString()
        {
            var genome = string.Concat(Genome.Select(x => x ? "1" : "0"));
            return $"{genome} : {Fitness}";
        }

        public void Run(object[,] environment)
        {
            var ba = new BitArray(Genome.Reverse().ToArray());
            int[] ints = new int[1];
            ba.CopyTo(ints, 0);
            Fitness = ints[0];
        }
    }
}