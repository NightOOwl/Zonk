using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zonk.Models
{
    public class Player
    {
        public Player(string name)
        {
            Id = new Guid(); 
            Name = name;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool isActive { get; set; }
        public int Score { get; set; }
        public List<Dice>? Dices;
        private CombinationValidator validator;

        public void Activate()
        {
            isActive = true;
            Dices = new List<Dice>();
            for (int i = 0; i < 5; i++)
            {
                Dices.Add(new Dice());
            }
        }
        public void GetDices( List<Dice> dices) => Dices = dices;
        public void Throw()
        {
            if (isActive)
            {
                foreach (Dice dice in Dices)
                {
                    dice.Throw();
                }
            }
            else { Console.WriteLine("Сейчас не ваш ход.");}
        }
        public int? ChooseCombination(int combinationId)
        {
            if (isActive)
            {
                return combinationId;
            }
            else { Console.WriteLine("Сейчас не ваш ход."); return null; }
        }
        public void Deactivate()
        {
            isActive = false;
            Dices = null;
        }
        public override string ToString()
        {
            return $"Id: {Id}; Name: {Name}";
        }
    }
}
