using System;
using System.Linq;

namespace set
{
    public class Vector
    {
        private readonly int[] _coefficients;

        public Vector(int[] coefficients)
        {
            _coefficients = coefficients;
        }

        public Vector[] CreateSet(Vector direction)
        {
            var current = this;
            var result = new Vector[3];
            
            for (var i = 0; i < 3; i++)
            {
                result[i] = current;
                current = current.Add(direction);
            }

            return result;
        }

        public Vector Add(Vector direction)
        {
            if (_coefficients.Length != direction._coefficients.Length)
                throw new ArgumentException("The direction vector does not have the correct number of coefficients.");
            
            return new Vector(
                _coefficients
                    .Zip(direction._coefficients, (x1, x2) => ((x1 + x2) % 3))
                    .ToArray()
            );
        }

        public int GetIndex()
        {
            return _coefficients.Reverse().Aggregate(0, (a, b) => a * 3 + b);
        }

        /**
         * Determines whether this is the smallest possible representation of the direction, to avoid duplicates.
         */
        public bool IsFundamental()
        {
            return _coefficients.First(i => i != 0) == 1;
        }

        public static Vector FromIndex(int index, int n)
        {
            return new Vector(Enumerable.Range(0, n).Select(i =>
            {
                var coefficient = index % 3;
                index /= 3;
                return coefficient;
            }).ToArray());
        }

        public override string ToString()
        {
            return $"({string.Join(", ", _coefficients)})";
        }
    }
}