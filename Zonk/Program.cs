using System.Threading.Channels;
using Zonk.Models;

namespace Zonk
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager manager = new GameManager();
            manager.InitGame();
            manager.GameLoop();
        }
        public static void PrintCollection(IEnumerable<int> values)
        => Console.WriteLine(string.Join(",", values));
       
    }
}
