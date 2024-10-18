namespace Zonk.Models
{
    public class Combination
    {
        public List<int> Value { get; init; }
        public int Price { get; private set; }

        public Combination(List<int> value, int price) 
        {
            Value = value;
            Price = price;
        }
        public override string ToString()
        {
            return $"Комбинация: {string.Join(", ", Value)} Очки: {Price}";
        }
    }
}
