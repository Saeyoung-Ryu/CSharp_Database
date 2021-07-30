using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binarytree
{

    class MyList<T>

    {
        const int DEFAULT_SIZE = 1;
        public T[] _data = new T[DEFAULT_SIZE];


        public int Count; // 실제로 사용중인 데이터 개수
        public int Capacity { get { return _data.Length; } } //예약된 데이터 개수

        public void Add(T item)
        {
            //1. 공간이 충분히 남아있는지 확인하기
            if (Count >= Capacity)
            {

                //공간을 다시 늘려서 확보한다
                T[] newArray = new T[Count * 2];
                for (int i = 0; i < Count; i++)
                {

                    newArray[i] = _data[i]; // 새로 늘린 배열에다가 기존의 데이터들을 넣고
                    _data = newArray;       // 새로 만든 배열을 다시 기존의 배열이라고 쓰기.
                }
                //Console.WriteLine("Count는" + Count);
            }

            //2. 공간이 확보되면 공간에다가 데이터를 넣어준다
            _data[Count] = item;
            Count++;
        }

        public T this[int index] //인덱서
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }

        public void RemoveAt(int index)
        {
            for (int i = index; i < Count - 1; i++)

                _data[i] = _data[i + 1];
            _data[Count - 1] = default(T);
            Count--;
        }
    } //배열 사이즈를 유동적으로 줄였다가늘렸다가 가능. 인덱스값으로 searching이 쉽게 가능
    class My_List
    {
        public static int count = 0;
        public static int list_count = 1;
        public int[] list = new int[list_count];

        public void Add(int value)
        {

            if (count < list_count)
            {
                list[count] = value;
                count++;
            }
            else if (count >= list_count)
            {

                list_count += 1;
                int[] newArray = new int[list_count];
                for (int i = 0; i < list.Length; i++)
                {
                    newArray[i] = list[i];
                }

                newArray[count] = value;
                count++;

                list = newArray;
            }

        } // 배열에 원소 추가
        public void Clear()
        {
            int[] newArray = new int[0];
            list = newArray;
        } // 배열 초기화
        public void Find(int value)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == value)
                {
                    Console.WriteLine(value + "는 인덱스" + i + "번째에 존재합니다");
                }
            }
        } // 특정 원소 찾기

    }
    class MyLinkedListNode<T>
    {
        public T Data;
        public MyLinkedListNode<T> Next;
        public MyLinkedListNode<T> Prev;
    }
    class MyLinkedList<T>

    {
        public MyLinkedListNode<T> Head = null; // first
        public MyLinkedListNode<T> Tail = null; // last
        public int Count = 0;

        public MyLinkedListNode<T> AddLast(T data)
        {
            MyLinkedListNode<T> newMyLinkedListNode = new MyLinkedListNode<T>();
            newMyLinkedListNode.Data = data;

            if (Head == null) // 만약에 아직 방이 아예 없었다면 새로 추가한 첫번째 방이 곧 HEAD 이다.
            {
                Head = newMyLinkedListNode;
            }

            //기존의 [마지막방]과 [새로 추가되는 방]을 연결해준다
            if (Tail != null)
            {
                Tail.Next = newMyLinkedListNode;
                newMyLinkedListNode.Prev = Tail;
            }

            //[새로 추가되는 방]을 [마지막 방]으로 인정한다 
            Tail = newMyLinkedListNode;
            Count++;
            return newMyLinkedListNode;
        }


        public void Remove(MyLinkedListNode<T> MyLinkedListNode)
        {
            //기존의 첫번째 방의 다음 방을 첫번째 방으로 인정한다
            if (Head == MyLinkedListNode)
            {
                Head = Head.Next;
            }

            //기존의 마지막 방의 이전 방을 마지막 방으로 인정한다
            if (Tail == MyLinkedListNode)
            {
                Tail = Tail.Prev;
            }

            if (MyLinkedListNode.Prev != null)
            {
                MyLinkedListNode.Prev.Next = MyLinkedListNode.Next;
            }

            if (MyLinkedListNode.Next != null)
            {
                MyLinkedListNode.Next.Prev = MyLinkedListNode.Prev;
            }

            Count--;
        }
    }//중간 삽입 삭제가 빠르고 편리하게 가능.
    class Graph
    {
        public int[,] adj = new int[7, 7]
        {
            {0,1,0,0,0,1,0 },
            {1,0,1,0,0,0,0 },
            {0,1,0,1,0,1,0 },
            {0,0,1,0,1,0,0 },
            {0,0,0,0,1,0,0 },
            {1,0,1,0,0,0,1 },
            {0,0,0,0,0,1,0 },
        };

        public List<int>[] adj2 = new List<int>[]
        {
            new List<int>() {1,5},
            new List<int>() {0,2},
            new List<int>() {1,3,5},
            new List<int>() {2,4},
            new List<int>() {3},
            new List<int>() {0,2,6},
            new List<int>() {5},
        };

        bool[] visited = new bool[7];
        public void DFS(int startPoint)
        {
            Console.WriteLine(startPoint);
            visited[startPoint] = true;

            //case1
            for (int next = 0; next < adj.GetLength(0); next++)
            {

                if (adj[startPoint, next] == 0) // 연결안되있으면 스킵
                {
                    continue;
                }

                if (visited[next]) // 이미 방문했다면 스킵
                {
                    continue;
                }
                DFS(next); // 재귀함수. 이 함수가 끝나면 다시 원래 반복문으로 돌아와 진행됨.
            }

        }

        public void BFS(int startPoint)
        {
            bool[] found = new bool[7];

            Queue<int> q = new Queue<int>();
            q.Enqueue(startPoint);
            found[startPoint] = true;

            while (q.Count > 0)
            {
                int now = q.Dequeue();
                Console.WriteLine(now);

                for (int next = 0; next < 7; next++)
                {
                    if (adj[now, next] == 0) // 인접한 애라면 스킵
                    {
                        continue;
                    }
                    if (found[next]) // 이미 발견한 애라면 스킵
                    {
                        continue;
                    }
                    q.Enqueue(next);
                    found[next] = true;
                }
            }

        }
    }
    class Node_Tree // 일반 트리 노드
    {
        public int value;
        public List<Node_Tree> node = new List<Node_Tree>();

        public Node_Tree(int value)
        {
            this.value = value;
        }
    }
    class Node_BinaryTree // 이진 트리 노드
    {
        public int value;
        public Node_BinaryTree left;
        public Node_BinaryTree right;

        public Node_BinaryTree(int value) // 
        {
            this.value = value;
        }
    }
    public class Node_HeapTree
    {
        public static int index = 0;
        public int _index;
        public int value;

        public Node_HeapTree parent_node;
        public Node_HeapTree left_node;
        public Node_HeapTree right_node;

        public bool isLeft;
        public bool isRight;
        public bool isRoot = false;
        public bool lastswap = false;


        public Node_HeapTree(int value)
        {


            this.value = value;
            _index = index;
            index++;
            if (_index % 2 == 0 && _index != 0)
            {
                isLeft = true;
            }
            else if (_index % 2 == 1 && _index != 0 && HeapTree.node_HeapTrees.Count > 1)
            {
                isRight = true;
            }

            //if(HeapTree.node_HeapTrees.Count > 0)
            //{
            //    if (HeapTree.node_HeapTrees[_index]._index / 2 >= 1)
            //    {
            //        if (HeapTree.node_HeapTrees[_index].isLeft == true)
            //        {
            //            HeapTree.node_HeapTrees[_index].parent_node = HeapTree.node_HeapTrees[_index / 2];// 부모노드 정하기
            //            HeapTree.node_HeapTrees[_index / 2].left_node = HeapTree.node_HeapTrees[_index].parent_node; // 좌노드 정하기
            //        }
            //        else if (HeapTree.node_HeapTrees[_index].isRight == true)
            //        {
            //            HeapTree.node_HeapTrees[_index].parent_node = HeapTree.node_HeapTrees[(_index - 1) / 2];// 부모노드 정하기
            //            HeapTree.node_HeapTrees[(_index - 1) / 2].right_node = HeapTree.node_HeapTrees[_index].parent_node; // 우노드 정하기
            //        }

            //        //새로운 노드가 생성될때에는
            //        //1. 자신의 부모노드가 누구인지
            //        //2. 자신이 누구의 좌/우 노드인지
            //        //구하기!!
            //    }
            //}
            //else
            //{
            //    return;
            //}



        }
    }// 힙 트리 노드
    class BinaryTree // 이진 트리
    {
        public static int count;
        public static void InOrderTraversal(Node_BinaryTree root)
        {
            if (root.left != null)
            {
                InOrderTraversal(root.left);
            }

            Console.WriteLine(root.value);

            if (root.right != null)
            {
                InOrderTraversal(root.right);
            }


        }
        public static void PreOrderTraversal(Node_BinaryTree root)
        {
            Console.WriteLine(root.value);
            if (root.left != null)
            {
                PreOrderTraversal(root.left);
            }
            if (root.right != null)
            {
                PreOrderTraversal(root.right);
            }



        }
        public static void PostOrderTraversal(Node_BinaryTree root)
        {

            if (root.left != null)
            {
                PostOrderTraversal(root.left);
            }
            if (root.right != null)
            {
                PostOrderTraversal(root.right);
            }

            Console.WriteLine(root.value);

        }
        public static Node_BinaryTree Search_(Node_BinaryTree root, int value)
        {

            if (root.value == value)
            {
                Console.WriteLine(count + "번만에 찾았습니다");
                return root;
            }
            else if (value < root.value)
            {
                count++;
                Console.WriteLine("아래, 왼쪽으로 이동");
                //go to left
                root = Search_(root.left, value);
                // 원래 root는 매개변수로 받는 값이였지만,
                // root = Search_() 함수로 인해
                // root = root.left.right.... 이런식으로 바뀐다
            }
            else if (value > root.value)
            {
                count++;
                Console.WriteLine("아래, 오른쪽으로 이동");
                //go to right
                root = Search_(root.right, value);
            }


            return root;
        }
        public static Node_BinaryTree Insert(Node_BinaryTree root, int value)
        {
            if (root.value == value)
            {
                return root;
            }
            else if (value < root.value)
            {
                if (root.left != null)
                {
                    root.left = Insert(root.left, value);
                }
                else
                {
                    //insert new Node_BinaryTree
                    Node_BinaryTree newNode_BinaryTree = new Node_BinaryTree(value);
                    root.left = newNode_BinaryTree;
                }
            }
            else if (value > root.value)
            {
                if (root.right != null)
                {
                    root.right = Insert(root.right, value);
                }
                else
                {
                    //insert new Node_BinaryTree
                    Node_BinaryTree newNode_BinaryTree = new Node_BinaryTree(value);
                    root.right = newNode_BinaryTree;
                }
            }

            return root;
        }
    }
    class PriorityQueue<T> where T : IComparable<T> //우선순위 큐(값이 큰 순서대로 나오게 해준다)
    {
        List<T> _heap = new List<T>();

        public void Enqueue(T data)
        {
            // 힙의 맨 끝에 새로운 데이터를 삽입한다
            _heap.Add(data);

            int now = _heap.Count - 1;
            //도장깨기를 시작
            while (now > 0)
            {
                //도장깨기를 시도
                int next = (now - 1) / 2; // 이진탐색을 사용하여, 가운데서부터 찾는다
                if (_heap[now].CompareTo(_heap[next]) < 0)
                    break; //실패

                // 두 값을 교체한다
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                //검사 위치를 이동한다

                now = next;
            }
        }

        public T Dequeue()
        {
            //반환할 데이터를 따로 저장
            T ret = _heap[0];

            // 마지막 데이터를 루트로 이동한다
            int lastindex = _heap.Count - 1;
            _heap[0] = _heap[lastindex];
            _heap.RemoveAt(lastindex);
            lastindex--;



            // 역으로 내려가는 도장꺠기 시작
            int now = 0;
            while (true)
            {
                int left = 2 * now + 1;
                int right = 2 * now + 2;

                int next = now;
                //왼쪽 값이 현재 값보다 크면, 왼쪽으로 이동
                if (left <= lastindex && _heap[next].CompareTo(_heap[left]) < 0)
                    next = left;
                //오른값이 현재값보다 크면, 오른쪽으로 이동
                if (right <= lastindex && _heap[next].CompareTo(_heap[right]) < 0)
                    next = right;

                //왼쪽 오른쪽 모두 현재값보다 작으면 종료
                if (next == now)
                    break;
                // 두 값을 교체한다
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;
                //검사 위치를 이동한다

                now = next;
            }

            return ret;
        }
        public int Count()
        {
            return _heap.Count;
        }

    }
    class MyPriorityQueue
    {
        public static List<int> Mylist = new List<int>();
        public int index = 0;
        public int n = 1;

        public void SetOrder(int value)
        {
            if (Mylist[Mylist.Count - n] >= Mylist[Mylist.Count - (n + 1)])
            {
                return;
            }

            else if (Mylist[Mylist.Count - n] < Mylist[Mylist.Count - (n + 1)])
            {

                int a = Mylist[Mylist.Count - n];
                Mylist[Mylist.Count - n] = Mylist[Mylist.Count - (n + 1)];
                Mylist[Mylist.Count - (n + 1)] = a;
                n++;
                if (Mylist[Mylist.Count] - n > 0)
                {
                    SetOrder(value);
                }
                else if (Mylist[Mylist.Count] - n < 0)
                {
                    return;
                }
            }

        }

        public void Enqueue(int value)
        {
            if (Mylist.Count == 0)
            {
                Mylist.Add(value);
            }
            else if (Mylist.Count >= 1)
            {
                Mylist.Add(value);
                SetOrder(value);
            }
        }
    }
    public class HeapTree
    {
        public static int level;
        public static List<Node_HeapTree> node_HeapTrees = new List<Node_HeapTree>();

        public static Node_HeapTree CreateNode(int value)// insert 할때 사용하기
        {
            Node_HeapTree newnode = new Node_HeapTree(value);
            node_HeapTrees.Add(newnode);
            if (HeapTree.node_HeapTrees.Count > 2)
            {
                if (HeapTree.node_HeapTrees[newnode._index]._index / 2 >= 1)
                {
                    if (HeapTree.node_HeapTrees[newnode._index].isLeft == true)
                    {
                        HeapTree.node_HeapTrees[newnode._index].parent_node = HeapTree.node_HeapTrees[newnode._index / 2];// 부모노드 정하기
                        HeapTree.node_HeapTrees[newnode._index / 2].left_node = HeapTree.node_HeapTrees[newnode._index]; // 좌노드 정하기
                    }
                    else if (HeapTree.node_HeapTrees[newnode._index].isRight == true)
                    {
                        HeapTree.node_HeapTrees[newnode._index].parent_node = HeapTree.node_HeapTrees[(newnode._index - 1) / 2];// 부모노드 정하기
                        HeapTree.node_HeapTrees[(newnode._index - 1) / 2].right_node = HeapTree.node_HeapTrees[newnode._index]; // 우노드 정하기
                    }

                    //새로운 노드가 생성될때에는
                    //1. 자신의 부모노드가 누구인지
                    //2. 자신이 누구의 좌/우 노드인지
                    //구하기!!
                }
            }
            else
            {
                return newnode;
            }



            return newnode;
        }
        public static void SetList()
        {
            CreateNode(0);
            CreateNode(5);
            CreateNode(7);
            CreateNode(9);
            CreateNode(13);
            CreateNode(11);
            CreateNode(15);
            CreateNode(10);

            //Node_HeapTree n1 = new Node_HeapTree(5);
            //Node_HeapTree n2 = new Node_HeapTree(7);
            //Node_HeapTree n3 = new Node_HeapTree(9);
            //Node_HeapTree n4 = new Node_HeapTree(13);
            //Node_HeapTree n5 = new Node_HeapTree(11);
            //Node_HeapTree n6 = new Node_HeapTree(15);
            //Node_HeapTree n7 = new Node_HeapTree(10);



        }
        public static void Insert(int value)
        {
            CreateNode(value);
            Arrange_Insert(node_HeapTrees.Count - 1);
        }
        public static void Arrange_Insert(int value)
        {
            if (node_HeapTrees[value].parent_node != null)
            {

                if (HeapTree.node_HeapTrees[value].isLeft == true && node_HeapTrees[value].value < node_HeapTrees[value / 2].value)
                {
                    int save = node_HeapTrees[value].value;
                    node_HeapTrees[value].value = node_HeapTrees[value / 2].value;
                    node_HeapTrees[value / 2].value = save;

                    Arrange_Insert(value / 2);

                }
                else if (HeapTree.node_HeapTrees[value].isRight == true && node_HeapTrees[value].value < node_HeapTrees[(value - 1) / 2].value)
                {
                    int save = node_HeapTrees[value].value;
                    node_HeapTrees[value].value = node_HeapTrees[(value - 1) / 2].value;
                    node_HeapTrees[(value - 1) / 2].value = save;

                    Arrange_Insert((value - 1) / 2);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }



        }
        public static void Remove()
        {
            if (node_HeapTrees.Count > 0)
            {
                node_HeapTrees[1].value = node_HeapTrees[node_HeapTrees.Count - 1].value;

                node_HeapTrees.RemoveAt(node_HeapTrees.Count - 1);
                Node_HeapTree.index--;
            }


            Arrange_Remove(1);
        }
        public static void Arrange_Remove(int value)
        {
            if (node_HeapTrees[value].left_node != null && node_HeapTrees[value].right_node != null)
            {
                if (node_HeapTrees[value].left_node.value < node_HeapTrees[value].value && node_HeapTrees[value].right_node.value < node_HeapTrees[value].value)
                {
                    if (node_HeapTrees[value].left_node.value > node_HeapTrees[value].right_node.value)
                    {
                        int save = node_HeapTrees[value].value;
                        node_HeapTrees[value].value = node_HeapTrees[value].right_node.value;
                        node_HeapTrees[value].right_node.value = save;
                        Arrange_Remove(node_HeapTrees[value].right_node._index);
                    }
                    else if (node_HeapTrees[value].left_node.value < node_HeapTrees[value].right_node.value)
                    {
                        int save = node_HeapTrees[value].value;
                        node_HeapTrees[value].value = node_HeapTrees[value].left_node.value;
                        node_HeapTrees[value].left_node.value = save;
                        Arrange_Remove(node_HeapTrees[value].left_node._index);
                    }
                }
                else if (node_HeapTrees[value].left_node.value < node_HeapTrees[value].value && node_HeapTrees[value].right_node.value > node_HeapTrees[value].value)
                {
                    int save = node_HeapTrees[value].value;
                    node_HeapTrees[value].value = node_HeapTrees[value].left_node.value;
                    node_HeapTrees[value].left_node.value = save;
                    Arrange_Remove(node_HeapTrees[value].left_node._index);
                }
                else if (node_HeapTrees[value].left_node.value > node_HeapTrees[value].value && node_HeapTrees[value].right_node.value < node_HeapTrees[value].value)
                {
                    int save = node_HeapTrees[value].value;
                    node_HeapTrees[value].value = node_HeapTrees[value].right_node.value;
                    node_HeapTrees[value].right_node.value = save;
                    Arrange_Remove(node_HeapTrees[value].right_node._index);
                }
                else
                {
                    return;
                }
            }
            else if (node_HeapTrees[value].left_node != null && node_HeapTrees[value].right_node == null)
            {
                if (node_HeapTrees[value].left_node.value < node_HeapTrees[value].value)
                {
                    int save = node_HeapTrees[value].value;
                    node_HeapTrees[value].value = node_HeapTrees[value].left_node.value;
                    node_HeapTrees[value].left_node.value = save;
                    Arrange_Remove(node_HeapTrees[value].left_node._index);
                }
                else
                {
                    return;
                }
            }
            else if (node_HeapTrees[value].left_node == null && node_HeapTrees[value].right_node == null)
            {
                return;
            }


        }



    }
    public class Node
    {
        public static int level = 1;
        public static int m; //4차, 3개의 자식을 최대로 갖는다. (짝수 차수만 넣기!!)

        public List<Key> key = new List<Key>(); // 작은것 에서 큰 순서대로 나열하기 (Priority Queue 사용), 최대 들어갈수 있는 키 수는 m - 1
        public List<Node> childNode = new List<Node>();

        public bool isRoot;

        public Node parentNode;
        public Node(Node node)
        {
            //parentNode = node;
        }


    }
    public class Key
    {
        public int key_value;

        public Key(int value)
        {
            key_value = value;
        }
    }
    public class Btree
    {
        public static Node nodechanged; // 노드 탐색할떄 쓰임        
        public static int count; // 형제노드가 빈곤한지 아닌지 확인할때쓰임        
        public static int key_count = 1;
        public static Key save;
        public static Node fullBrotherNode;
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        public static void SetOrder(int value)
        {
            Node.m = value;
        } // 몇차 비트리인지 정하는 함수(짝수 차수 일단)
        public static void Arrange_key(Node node)
        {
            PriorityQueue<int> queue = new PriorityQueue<int>();

            int a = node.key.Count;

            for (int i = 0; i < a; i++)
            {
                queue.Enqueue(node.key[i].key_value);
            }
            node.key.Clear();

            for (int i = 0; i < a; i++)
            {
                node.key.Add(new Key(queue.Dequeue()));
            }
        } // 노드 안의 키 값 작은순서대로 정렬
        public static void Arrange_node(Node node)
        {
            List<Node> compare = new List<Node>();
            for (int i = 0; i < node.childNode.Count; i++)
            {
                compare.Add(node.childNode[i]);

                if (compare.Count > 1)
                {
                    for (int j = 1; j < compare.Count; j++)
                    {
                        if (compare[compare.Count - j - 1].key[0].key_value > compare[compare.Count - j].key[0].key_value)
                        {
                            Node save = compare[compare.Count - j - 1];
                            compare[compare.Count - j - 1] = compare[compare.Count - j];
                            compare[compare.Count - j] = save;

                        }
                        else
                        {
                            continue;
                        }
                    }
                    continue;


                }
                else
                {
                    continue;
                }
            }
            node.childNode.Clear();
            for (int k = 0; k < compare.Count; k++)
            {
                node.childNode.Add(compare[k]);
            }
            compare.Clear();


        } // 자식 노드 작은순서대로 정렬
          //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        public static void Split(Key key, Node node)
        {

            int count = node.key.Count;

            if (node.isRoot == true && node.key.Count == Node.m - 1) // 루트노드를 쪼개는 상황일떄
            {
                Node.level++;

                if (node.childNode.Count == 0) // 루트노드가 자식이 없는 상태에서 쪼개질때
                {

                    Node node_left = new Node(node) { parentNode = node.parentNode };

                    Node node_right = new Node(node) { parentNode = node.parentNode };

                    if (Node.m % 2 == 0)
                    {
                        for (int i = 0; i < (int)(Node.m / 2 - 0.5); i++)
                        {
                            node_left.key.Add(node.key[i]);
                            Arrange_key(node_left);
                        }

                        for (int i = count - 1; i > Node.m / 2 - 1; i--)
                        {
                            node_right.key.Add(node.key[i]);
                            Arrange_key(node_right);
                        }

                        int save = (int)((Node.m - 1) / 2);
                        Key save_key = node.key[save];
                        node.key.Clear();
                        node.key.Add(save_key);
                        AddChild(node_left, node);
                        AddChild(node_right, node);
                    }
                    else if (Node.m % 2 != 0) // 홀수일때 노드값 넣는 방법 (중간에서 더큰 애가 위로 올라감)
                    {
                        for (int i = 0; i < Node.m / 2 - 0.5; i++)
                        {
                            node_left.key.Add(node.key[i]);
                            Arrange_key(node_left);
                        }

                        for (int i = count - 1; i > (int)(Node.m / 2); i--)
                        {
                            node_right.key.Add(node.key[i]);
                            Arrange_key(node_right);
                        }

                        int save = (int)((Node.m - 1) / 2);
                        Key save_key = node.key[save];
                        node.key.Clear();
                        node.key.Add(save_key);
                        AddChild(node_left, node);
                        AddChild(node_right, node);
                    }




                    if (node.key.Count == 1) // Insert 함수라고 볼수 있다. 리프노드에서 추가
                    {
                        if (key.key_value < node.key[0].key_value) // 추가할라는 값 적당한 위치에 크기비교하여 삽입하기!
                        {
                            node.childNode[0].key.Add(key);
                            Arrange_key(node.childNode[0]);
                        }
                        else
                        {
                            node.childNode[1].key.Add(key);
                            Arrange_key(node.childNode[1]);
                        }
                    }
                    else if (node.key.Count == 2)
                    {
                        if (key.key_value < node.key[0].key_value) // 추가할라는 값 적당한 위치에 크기비교하여 삽입하기!
                        {
                            node.childNode[0].key.Add(key);
                            Arrange_key(node.childNode[0]);
                        }
                        else if (key.key_value > node.key[0].key_value && key.key_value < node.key[1].key_value)
                        {
                            node.childNode[1].key.Add(key);
                            Arrange_key(node.childNode[1]);
                        }
                        else
                        {
                            node.childNode[2].key.Add(key);
                            Arrange_key(node.childNode[2]);
                        }
                    }
                    else if (node.key.Count == 3)
                    {
                        if (key.key_value < node.key[0].key_value) // 추가할라는 값 적당한 위치에 크기비교하여 삽입하기!
                        {
                            node.childNode[0].key.Add(key);
                            Arrange_key(node.childNode[0]);
                        }
                        else if (key.key_value > node.key[0].key_value && key.key_value < node.key[1].key_value)
                        {
                            node.childNode[1].key.Add(key);
                            Arrange_key(node.childNode[1]);
                        }
                        else if (key.key_value > node.key[1].key_value && key.key_value < node.key[2].key_value)
                        {
                            node.childNode[2].key.Add(key);
                            Arrange_key(node.childNode[2]);
                        }
                        else
                        {
                            node.childNode[3].key.Add(key);
                            Arrange_key(node.childNode[3]);
                        }
                    }
                    else if (node.key.Count == 4)
                    {
                        if (key.key_value < node.key[0].key_value) // 추가할라는 값 적당한 위치에 크기비교하여 삽입하기!
                        {
                            node.childNode[0].key.Add(key);
                            Arrange_key(node.childNode[0]);
                        }
                        else if (key.key_value > node.key[0].key_value && key.key_value < node.key[1].key_value)
                        {
                            node.childNode[1].key.Add(key);
                            Arrange_key(node.childNode[1]);
                        }
                        else if (key.key_value > node.key[1].key_value && key.key_value < node.key[2].key_value)
                        {
                            node.childNode[2].key.Add(key);
                            Arrange_key(node.childNode[2]);
                        }
                        else if (key.key_value > node.key[2].key_value && key.key_value < node.key[3].key_value)
                        {
                            node.childNode[3].key.Add(key);
                            Arrange_key(node.childNode[3]);
                        }
                        else
                        {
                            node.childNode[4].key.Add(key);
                            Arrange_key(node.childNode[4]);
                        }
                    }

                }
                else // 루트노드가 자식이 잇는 상태에서 쪼개질떄
                {

                    Node node_left = new Node(node) { parentNode = node.parentNode };

                    Node node_right = new Node(node) { parentNode = node.parentNode };

                    if (Node.m % 2 == 0)
                    {
                        for (int i = 0; i < (int)(Node.m / 2 - 0.5); i++) // 키값 할당하기(왼쪽 노드에)
                        {
                            node_left.key.Add(node.key[i]);
                            Arrange_key(node_left);
                        }

                        for (int i = count - 1; i > Node.m / 2 - 0.5; i--) // 키값 할당하기(오른쪽 노드에)
                        {
                            node_right.key.Add(node.key[i]);
                            Arrange_key(node_right);
                        }
                    }
                    else if (Node.m % 2 != 0)
                    {
                        for (int i = 0; i < (int)(Node.m / 2); i++) // 키값 할당하기(왼쪽 노드에)
                        {
                            node_left.key.Add(node.key[i]);
                            Arrange_key(node_left);
                        }

                        for (int i = count - 1; i > Node.m / 2; i--) // 키값 할당하기(오른쪽 노드에)
                        {
                            node_right.key.Add(node.key[i]);
                            Arrange_key(node_right);
                        }
                    }

                    if (Node.m % 2 == 0) // 짝수 차수일때
                    {
                        for (int i = 0; i < Node.m / 2; i++) // 자식노드 할당하기(왼쪽 노드에)
                        {
                            //node_left.childNode.Add(node.childNode[i]);
                            AddChild(node.childNode[i], node_left);
                            Arrange_node(node_left);
                        }

                        for (int i = Node.m - 1; i >= Node.m / 2; i--) // 자식노드 할당하기(오른쪽 노드에)
                        {
                            //node_right.childNode.Add(node.childNode[i]);
                            AddChild(node.childNode[i], node_right);
                            Arrange_node(node_right);
                        }

                        int save = (int)((Node.m - 1) / 2);
                        Key save_key = node.key[save];
                        node.key.Clear();
                        node.key.Add(save_key);
                        node.childNode.Clear();
                        AddChild(node_left, node);
                        AddChild(node_right, node);
                        Arrange_node(node);
                    }
                    else if (Node.m % 2 != 0) // 홀수 차수일때 -0.5 해주기!
                    {
                        for (int i = 0; i < (int)(Node.m / 2) + 1; i++) // 자식노드 할당하기(왼쪽 노드에)
                        {
                            //node_left.childNode.Add(node.childNode[i]);
                            AddChild(node.childNode[i], node_left);
                            Arrange_node(node_left);
                        }

                        for (int i = Node.m - 1; i > (int)(Node.m / 2); i--) // 자식노드 할당하기(오른쪽 노드에) 
                        {
                            //node_right.childNode.Add(node.childNode[i]);
                            AddChild(node.childNode[i], node_right);
                            Arrange_node(node_right);


                        }
                        int save = (Node.m - 1) / 2;
                        Key save_key = node.key[save];
                        node.key.Clear();
                        node.key.Add(save_key);
                        node.childNode.Clear();
                        AddChild(node_left, node);
                        AddChild(node_right, node);

                        Arrange_node(node);
                    }



                }



            }
            else if (node.isRoot == false && node.key.Count == Node.m - 1) // 루트노드가 아닌 노드를 쪼갤떄. 이것은 새로운 위에 노드를 생성하는것이 아닌 기존 부모노드에 키값 삽입해야됨
            {
                if (node.childNode.Count != 0) // 리프노드가 아닌걸 쪼갤때
                {
                    Node node_left = new Node(node) { parentNode = node.parentNode };

                    Node node_right = new Node(node) { parentNode = node.parentNode };

                    if (Node.m % 2 == 0)
                    {
                        for (int i = 0; i < (int)(Node.m / 2 - 0.5); i++) // 키값 할당하기(왼쪽 노드에)
                        {
                            node_left.key.Add(node.key[i]);
                            Arrange_key(node_left);
                        }

                        for (int i = count - 1; i > Node.m / 2 - 0.5; i--) // 키값 할당하기(오른쪽 노드에)
                        {
                            node_right.key.Add(node.key[i]);
                            Arrange_key(node_right);
                        }
                    }
                    else if (Node.m % 2 != 0)
                    {
                        for (int i = 0; i < (int)(Node.m / 2); i++) // 키값 할당하기(왼쪽 노드에)
                        {
                            node_left.key.Add(node.key[i]);
                            Arrange_key(node_left);
                        }

                        for (int i = count - 1; i > Node.m / 2; i--) // 키값 할당하기(오른쪽 노드에)
                        {
                            node_right.key.Add(node.key[i]);
                            Arrange_key(node_right);
                        }
                    }


                    if (Node.m % 2 == 0) // 짝수 차수일때
                    {
                        for (int i = 0; i < Node.m / 2; i++) // 자식노드 할당하기(왼쪽 노드에)
                        {
                            //node_left.childNode.Add(node.childNode[i]);
                            AddChild(node.childNode[i], node_left);
                            Arrange_node(node_left);
                        }

                        for (int i = Node.m - 1; i >= Node.m / 2; i--) // 자식노드 할당하기(오른쪽 노드에)
                        {
                            //node_right.childNode.Add(node.childNode[i]);
                            AddChild(node.childNode[i], node_right);
                            Arrange_node(node_right);
                        }

                        int save = (int)((Node.m - 1) / 2);
                        Key save_key = node.key[save];
                        node.key.Clear();


                        node.key.Add(save_key);
                        Arrange_key(node);

                        node.childNode.Clear();

                        node_left.parentNode = node.parentNode;
                        node_right.parentNode = node.parentNode;

                        AddChild(node_left, node);
                        AddChild(node_right, node);

                        Arrange_node(node);

                        node.parentNode.key.Add(node.key[0]);
                        Arrange_key(node.parentNode);

                        node = node.childNode[0];
                        AddChild(node.childNode[1], node.parentNode);

                        Arrange_node(node.parentNode);
                    }
                    else if (Node.m % 2 != 0) // 홀수 차수일때
                    {
                        for (int i = 0; i < Node.m / 2; i++) // 자식노드 할당하기(왼쪽 노드에)
                        {
                            //node_left.childNode.Add(node.childNode[i]);
                            AddChild(node.childNode[i], node_left);
                            Arrange_node(node_left);
                        }

                        for (int i = Node.m - 1; i > Node.m / 2; i--) // 자식노드 할당하기(오른쪽 노드에)
                        {
                            //node_right.childNode.Add(node.childNode[i]);
                            AddChild(node.childNode[i], node_right);
                            Arrange_node(node_right);
                        }

                        int save = (int)((Node.m - 1) / 2);
                        Key save_key = node.key[save];
                        node.key.Clear();


                        node.key.Add(save_key);
                        Arrange_key(node);

                        node.childNode.Clear();

                        node_left.parentNode = node.parentNode;
                        node_right.parentNode = node.parentNode;

                        AddChild(node_left, node);
                        AddChild(node_right, node);

                        Arrange_node(node);

                        node.parentNode.key.Add(node.key[0]);
                        Arrange_key(node.parentNode);

                        node = node.childNode[0];
                        AddChild(node.childNode[1], node.parentNode);

                        Arrange_node(node.parentNode);
                    }





                }
                else // 리프노드일때
                {

                    Node node_left = new Node(node) { parentNode = node.parentNode };

                    Node node_right = new Node(node) { parentNode = node.parentNode };




                    if (Node.m % 2 == 0)
                    {
                        for (int i = 0; i < (int)(Node.m / 2 - 0.5); i++) // 키값 할당하기(왼쪽 노드에) 
                        {
                            node_left.key.Add(node.key[i]);
                            Arrange_key(node_left);
                        }

                        for (int i = count - 1; i > Node.m / 2 - 0.5; i--) // 키값 할당하기(오른쪽 노드에)
                        {
                            node_right.key.Add(node.key[i]);
                            Arrange_key(node_right);
                        }

                        int save = (int)((Node.m - 1) / 2);
                        Key save_key = node.key[save];


                        node.parentNode.key.Add(save_key);
                        Arrange_key(node.parentNode);
                    }
                    else
                    {
                        for (int i = 0; i < Node.m / 2 - 0.5; i++) // 키값 할당하기(왼쪽 노드에) 
                        {
                            node_left.key.Add(node.key[i]);
                            Arrange_key(node_left);
                        }

                        for (int i = count - 1; i > Node.m / 2; i--) // 키값 할당하기(오른쪽 노드에)
                        {
                            node_right.key.Add(node.key[i]);
                            Arrange_key(node_right);
                        }

                        int save = (int)((Node.m - 1) / 2);
                        Key save_key = node.key[save];


                        node.parentNode.key.Add(save_key);
                        Arrange_key(node.parentNode);
                    }





                    //이미 키를 추가했는데도 count 가 1이라는것은 루트노드 였다는 뜻이므로,
                    //if(node.parentNode.key.Count ==0 )은 생략한다

                    if (node.parentNode.key.Count == 2)
                    {
                        if (key.key_value < node.parentNode.key[0].key_value) // 추가할라는 값 적당한 위치에 크기비교하여 삽입하기!
                        {
                            node.parentNode.childNode[0] = node_left;
                            AddChild(node_right, node.parentNode);
                            //node.parentNode.childNode.Add(node_right);
                            Arrange_node(node.parentNode);
                        }
                        else
                        {
                            node.parentNode.childNode[1] = node_left;
                            AddChild(node_right, node.parentNode);

                            Arrange_node(node.parentNode);
                        }
                    }
                    else if (node.parentNode.key.Count == 3)
                    {
                        if (key.key_value < node.parentNode.key[0].key_value) // 추가할라는 값 적당한 위치에 크기비교하여 삽입하기!
                        {
                            node.parentNode.childNode[0] = node_left;
                            AddChild(node_right, node.parentNode);

                            Arrange_node(node.parentNode);
                        }
                        else if (key.key_value > node.parentNode.key[0].key_value && key.key_value < node.parentNode.key[1].key_value)
                        {
                            node.parentNode.childNode[1] = node_left;
                            AddChild(node_right, node.parentNode);

                            Arrange_node(node.parentNode);
                        }
                        else
                        {
                            node.parentNode.childNode[2] = node_left;
                            AddChild(node_right, node.parentNode);

                            Arrange_node(node.parentNode);
                        }

                    }
                    else if (node.parentNode.key.Count == 4)
                    {
                        if (key.key_value < node.parentNode.key[0].key_value) // 추가할라는 값 적당한 위치에 크기비교하여 삽입하기!
                        {
                            node.parentNode.childNode[0] = node_left;
                            AddChild(node_right, node.parentNode);

                            Arrange_node(node.parentNode);
                        }
                        else if (key.key_value > node.parentNode.key[0].key_value && key.key_value < node.parentNode.key[1].key_value)
                        {
                            node.parentNode.childNode[1] = node_left;
                            AddChild(node_right, node.parentNode);

                            Arrange_node(node.parentNode);
                        }
                        else if (key.key_value > node.parentNode.key[1].key_value && key.key_value < node.parentNode.key[2].key_value)
                        {
                            node.parentNode.childNode[2] = node_left;
                            AddChild(node_right, node.parentNode);

                            Arrange_node(node.parentNode);
                        }
                        else
                        {
                            node.parentNode.childNode[3] = node_left;
                            AddChild(node_right, node.parentNode);

                            Arrange_node(node.parentNode);
                        }

                    }



                    if (node.parentNode.key.Count == 1)
                    {
                        if (key.key_value < node.parentNode.key[0].key_value) // 추가할라는 값 적당한 위치에 크기비교하여 삽입하기!
                        {
                            node.parentNode.childNode[0].key.Add(key);
                            Arrange_key(node.parentNode.childNode[0]);


                        }
                        else
                        {
                            node.parentNode.childNode[1].key.Add(key);
                            Arrange_key(node.parentNode.childNode[1]);


                        }
                    }
                    else if (node.parentNode.key.Count == 2)
                    {
                        if (key.key_value < node.parentNode.key[0].key_value) // 추가할라는 값 적당한 위치에 크기비교하여 삽입하기!
                        {
                            node.parentNode.childNode[0].key.Add(key);
                            Arrange_key(node.parentNode.childNode[0]);


                        }
                        else if (key.key_value > node.parentNode.key[0].key_value && key.key_value < node.parentNode.key[1].key_value)
                        {
                            node.parentNode.childNode[1].key.Add(key);
                            Arrange_key(node.parentNode.childNode[1]);


                        }
                        else
                        {
                            node.parentNode.childNode[2].key.Add(key);
                            Arrange_key(node.parentNode.childNode[2]);


                        }
                    }
                    else if (node.parentNode.key.Count == 3)
                    {
                        if (key.key_value < node.parentNode.key[0].key_value) // 추가할라는 값 적당한 위치에 크기비교하여 삽입하기!
                        {
                            node.parentNode.childNode[0].key.Add(key);
                            Arrange_key(node.parentNode.childNode[0]);


                        }
                        else if (key.key_value > node.parentNode.key[0].key_value && key.key_value < node.parentNode.key[1].key_value)
                        {
                            node.parentNode.childNode[1].key.Add(key);
                            Arrange_key(node.parentNode.childNode[1]);


                        }
                        else if (key.key_value > node.parentNode.key[1].key_value && key.key_value < node.parentNode.key[2].key_value)
                        {
                            node.parentNode.childNode[2].key.Add(key);
                            Arrange_key(node.parentNode.childNode[2]);


                        }
                        else
                        {
                            node.parentNode.childNode[3].key.Add(key);
                            Arrange_key(node.parentNode.childNode[3]);


                        }
                    }
                    else if (node.parentNode.key.Count == 4)
                    {
                        if (key.key_value < node.parentNode.key[0].key_value) // 추가할라는 값 적당한 위치에 크기비교하여 삽입하기!
                        {
                            node.parentNode.childNode[0].key.Add(key);
                            Arrange_key(node.parentNode.childNode[0]);


                        }
                        else if (key.key_value > node.parentNode.key[0].key_value && key.key_value < node.parentNode.key[1].key_value)
                        {
                            node.parentNode.childNode[1].key.Add(key);
                            Arrange_key(node.parentNode.childNode[1]);


                        }
                        else if (key.key_value > node.parentNode.key[1].key_value && key.key_value < node.parentNode.key[2].key_value)
                        {
                            node.parentNode.childNode[2].key.Add(key);
                            Arrange_key(node.parentNode.childNode[2]);


                        }
                        else if (key.key_value > node.parentNode.key[2].key_value && key.key_value < node.parentNode.key[3].key_value)
                        {
                            node.parentNode.childNode[3].key.Add(key);
                            Arrange_key(node.parentNode.childNode[3]);


                        }
                        else
                        {
                            node.parentNode.childNode[4].key.Add(key);
                            Arrange_key(node.parentNode.childNode[4]);


                        }
                    }
                }
            }
        }
        public static void GoThrough(Key key, Node node)
        {
            if (node.childNode.Count != 0) // 자식노드가 있을때만 들어가라
            {
                if (node.key.Count == 1) // 노드의 key 수가 1개일떄
                {
                    if (node.key[0].key_value > key.key_value)
                    {
                        Search(key, node.childNode[0]);
                    }
                    else if (node.key[0].key_value < key.key_value)
                    {
                        Search(key, node.childNode[1]);
                    }
                }

                else if (node.key.Count == 2) // 노드의 key 수가 2개일떄
                {
                    if (node.key[0].key_value > key.key_value)
                    {
                        Search(key, node.childNode[0]);
                    }
                    else if (node.key[0].key_value < key.key_value && node.key[1].key_value > key.key_value)
                    {
                        Search(key, node.childNode[1]);
                    }
                    else if (node.key[1].key_value < key.key_value)
                    {
                        Search(key, node.childNode[2]);
                    }
                }

                else if (node.key.Count == 3) // 노드의 key 수가 3개일떄. (일단은 4차로 할거니까 3개까지만 만들어도 충분하다)
                {
                    if (node.key[0].key_value > key.key_value)
                    {
                        Search(key, node.childNode[0]);
                    }
                    else if (node.key[0].key_value < key.key_value && node.key[1].key_value > key.key_value)
                    {
                        Search(key, node.childNode[1]);
                    }
                    else if (node.key[1].key_value < key.key_value && node.key[2].key_value > key.key_value)
                    {
                        Search(key, node.childNode[2]);
                    }
                    else if (node.key[2].key_value < key.key_value)
                    {
                        Search(key, node.childNode[3]);
                    }
                }

                else if (node.key.Count == 4) // 노드의 key 수가 4개일떄. (5차로 할거니까 4개)
                {
                    if (node.key[0].key_value > key.key_value)
                    {
                        Search(key, node.childNode[0]);
                    }
                    else if (node.key[0].key_value < key.key_value && node.key[1].key_value > key.key_value)
                    {
                        Search(key, node.childNode[1]);
                    }
                    else if (node.key[1].key_value < key.key_value && node.key[2].key_value > key.key_value)
                    {
                        Search(key, node.childNode[2]);
                    }
                    else if (node.key[2].key_value < key.key_value && node.key[3].key_value > key.key_value)
                    {
                        Search(key, node.childNode[3]);
                    }
                    else if (node.key[3].key_value < key.key_value)
                    {
                        Search(key, node.childNode[3]);
                    }
                }
            }


        }
        public static void Search(Key key, Node node)
        {
            if (node.childNode.Count == 0 && node.isRoot == true) //자식이 없는 루트노드 단 하나만 있는 상태(초기 상태) 자식이 없으므로 gothrough 함수 호출하지 않는다
            {
                if (node.key.Count < Node.m - 1)
                {
                    node.key.Add(key);
                    Arrange_key(node);
                }
                else if (node.key.Count == Node.m - 1)
                {
                    Split(key, node);
                }
            }
            else if (node.childNode.Count != 0 && node.isRoot == true)//자식이 있는 루트노드
            {
                if (node.key.Count == Node.m - 1) // 꽉 찼다면
                {
                    Split(key, node); // ** 여기서는 더하면 안된다. ADD함수 아직 쓰면 안됨 이쪽 split 에서!!
                    GoThrough(key, node);
                }
                else // 꽉 안찼다면
                {
                    GoThrough(key, node);
                }
            }
            else if (node.childNode.Count != 0 && node.isRoot == false) // 일반노드일때(자식 있음)
            {
                if (node.key.Count == Node.m - 1) // 꽉 찼다면
                {
                    Split(key, node);
                    GoThrough(key, node);
                }
                else // 꽉 안찼다면
                {
                    GoThrough(key, node);
                }
            }
            else if (node.childNode.Count == 0 && node.isRoot == false) // 리프노드일떄(자식 없음), 여기서 ADD 하기
            {
                if (node.key.Count < Node.m - 1)
                {
                    node.key.Add(key);
                    Arrange_key(node);
                }
                else if (node.key.Count == Node.m - 1)
                {
                    Split(key, node);

                }
            }
        }
        public static void AddChild(Node node_toAdd, Node current_node) // 새로운 노드를 생성함과 동시에, 그것의 부모 노드도 정해준다.
        {
            current_node.childNode.Add(node_toAdd);
            Arrange_node(current_node);

            node_toAdd.parentNode = current_node;
        } // 부모노드까지 세팅해줌
        public static void Insert(Key key, Node node)
        {
            Search(key, node);
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//       
        public static bool ContainsKey(Key key, Node node)
        {
            for (int i = 0; i < node.key.Count; i++)
            {
                if (node.key[i].key_value == key.key_value)
                {
                    return true;
                }
            }

            return false;
        }
        public static bool IsBrotherNodeFull(Key key, Node node)
        {
            if (count == 0)
            {
                if (node.parentNode.childNode[count + 1].key.Count >= (int)(Node.m / 2))
                {
                    fullBrotherNode = node.parentNode.childNode[count + 1];
                    return true;
                }
                else
                {
                    fullBrotherNode = null; // node.parentNode.childNode[count + 1];
                    return false;
                }
            }
            else if (count == 1)
            {
                if (node.parentNode.key.Count == 1)
                {
                    if (node.parentNode.childNode[count - 1].key.Count >= (int)(Node.m / 2))
                    {
                        fullBrotherNode = node.parentNode.childNode[count - 1];
                        return true;
                    }
                    else
                    {
                        fullBrotherNode = null; // node.parentNode.childNode[count - 1];
                        return false;
                    }

                }
                else if (node.parentNode.key.Count >= 2)
                {
                    if (node.parentNode.childNode[count - 1].key.Count >= (int)(Node.m / 2))
                    {
                        fullBrotherNode = node.parentNode.childNode[count - 1];
                        return true;
                    }
                    else if (node.parentNode.childNode[count + 1].key.Count >= (int)(Node.m / 2))
                    {
                        fullBrotherNode = node.parentNode.childNode[count + 1];
                        return true;
                    }
                    else
                    {
                        fullBrotherNode = null; // node.parentNode.childNode[count - 1];
                        return false;
                    }

                }

            }
            else if (count == 2)
            {
                if (node.parentNode.key.Count == 2)
                {
                    if (node.parentNode.childNode[count - 1].key.Count >= (int)(Node.m / 2))
                    {
                        fullBrotherNode = node.parentNode.childNode[count - 1];
                        return true;
                    }
                    else
                    {
                        fullBrotherNode = null; // node.parentNode.childNode[count - 1];
                        return false;
                    }
                }
                else if (node.parentNode.key.Count == 3)
                {
                    if (node.parentNode.childNode[count - 1].key.Count >= (int)(Node.m / 2))
                    {
                        fullBrotherNode = node.parentNode.childNode[count - 1];
                        return true;
                    }
                    else if (node.parentNode.childNode[count + 1].key.Count >= (int)(Node.m / 2))
                    {
                        fullBrotherNode = node.parentNode.childNode[count + 1];
                        return true;
                    }
                    else
                    {
                        fullBrotherNode = null; // node.parentNode.childNode[count - 1];
                        return false;

                    }




                }
            }
            return false;
        }
        public static void FindNext(Key key, Node node)
        {
            if (key_count == 1)
            {
                if (node.key.Count == 1)
                {
                    if (node.key[0].key_value > key.key_value)
                    {
                        count = 0;
                        Delete(key, node.childNode[0]);
                    }
                    else if (node.key[0].key_value < key.key_value)
                    {
                        count = 1;
                        Delete(key, node.childNode[1]);
                    }

                }
                else if (node.key.Count == 2)
                {
                    if (node.key[0].key_value > key.key_value)
                    {
                        count = 0;
                        Delete(key, node.childNode[0]);
                    }
                    else if (node.key[0].key_value < key.key_value && node.key[1].key_value > key.key_value)
                    {
                        count = 1;
                        Delete(key, node.childNode[1]);
                    }
                    else if (node.key[1].key_value < key.key_value)
                    {
                        count = 2;
                        Delete(key, node.childNode[2]);
                    }
                }
                else if (node.key.Count == 3)
                {
                    if (node.key[0].key_value > key.key_value)
                    {
                        count = 0;
                        Delete(key, node.childNode[0]);
                    }
                    else if (node.key[0].key_value < key.key_value && node.key[1].key_value > key.key_value)
                    {
                        count = 1;
                        Delete(key, node.childNode[1]);
                    }
                    else if (node.key[1].key_value < key.key_value && node.key[2].key_value > key.key_value)
                    {
                        count = 2;
                        Delete(key, node.childNode[2]);
                    }
                    else if (node.key[2].key_value < key.key_value)
                    {
                        count = 3;
                        Delete(key, node.childNode[3]);
                    }
                }
            }
            else if (key_count == 2)
            {
                if (node.key.Count == 1)
                {
                    if (node.key[0].key_value > key.key_value)
                    {

                        Delete(key, node.childNode[0]);
                    }
                    else if (node.key[0].key_value < key.key_value)
                    {

                        Delete(key, node.childNode[1]);
                    }
                    else if (node.key[0].key_value == key.key_value)
                    {
                        key_count--;
                        //FindNext(key, node.childNode[count + 1]);
                        Delete(key, node.childNode[count + 1]);
                    }
                }
                else if (node.key.Count == 2)
                {
                    if (node.key[0].key_value > key.key_value)
                    {

                        Delete(key, node.childNode[0]);
                    }
                    else if (node.key[0].key_value < key.key_value && node.key[1].key_value > key.key_value)
                    {

                        Delete(key, node.childNode[1]);
                    }
                    else if (node.key[1].key_value < key.key_value)
                    {

                        Delete(key, node.childNode[2]);
                    }
                    else if (node.key[0].key_value == key.key_value || node.key[1].key_value == key.key_value)
                    {
                        key_count--;
                        FindNext(key, node.childNode[count + 1]);
                        //Delete(key, node.childNode[count + 1]);
                    }
                }
                else if (node.key.Count == 3)
                {
                    if (node.key[0].key_value > key.key_value)
                    {
                        count = 0;
                        Delete(key, node.childNode[0]);
                    }
                    else if (node.key[0].key_value < key.key_value && node.key[1].key_value > key.key_value)
                    {
                        count = 1;
                        Delete(key, node.childNode[1]);
                    }
                    else if (node.key[1].key_value < key.key_value && node.key[2].key_value > key.key_value)
                    {
                        count = 2;
                        Delete(key, node.childNode[2]);
                    }
                    else if (node.key[2].key_value < key.key_value)
                    {
                        count = 3;
                        Delete(key, node.childNode[3]);
                    }
                    else if (node.key[0].key_value == key.key_value || node.key[1].key_value == key.key_value || node.key[2].key_value == key.key_value)
                    {
                        key_count--;
                        FindNext(key, node.childNode[count + 1]);
                        //Delete(key, node.childNode[count + 1]);
                    }
                }
            }

        }
        public static void Remove(Key key, Node node)
        {
            for (int i = 0; i < node.key.Count; i++)
            {
                if (key.key_value == node.key[i].key_value)
                {
                    node.key.RemoveAt(i);
                }
            }
        }
        public static void Swap(Key key, Node node)
        {
            for (int i = 0; i < node.key.Count; i++)
            {
                if (key.key_value == node.key[i].key_value)
                {
                    if (node.childNode[0].childNode.Count == 0)
                    {
                        save = node.childNode[i + 1].key[0];
                    }
                    else if (node.childNode[0].childNode[0].childNode.Count == 0)
                    {
                        save = node.childNode[i + 1].childNode[0].key[0];
                    }
                    else if (node.childNode[0].childNode[0].childNode[0].childNode.Count == 0)
                    {
                        save = node.childNode[i + 1].childNode[0].childNode[0].key[0];
                    }
                    else if (node.childNode[0].childNode[0].childNode[0].childNode[0].childNode.Count == 0)
                    {
                        save = node.childNode[i + 1].childNode[0].childNode[0].childNode[0].key[0];
                    }

                    node.key[i] = save;
                    key_count++;
                }
            }

            Delete(new Key(save.key_value), node);

        }
        public static void Combine(Key key, Node node)
        {
            if (count == 0)
            {
                node.key.Add(node.parentNode.key[0]);
                node.key.Add(node.parentNode.childNode[1].key[0]);
                Arrange_key(node);

                for (int i = 0; i < node.parentNode.childNode[1].childNode.Count; i++)
                {
                    node.childNode.Add(node.parentNode.childNode[1].childNode[i]);
                }

                Arrange_node(node);

                node.parentNode.key.RemoveAt(0);
                node.parentNode.childNode.RemoveAt(1);

                if (node.parentNode.isRoot == true && node.parentNode.key.Count == 1)
                {
                    node.isRoot = true;

                }
            }
            else if (count == 1)
            {
                node.key.Add(node.parentNode.key[count - 1]);
                node.key.Add(node.parentNode.childNode[count - 1].key[0]);
                Arrange_key(node);

                for (int i = 0; i < node.parentNode.childNode[0].childNode.Count; i++)
                {
                    node.childNode.Add(node.parentNode.childNode[count - 1].childNode[i]);
                }

                Arrange_node(node);

                node.parentNode.key.RemoveAt(count - 1);
                node.parentNode.childNode.RemoveAt(count - 1);

                if (node.parentNode.isRoot == true && node.parentNode.key.Count == 1)
                {
                    node.isRoot = true;

                }
            }
            else if (count == 2)
            {
                node.key.Add(node.parentNode.key[count - 1]);
                node.key.Add(node.parentNode.childNode[count - 1].key[0]);
                Arrange_key(node);

                for (int i = 0; i < node.parentNode.childNode[0].childNode.Count; i++)
                {
                    node.childNode.Add(node.parentNode.childNode[count - 1].childNode[i]);
                }

                Arrange_node(node);

                node.parentNode.key.RemoveAt(count - 1);
                node.parentNode.childNode.RemoveAt(count - 1);

                if (node.parentNode.isRoot == true && node.parentNode.key.Count == 1)
                {
                    node.isRoot = true;

                }
            }
            else if (count == 3)
            {
                node.key.Add(node.parentNode.key[count - 1]);
                node.key.Add(node.parentNode.childNode[count - 1].key[0]);
                Arrange_key(node);

                for (int i = 0; i < node.parentNode.childNode[0].childNode.Count; i++)
                {
                    node.childNode.Add(node.parentNode.childNode[count - 1].childNode[i]);
                }

                Arrange_node(node);

                node.parentNode.key.RemoveAt(count - 1);
                node.parentNode.childNode.RemoveAt(count - 1);

                if (node.parentNode.isRoot == true && node.parentNode.key.Count == 1)
                {
                    node.isRoot = true;

                }
            }
        }
        public static void Borrow(Key key, Node node)
        {
            if (fullBrotherNode.key[0].key_value > node.key[0].key_value) // 첫번째 키를 부모노드한테 줘야할때
            {
                fullBrotherNode.parentNode.key.Add(fullBrotherNode.key[0]);
                Arrange_key(fullBrotherNode.parentNode);
                fullBrotherNode.key.RemoveAt(0);

                node.key.Add(node.parentNode.key[count]);
                Arrange_key(node);

                node.parentNode.key.RemoveAt(count);
            }
            else if (fullBrotherNode.key[0].key_value < node.key[0].key_value)
            {
                fullBrotherNode.parentNode.key.Add(fullBrotherNode.key[fullBrotherNode.key.Count - 1]);
                Arrange_key(fullBrotherNode.parentNode);
                fullBrotherNode.key.RemoveAt(fullBrotherNode.key.Count - 1);

                node.key.Add(node.parentNode.key[count + 1]);
                Arrange_key(node);

                node.parentNode.key.RemoveAt(count + 1);
            }
            //for (int i = 0; i < node.parentNode.key.Count; i++)
            //{
            //    if(node.parentNode.key[i].key_value < node.key[0].key_value && node.parentNode.key[i].key_value > fullBrotherNode.key[0].key_value
            //        || node.parentNode.key[i].key_value > node.key[0].key_value && node.parentNode.key[i].key_value < fullBrotherNode.key[0].key_value)
            //    {
            //        if(fullBrotherNode.key[0].key_value > node.key[0].key_value)
            //        {
            //            Key save = fullBrotherNode.key[0];
            //            node.key.Add(node.parentNode.key[i]);
            //            Arrange_key(node);
            //            node.parentNode.key.Add(save);
            //            Arrange_key(node.parentNode);
            //            fullBrotherNode.key.RemoveAt(0);

            //            if(fullBrotherNode.childNode.Count != 0) // (수정)
            //            {
            //                node.childNode.Add(fullBrotherNode.childNode[0]);
            //                fullBrotherNode.childNode.RemoveAt(0);
            //            } 
            //        }
            //        else
            //        {
            //            Key save = fullBrotherNode.key[fullBrotherNode.key.Count - 1];
            //            node.key.Add(node.parentNode.key[i]);
            //            Arrange_key(node);
            //            node.parentNode.key.Add(save);
            //            Arrange_key(node.parentNode);
            //            fullBrotherNode.key.RemoveAt(fullBrotherNode.key.Count - 1);

            //            if(fullBrotherNode.childNode.Count != 0) // (수정)
            //            {
            //                node.childNode.Add(fullBrotherNode.childNode[(fullBrotherNode.childNode.Count - 1)]);
            //                fullBrotherNode.childNode.RemoveAt(fullBrotherNode.childNode.Count - 1);
            //            }             
            //        }
            //    }
            //}
        }
        public static void Delete(Key key, Node node) // 메인함수
        {
            if (node.isRoot == true) // 루트노드라면
            {
                if (ContainsKey(key, node) == true) // 노드안에 key 값이 있다면
                {
                    if (key_count == 1) // 스왑이 안된상태!
                    {
                        if (node.childNode.Count == 0) // 리프노드라면
                        {
                            Remove(key, node);
                        }
                        else // 리프노드가 아니라면
                        {
                            Swap(key, node);
                        }
                    }
                    else // 스왑이 된상태!
                    {
                        //key_count--; // (수정)
                        FindNext(key, node);
                    }
                }
                else
                {
                    FindNext(key, node);
                }
            }
            else // 루트노드가 아니라면
            {
                if (node.key.Count < (int)((Node.m) / 2)) // 현재 노드가 풍족하지 않다면
                {
                    if (IsBrotherNodeFull(key, node) == true) // 형제노드가 풍족하다면
                    {
                        Borrow(key, node);
                        if (ContainsKey(key, node) == true) // 노드안에 key 값이 있다면
                        {
                            if (key_count == 1) // 스왑이 안된상태
                            {
                                if (node.childNode.Count == 0) // 리프노드라면
                                {
                                    Remove(key, node);
                                }
                                else // 리프노드가 아니라면
                                {
                                    Swap(key, node); // 다시 삭제하는 메소드 만들기!
                                }
                            }
                            else // 스왑이 된상태
                            {

                                FindNext(key, node);
                            }
                        }
                        else // 노드안에 key 값이 없다면
                        {
                            FindNext(key, node);
                        }
                    }
                    else if (IsBrotherNodeFull(key, node) == false) // 형제노드도 결핍하다면
                    {
                        Combine(key, node);
                        if (ContainsKey(key, node) == true) // 노드안에 key 값이 있다면
                        {
                            if (node.childNode.Count == 0) // 리프노드라면
                            {
                                Remove(key, node);
                            }
                            else // 리프노드가 아니라면
                            {
                                Swap(key, node); // 다시 삭제하는 메소드 만들기!
                            }
                        }
                        else // 노드안에 key 값이 없다면
                        {
                            FindNext(key, node);
                        }
                    }
                }
                else // 현재 노드가 풍족하다면
                {
                    if (key_count == 1)
                    {
                        if (ContainsKey(key, node) == true) // 노드안에 key 값이 있다면
                        {
                            if (node.childNode.Count == 0) // 리프노드라면
                            {
                                Remove(key, node);
                            }
                            else // 리프노드가 아니라면
                            {
                                Swap(key, node); // 다시 삭제하는 메소드 만들기!
                            }
                        }
                        else
                        {
                            FindNext(key, node);
                        }
                    }
                    else
                    {
                        FindNext(key, node);
                    }
                }
            }
        }
        class Test
        {
            public int value;
            public Test left;
            public Test right;

            public Test(int value)
            {
                this.value = value;
            }

        }
        class Program
        {
            static void Main(string[] args)
            {
                
            }
        }
    }
}
