using System.Collections.Generic;
using System.Linq;

namespace set
{
    internal class Deck
    {
        private readonly int _n;
        private readonly int[] _layerSizes;
        private SortedSet<int> _freeCards;
        public Vector[] Directions;

        public Deck(int n)
        {
            _n = n;
            _layerSizes = Enumerable.Range(0, n + 1)
                .Select(i => Pow(3, i))
                .ToArray();
            _freeCards = new SortedSet<int>(Enumerable.Range(0, _layerSizes[n]));

            Directions = Enumerable.Range(1, _layerSizes[n] - 1)
                .Select(i => Vector.FromIndex(i, _n))
                .Where(direction => direction.IsFundamental())
                .ToArray();
        }

        public bool HasFreeCards()
        {
            return _freeCards.Count > 0;
        }

        public Vector GetFirstFreeCard()
        {
            return Vector.FromIndex(_freeCards.First(), _n);
        }

        public void Set(IEnumerable<Vector> cards, bool free)
        {
            if (free)
                foreach (var card in cards)
                    _freeCards.Add(card.GetIndex());
            else
                foreach (var card in cards)
                    _freeCards.Remove(card.GetIndex());
        }

        public bool AreFree(IEnumerable<Vector> cards)
        {
            return cards.Select(card => _freeCards.Contains(card.GetIndex()))
                .Aggregate((a, b) => a && b);
        }

        private static int Pow(int b, int e)
        {
            if (e == 0) return 1;
            var result = Pow(b, e / 2);
            result *= result;
            if ((e & 1) == 1) result *= b;
            return result;
        }
    }
}