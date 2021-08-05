using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GA
{
    public class GA<T> where T:IMember, new(){
        private readonly Func<T[],bool> _evaluatePopulation;
        private double _pX;
        private double _pM;

        public GA(Func<T[],bool> evaluatePopulation)
        {
            _evaluatePopulation = evaluatePopulation;
        }

        public int Evolve(double pX, double pM,  int maxGenerations)
        {
            _pX = pX;
            _pM = pM;
            int generation = 0;
            var pop = Enumerable.Range(0,100).Select(x => new T()).ToArray();
            bool halt = false; 

            while (!halt && (generation < maxGenerations ))
            {
                pop = CreateNewPopulation(pop);
                halt = _evaluatePopulation(pop);
                generation++;
            }

            return generation;
        }

        private T[] CreateNewPopulation(T[] population)
        {
            var perc = population.Length / 2;

            Array.Sort(population);

            for (var i = 1; i < perc; i++)
            {
                population[perc + i] = population[i-1].Breed(population[i], _pX, _pM);
            }

            return population;
        }

    }
}