
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GA
{
    public abstract class Member<T> : IMember, IComparable
    {
        public Member(){}

        protected Member(T[] genome)
        {
            this.Genome = genome;
        }

        public T[] Genome { get; set; }

        
        public TMember Breed<TMember>(TMember mate, double pX, double pM) where TMember:IMember, new()
        {
            var offSpring = new TMember();
            var genome = CombineGenomes(mate, pX, pM);
            
            offSpring.SetGenome( genome);
            return offSpring;
        }

        public void SetGenome<TGene>(TGene[] genes)
        {
            Genome = genes.Cast<T>().ToArray();
        }

        public TGene[] GetGenome<TGene>()
        {
            return Genome.Cast<TGene>().ToArray();
        }


        public int Fitness { get; set; }
       
        public T this[int index] => this.Genome[index];

        public abstract override string ToString();
        public int CompareTo(object? obj)
        {
            if(!(obj is Member<T> o)) throw new InvalidOperationException();
            return o.Fitness.CompareTo(Fitness);
        }

        private T[] CombineGenomes<TMember>(TMember mate, double pX, double pM) where TMember:IMember, new()
        {  
            var rnd = new Random();
            var swapSource = true;
            var genes = Enumerable.Range(0, Genome.Length)
                .Select(x =>
                {
                    if (rnd.p(pX)) swapSource = !swapSource;
                    return rnd.p(pM) ? Mutate() : swapSource ?this[x] : mate.GetGenome<T>() [x] ;
                })
                .ToArray();
            return genes;
        }

        protected abstract T Mutate();
    }
}