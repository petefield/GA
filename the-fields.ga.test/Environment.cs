using System;
using System.Collections.Generic;

namespace GA.Test
{
    public class Environment
    {
        private object[,] map = new object[100,50];
        private IEnumerable<TestMember> _population;

        public void Initialise()
        {
            var rnd = new Random();

            for (var x = 0; x < 100; x++) 
            {
                for (var y = 0; y < 50; y++)
                {
                    map[x, y] = rnd.p(0.02) ? new Food() : null;
                }
            }
        }
        
        public void Render()
        {
            Console.SetCursorPosition(0,0);
            for (var y = 49; y >= 0; y--)
            {
                var row = "";
                for (var x = 0; x < 100; x++)
                {
                    row += map[x, y] switch
                    {
                        Food _ => "█",
                        TestMember _ => "*",
                        _ => "."
                    };
                }
                Console.WriteLine(row);
            }
        }

        public void Populate(IEnumerable<TestMember> population)
        {
            _population = population;
            var rnd = new Random();
            

            for (var y = 49; y >= 0; y--)
            {
                for (var x = 0; x < 100; x++)
                {
                    if (map[x, y] is TestMember) map[x, y] = null;
                }
            }

            foreach (var member in _population)
            {
                var x = rnd.Next(0, 99);
                var y = rnd.Next(0, 49);
                while (map[x, y] is TestMember)
                {
                    x = rnd.Next(0, 99);
                    y = rnd.Next(0, 49);
                }

                map[x, y] = member;
            }
        }

        public void Run(TimeSpan duration)
        {
            while (true)
            {
                foreach (var member in _population)
                {
                    member.Run(map);
                }
            }
        }
    }
}