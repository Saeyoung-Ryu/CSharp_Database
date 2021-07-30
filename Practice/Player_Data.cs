using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    class Player_Data
    {
        enum Type { melee_attack, ranged_attack, tank}

        Type type;
        public int attack;
        public int health;
        public int currentLevel;
        public int maxLevel;
        public int currentEXP;
        public int attackRange;
        public int[] maxEXP = new int[20];

        public Player_Data(int _attack, int _health, int _currentlevel, int _currentEXP, int _attackRange)
        {
            attack = _attack;
            health = _health;
            currentLevel = _currentlevel;
            currentEXP = _currentEXP;
            attackRange = _attackRange;
        }
    }

    class Ashe : Attack, LevelUp, Dead, Potion_Heal
    {
        Player_Data ashe = new Player_Data(60,500,1,0,600);
        void ItemSet()
        {
            
        }

        public void Attack()
        {
            
        }

        public void Dead()
        {
            
        }

        public void Heal()
        {
            
        }

        public void LevelUp()
        {
            
        }
    }
}
