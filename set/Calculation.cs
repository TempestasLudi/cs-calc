using System;
using System.Linq;

namespace set
{
    public class Calculation
    {
        private int _n;
        private Deck _deck;

        public Calculation(int n)
        {
            _n = n;
           
            _deck = new Deck(n);
        }

        public int CountSolutions()
        {
            if (!_deck.HasFreeCards()) return 1;
            var setStart = _deck.GetFirstFreeCard();
            
            var result = 0;
            foreach (var direction in _deck.Directions)
            {
                var set = setStart.CreateSet(direction);
                
//                Console.WriteLine(string.Join(", ", set.Select(v => v.ToString())));
                
                if (!_deck.AreFree(set)) continue;
                
                _deck.Set(set, false);
                
                result += CountSolutions();
                
                _deck.Set(set, true);
            }

            return result;
        }
    }
}