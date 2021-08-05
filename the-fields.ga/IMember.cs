using System.Collections.Generic;

namespace GA
{


    public interface IMember
    {
        int Fitness { get; }
        TMember Breed<TMember>(TMember mate, double pX, double pM) where TMember:IMember, new();
        void SetGenome<TGene>(TGene[] genes);

        TGene[] GetGenome<TGene>();
    }
}