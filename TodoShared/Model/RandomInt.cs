using System;
using System.Collections.Generic;
using System.Text;

namespace TodoLocalized.Model
{
    class RandomInt
    {
        public Double rand()
        {
            Random rnd = new Random();
            return rnd.NextDouble();
        }
    }
}
