using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zonk.Models;

namespace Zonk
{
    public class GameManager
    {
        public GameManager()
        {
            PlayerQueue = new LoopedQueue<Player>();   
            validator = new CombinationValidator();
        }
        private bool inGame = false;
        public LoopedQueue<Player> PlayerQueue { get; private set; }
        private CombinationValidator validator;
        private int winScore = 4000; 
        public void InitGame()
        {
            var stopWord = "всё";
            Console.WriteLine("Начинается набор участников");
            do
            {
                Console.WriteLine("Укажите имя игрока");
                var name = Console.ReadLine();
                if (name == stopWord)
                {
                    break;
                }
                PlayerQueue.Add(new Player(name ?? $"defaultPlayer{PlayerQueue.Count + 1}"));

            } while (true);
            Console.WriteLine("Набор завершен");
            if (!PlayerQueue.IsEmpty)
            {
                inGame = true;
            }
            else
            {
                Console.WriteLine("Никто не захотел участвовать в игре. Игра отменяется.");
                return;
            }
        }
        public List<Dice> GiveNewDices()
        {
            var dices = new List<Dice>();
            for (int i = 0; i < 6; i++)
            {
                dices.Add(new Dice());
            }
            return dices;
        }

        public void GameLoop()
        {
            while (inGame)
            {
                var activePlayer = PlayerQueue.Next().Data;
                activePlayer.Activate();
                var roundScore = 0;
                var rethrowsCount = 0;
                activePlayer.GetDices(GiveNewDices());
                while (true)
                {
                    Console.WriteLine($"Кубики бросает: {activePlayer.Name}");
                    activePlayer.Throw();
                    PrintThrowResult(activePlayer.Dices);
                    
                    var throwResult = validator.FindAllCombinations(activePlayer.Dices);
                    var comboDict = throwResult.ToDictionary(throwResult.IndexOf, x => x);
  
                    if (comboDict.Count == 0)
                    {
                        Console.WriteLine("Плохой бросок, сожалею...");
                        roundScore = 0;
                        break;
                    }
                    Console.WriteLine("Выбери комбинацию из списка:");
                    foreach (var combo in comboDict)
                    {
                        Console.WriteLine($"{combo.Key}) {combo.Value}");
                    }
                    var choise = int.Parse(Console.ReadLine());
                    roundScore += ApplyChoise(choise, activePlayer.Dices, comboDict);

                    while (true)
                    {
                        PrintThrowResult(activePlayer.Dices);
                        throwResult = validator.FindAllCombinations(activePlayer.Dices);
                        comboDict = throwResult.ToDictionary(throwResult.IndexOf, x => x);
                        if (comboDict.Count == 0)
                        {
                            break;
                        }
                        Console.WriteLine("Ты можешь выбрать еще: ");
                        foreach (var combo in comboDict)
                        {
                            Console.WriteLine($"{combo.Key}) {combo.Value}");
                        }
                        var ok = int.TryParse(Console.ReadLine(), out choise);
                        if (ok)
                        {
                             roundScore += ApplyChoise(choise, activePlayer.Dices, comboDict);
                        }
                        else
                        {
                            break;
                        }
                    }
                    Console.WriteLine("Ты можешь остановиться и зафиксировать очки за раунд (стоп)");
                    if (Console.ReadLine() == "стоп")
                    {
                        break;
                    }
                    if (activePlayer.Dices.Count == 0)
                    {
                        activePlayer.GetDices(GiveNewDices());
                    }
                }
                activePlayer.Score += roundScore;
                Console.WriteLine($"{activePlayer.Name} Завершает ход. Очки игрока: {activePlayer.Score}");
                activePlayer.Deactivate();
                if (activePlayer.Score >= winScore)
                {
                    Console.WriteLine($"{activePlayer.Name} набрал[а] достаточно очков для победы. Поздравляем!");
                    inGame = false;
                }
            }
        }
        private void PrintThrowResult(List<Dice> dices)
        {
            Console.WriteLine(string.Join(",", dices.Select(x=>x.ToString())));
        }
        private int ApplyChoise(int combinationId, List<Dice> dices, Dictionary<int, Combination> throwResult)
        {
            var ok = throwResult.TryGetValue(combinationId, out var combination);
            if (ok)
            {
                foreach (var dice in combination.Value)
                {
                    dices.Remove(dices.Where(x => x.Value == dice).First());
                }
                return combination.Price;
            }
            else { return 0; }
        }
    }
}
