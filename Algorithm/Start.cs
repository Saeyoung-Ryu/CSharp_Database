using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    
    //class Node_
    //{
    //    public static Queue<Node_> queue = new Queue<Node_>();

    //    public string value;

    //    public Node_ left_node;
    //    public Node_ right_node;

    //    public Node_(string _value)
    //    {
    //        value = _value;
    //    }
    //}

    //class Search
    //{
    //    public static string valuetoFind;
    //    public static void DFS(Node_ node)
    //    {
            

    //        if(node.left_node != null)
    //        {
    //            if(node.left_node.value == valuetoFind)
    //            {
    //                Console.WriteLine("값이 존재합니다");
    //                return;
    //            }                
    //        }
            
    //        if(node.right_node != null)
    //        {
    //            if(node.right_node.value == valuetoFind)
    //            {
    //                Console.WriteLine("값이 존재합니다");
    //                return;
    //            }
    //            else
    //            {
    //                if (node.left_node != null)
    //                {
    //                    Node_.queue.Enqueue(node.left_node);
    //                }
    //                if (node.right_node != null)
    //                {
    //                    Node_.queue.Enqueue(node.right_node);
    //                }

    //                DFS(Node_.queue.Dequeue());
    //            }
    //        }

    //        //Console.WriteLine("값이 존재하지 않습니다");
    //    }
    //}

    //class Node
    //{

    //    public int value;

    //    public Node left_node;
    //    public Node right_node;

    //    public bool isRoot;

    //    public Node(int _value)
    //    {
    //        value = _value;
    //    }
    //}

    //class BinaryTree 
    //{
    //    public static void Insert(Node node, Node node_inserted)
    //    {
    //        if(node.value < node_inserted.value)
    //        {
    //            if(node.right_node == null)
    //            {
    //                node.right_node = node_inserted;
    //            }
    //            else
    //            {
    //                Insert(node.right_node, node_inserted);
    //            }
    //        }
    //        else if(node.value > node_inserted.value)
    //        {
    //            if(node.left_node == null)
    //            {
    //                node.left_node = node_inserted;
    //            }
    //            else
    //            {
    //                Insert(node.left_node, node_inserted);
    //            }
    //        }
    //    }
    //}

    //class class_example 
    //{
    //    int a;
    //    int b;

    //    public void Class_Add(int _a, int _b)
    //    {
    //        a = _a;
    //        b = _b;

    //        int c;

    //        c = b;
    //        b = a;
    //        a = c;

    //        Console.WriteLine(a);
    //        Console.WriteLine(b);
    //        Console.WriteLine(c);
    //    }

    //    public void Class_Print()
    //    {
    //        Console.WriteLine(a);
    //        Console.WriteLine(b);
    //        //Console.WriteLine(c);
    //    }
    //}
    //struct struct_example 
    //{
    //    int a; // = 1;
    //    int b;

    //    public void Struct_Add(int _a, int _b)
    //    {
    //        a = _a;
    //        b = _b;

    //        int c;

    //        c = b;
    //        b = a;
    //        a = c;

    //        Console.WriteLine(a);
    //        Console.WriteLine(b);
    //        Console.WriteLine(c);
    //    }

    //    public void Struct_Print()
    //    {
    //        Console.WriteLine(a);
    //        Console.WriteLine(b);
    //        //Console.WriteLine(c);
    //    }
    //}

    public class Node_class
    {
        public List<Node_class> childnode = new List<Node_class>();

        public int value;

        public Node_class(int _value)
        {
            value = _value;
        }

        public static void Addvalue_class_node(Node_class nodeclass)
        {
            nodeclass.value++;
        }
    }
    public struct Node_struct
    {

        public int value;

        public Node_struct(int _value)
        {
            value = _value;
        }

        public static void Addvalue_struct_node(Node_struct nodestruct)
        {
            nodestruct.value++;
        }
    }

    public class Node 
    {
        public List<Node> childnode = new List<Node>();

        public int value;

        public Node(int _value)
        {
            value = _value;
        }
    }

    public class Btree
    {
        public static void Split(Node node)
        {
            Node left_node = new Node(2);
            Node right_node = new Node(11);

            node.childnode.Clear();
            node.childnode.Add(left_node);
            node.childnode.Add(right_node);

            node = left_node;
        }
    }

    public class Monster
    {
        public static Monster bossMonster;
        public int health;

        public Monster(int _health)
        {
            health = _health;
        }
        public static void Turn_Zombie(ref Monster monster)
        {
            Monster zombie = new Monster(0); //

            monster = zombie;
        }
    }

    class Program
    {
        static int a = 0;

        static Monster boss = new Monster(1000);
        static int[] arrayint = new int[10];

        static void Addhealth1(Monster monster)
        {
            monster.health++;
        }

        static void Addvalue_1(int value)
        {
            value++;
        }

        static void SwapMethod(ref Monster monster1, ref Monster monster2)
        {
            Monster monster3 = new Monster(3);
            monster3 = monster1;
            monster1 = monster2;
            monster2 = monster3;
        }

        static void Main(string[] args)
        {
            //1번
            //Monster monster1 = new Monster(100);
            //Monster monster2 = new Monster(120);

            //Monster.Turn_Zombie(ref monster1);


            //Console.WriteLine(monster1.health); // ref 를 안넣으면, 객체 전체를 집어넣었을떄 복사값이 들어가게 된다.


            //2번
            //Monster monster1 = new Monster(100);
            //monster1 = boss;
            //Addhealth1(ref boss);
            ////boss.health++;
            //Console.WriteLine(monster1.health);
            //Console.WriteLine(boss.health); // 참조값(class)

            //int b;
            //b = a;
            ////Addvalue_1(a); // 함수 안에 들어가는 a는 복사가 된 a
            //a++;
            //Console.WriteLine(a);
            //Console.WriteLine(b); // 복사값(struct), int 는 struct 이다!

            //3번
            Monster monster1 = new Monster(1); // 객체생성
            Monster monster2 = new Monster(2); // 객체 전체를 집어넣을때는 복사값이 들어가게 된다!

            SwapMethod(ref monster1, ref monster2);

            Console.WriteLine(monster1.health);
            Console.WriteLine(monster2.health);
        }
    }
}

