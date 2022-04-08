using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOffence
{
    public class GameModel
    {
        public List<Monster> availableMonsters = new List<Monster>() { new Monster() };

        public string map = @"    
CPP 
  P 
  PS";
    }
}
