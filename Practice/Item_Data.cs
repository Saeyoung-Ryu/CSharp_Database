using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    class Item_Data
    {
        

        public int plus_attack;
        public int plus_maxHealth;
        public int plus_currentHealth;
        public static Dictionary<string, Item_Data> item_dictionary = new Dictionary<string, Item_Data>();

        public Item_Data(int _plus_attack, int _plus_maxHealth, int _plus_currentHealth)
        {
            plus_attack = _plus_attack;
            plus_maxHealth = _plus_maxHealth;
            plus_currentHealth = _plus_currentHealth;
        }
    }

    class Sword
    {
        Item_Data sword = new Item_Data(15,0,0);

        public void AddSword()
        {
            Item_Data.item_dictionary.Add("sword", sword);
        }
        

    }

    class Shield
    {
        Item_Data shield = new Item_Data(0, 100, 0);

        public void AddShield()
        {
            Item_Data.item_dictionary.Add("shield", shield);
        }
    }

    class Potion 
    {
        Item_Data potion = new Item_Data(0, 0, 50);

        public void AddPotion()
        {
            Item_Data.item_dictionary.Add("potion", potion);
        }
    }

}
