
namespace Zonk.Models
{
    public class CombinationValidator
    {
        public List<Combination> FindAllCombinations( List<Dice> dices)
        {
            var pool = dices.Select(x => x.Value).Order();
            var combos = new List<Combination>();
            var group = pool.GroupBy(x => x);
            if (group.Count() == 5)
            {
                var range1 = Enumerable.Range(1, 5);
                var range2 = Enumerable.Range(2, 5);
                var sequence = pool.Distinct().ToList();
                if (sequence.SequenceEqual(range1) || sequence.SequenceEqual(range2))
                {
                    var smallRangeCombo = new Combination(sequence, 1500);
                    combos.Add(smallRangeCombo);
                }
            }
            if (group.Count() == 6)
            {
                var bigRangeCombo = new Combination(pool.Distinct().ToList(), 2000);
                combos.Add(bigRangeCombo);
            }
            if (group.Where(x => x.Count() >= 3).Any())
            {
                var sets = group.Where(x => x.Count() >= 3);
                foreach (var set in sets)
                {
                    int bonus;
                    if (set.FirstOrDefault() == 1)
                    {
                        bonus = (set.Count() - 2) * 1000;
                    }
                    else
                    {
                        bonus = (set.Count() - 2) * 100;
                    }

                    var setCombo = new Combination(set.ToList(), price: (set.FirstOrDefault() * bonus));
                    combos.Add(setCombo);
                }
            }
            foreach (var dice in pool.Where(x => x == 1 || x == 5))
            {
                var singeDiceCombo = new Combination(new List<int>() { dice }, dice == 1 ? dice * 100 : dice * 10);
                combos.Add(singeDiceCombo);
            }
            return combos;
        }
    }
}
