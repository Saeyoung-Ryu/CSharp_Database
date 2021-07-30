using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    class Start1
    {
        public int a = 1;
        public int b = 2;
        public virtual void Set()
        {

        }
    }

    public struct Start3
    {
        static Start3 start3;
        Start1 start1;
        public int a;

        public  void Set(int _a)
        {
            start1 = new Start1();
            a = _a;
            a = 1;
            Set2();
            Console.WriteLine(a);
        }

        public void Set(int _a, string _b)
        {

        }
        void Set2()
        {
            a = 2;
        }
    }
    class Start 
    {
        
        int a;
        int b = 20;
       
        static void Main(string[] args)
        {
            Start3 start3 = new Start3();
            start3.Set();
            Console.WriteLine(start3.a);

        }

        void Ex1()
        {
            Start start3 = new Start();

            start3.a = 5;
            start3.Ex();
            
            Console.WriteLine(start3.a + "this" + this.a);
        }

        void Ex()
        {
            Start start = new Start();
            start.a = 4;
            this.a = 3; //as you learned in singleton, do not use this when there is another object.

            //Console.WriteLine(start.a + "는" + this.a);

            Start3 start3 = new Start3();
            start3.Set();
            
        
        }

    }
}
