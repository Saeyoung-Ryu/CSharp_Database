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


    class Student 
    {
        public string name;
        public int correctNumber;

        public List<int> answer = new List<int>();

        public Student(string _name)
        {
            name = _name;
        }        
    }

    class Test
    {
        public static int numberOfQuestions;
        public static List<Student> student_list = new List<Student>();
        public static List<int> answer_list = new List<int>();

        static int ans = 1;

        public static void CreateStudent(string name)
        {
            Student student = new Student(name);
            student_list.Add(student);
        }
        public static void CreateTest(int _numberOfQuestions)
        {
            numberOfQuestions = _numberOfQuestions;

            Random ran = new Random();

            for (int i = 0; i < numberOfQuestions; i++)
            {
                
                int ans = ran.Next(1, 6);

                answer_list.Add(ans);
            }
        }
        public static void Guess_1(Student student)
        {
            for (int i = 1; i <= numberOfQuestions; i++)
            {
                student.answer.Add(ans);

                if(ans != 5)
                {
                    ans++;
                }
                else
                {
                    ans = 1;
                }

            }

            ans = 1;
        }
        public static void Guess_2(Student student)
        {
            for (int i = 1; i <= numberOfQuestions; i++)
            {
                if(i % 2 == 1)
                {
                    student.answer.Add(2);
                }
                else
                {
                    student.answer.Add(ans);

                    if (ans != 5)
                    {
                        ans++;
                    }
                    else
                    {
                        ans = 1;
                    }
                }
            }

            ans = 1;
        }
        public static void Guess_3(Student student)
        {
            int[] order = new int[5] { 3, 1, 2, 4, 5 };

            for (int i = 1; i <= numberOfQuestions; i++)
            {
                int num = 0;

                for (int j = 0; j < 2; j++)
                {
                    student.answer.Add(order[num]);
                    i++;
                }

                if(num != order.Length - 1)
                {
                    num++;
                }
                else
                {
                    num = 0;
                }
            }
        }
        public static void Ranking(List<Student> student_list)
        {
            for (int j = 0; j < student_list.Count; j++)
            {
                for (int i = 0; i < numberOfQuestions; i++)
                {
                    if (student_list[j].answer[i] == answer_list[i])
                    {
                        student_list[j].correctNumber++;
                    }
                }
            }

            foreach (var item in student_list)
            {
                Console.WriteLine(item.name + " 학생이 맞은 문제 수는" + item.correctNumber);
            }
        }
    }

    class Start
    {
        

        static void Main(string[] args)
        {
            Test.CreateStudent("가");
            Test.CreateStudent("나");
            //Test.CreateStudent("다");

            Test.CreateTest(20);

            Test.Guess_1(Test.student_list[0]);
            Test.Guess_2(Test.student_list[1]);
            //Test.Guess_3(Test.student_list[2]);

            Test.Ranking(Test.student_list);
        }
    }
}

