using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    class Graph
    {
        int[,] adj = new int[6, 6] 
        {
           // 0 1 2 3 4 5 번 노드
            { 0,1,0,1,0,0},
            { 1,0,1,1,0,0},
            { 0,1,0,0,0,0},
            { 1,1,0,0,1,0},
            { 0,0,0,1,0,1},
            { 0,0,0,0,1,0},
        };

        List<int>[] adj2 = new List<int>[] 
        {
            new List<int>() {1,3},
            new List<int>() {0,2,3},
            new List<int>() {1},
            new List<int>() {0,1,4},
            new List<int>() {3,5},
            new List<int>() {4},
        };

        //DFS(Depth First Search 깊이 우선 탐색)
        //BFS(Breathe First Search 너비 우선 탐색)
        // 1) 우선 now 부터 방문
        // 2) now 와 연결된 정점들을 하나씩 확인해서 아직 미방문 상태라면 방문한다.

        bool[] visited = new bool[6]; 
        public void DFS(int now) // 매게값은 던전 시작 위치(now), // 2차원 배열 형식의 DFS
        {
            Console.WriteLine(now);
            visited[now] = true;

            for (int next = 0; next < 6; next++)
            {
                if(adj[now, next] == 0)
                {
                    continue; // 연결 안되있으면 스킵
                }
                if (visited[next])
                {
                    continue; // 이미 방문했으면 스킵
                }
                DFS(next); //연결이 되어있고 방문을 안했다면
            }
        }

        public void DFS2(int now) // LIST 형식의 DFS
        {
            Console.WriteLine(now);
            visited[now] = true;

            foreach(int next in adj2[now])
            {
                if (visited[next])
                {
                    continue; // 이미 방문했으면 스킵
                }
                DFS2(next);
            }
        }

        public void SearchAll() // 만약에 줄이 끊어져 있으면
        {
            visited = new bool[6];
            for (int now = 0; now < 6; now++)
            {
                if(visited[now] == false)
                {
                    DFS(now);
                }
            }
        }

        public void BFS(int start)
        {
            bool[] found = new bool[6];

            Queue<int> q = new Queue<int>();

            q.Enqueue(start);
            found[start] = true;

            while(q.Count > 0)
            {
                int now = q.Dequeue();
                Console.WriteLine(now);

                for (int next = 0; next < 6; next++)
                {
                    if(adj[now, next] == 0)
                    {
                        continue; // 인접하지 않았으면 스킵
                    }
                    if (found[next])
                    {
                        continue; // 이미 발견한 애라면 스킵
                    }

                    q.Enqueue(next);
                    found[next] = true;

                }
            }
        }
    }
    class TreeNode<T> 
    {
        public T Data { get; set; }
        public List<TreeNode<T>> Children { get; set; } = new List<TreeNode<T>>();       
    }
    //class Vertex
    //{
    //    //public List<Vertex> edges = new List<Vertex>();

    //    //void CreateGraph()
    //    //{
    //    //    List<Vertex> v = new List<Vertex>(6);
    //    //    {
    //    //        new Vertex();
    //    //        new Vertex();
    //    //        new Vertex();
    //    //        new Vertex();
    //    //        new Vertex();
    //    //        new Vertex();
    //    //    };

    //    //    v[0].edges.Add(v[1]);
    //    //    v[0].edges.Add(v[3]);
    //    //    v[1].edges.Add(v[0]);
    //    //    v[1].edges.Add(v[2]);
    //    //    v[1].edges.Add(v[3]);
    //    //    v[3].edges.Add(v[4]);
    //    //    v[5].edges.Add(v[4]);
    //    //}

        
    //}
    class MyList<T>
    //배열 사이즈를 유동적으로 줄였다가늘렸다가 가능. 인덱스값으로 searching이 쉽게 가능
    {
        const int DEFAULT_SIZE = 1;
        public T[] _data = new T[DEFAULT_SIZE];

        
        public int Count; // 실제로 사용중인 데이터 개수
        public int Capacity { get { return _data.Length; } } //예약된 데이터 개수

        public void Add(T item)
        {
            //1. 공간이 충분히 남아있는지 확인하기
            if(Count >= Capacity)
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
            for(int i = index; i < Count - 1; i++)
            
                _data[i] = _data[i + 1];
            _data[Count - 1] = default(T);            
            Count--;
        }
    }   
    class MyLinkedListNode<T>
    {
        public T Data;
        public MyLinkedListNode<T> Next;
        public MyLinkedListNode<T> Prev;
    }
    class MyLinkedList<T> 
    //중간 삽입 삭제가 빠르고 편리하게 가능.
    {
        public MyLinkedListNode<T> Head = null; // first
        public MyLinkedListNode<T> Tail = null; // last
        public int Count = 0;

        public MyLinkedListNode<T> AddLast(T data)
        {
            MyLinkedListNode<T> newMyLinkedListNode = new MyLinkedListNode<T>();
            newMyLinkedListNode.Data = data;

            if(Head == null) // 만약에 아직 방이 아예 없었다면 새로 추가한 첫번째 방이 곧 HEAD 이다.
            {
                Head = newMyLinkedListNode;
            }

            //기존의 [마지막방]과 [새로 추가되는 방]을 연결해준다
            if(Tail != null)
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
            if(Head == MyLinkedListNode)
            {
                Head = Head.Next;
            }

            //기존의 마지막 방의 이전 방을 마지막 방으로 인정한다
            if(Tail == MyLinkedListNode)
            {
                Tail = Tail.Prev;
            }

            if(MyLinkedListNode.Prev != null)
            {
                MyLinkedListNode.Prev.Next = MyLinkedListNode.Next;
            }

            if(MyLinkedListNode.Next != null)
            {
                MyLinkedListNode.Next.Prev = MyLinkedListNode.Prev;
            }

            Count--;
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
            while(now > 0)
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
                if(left <= lastindex && _heap[next].CompareTo(_heap[left]) < 0)                
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
    class Knight : IComparable<Knight>
    {
        //public int ID { get; set; }
        public int ID;

        public Knight(int _ID)
        {
            ID = _ID;
        }
        

        public int CompareTo(Knight other)
        {
            if (ID == other.ID)
                return 0;
            return ID > other.ID ? 1 : -1; // 부등호 바뀌면 정렬되는 방향이 다르다!!!!
        }
    }

   
    class Start
    {
        
        public static TreeNode<string> MakeTree()
        {
            TreeNode<string> root = new TreeNode<string>() { Data = "R1 개발실" };
            {
                {
                    TreeNode<string> node = new TreeNode<string>() { Data = "디자인팀" };
                    node.Children.Add(new TreeNode<string>() { Data = "전투" });
                    node.Children.Add(new TreeNode<string>() { Data = "경제" });
                    node.Children.Add(new TreeNode<string>() { Data = "스토리" });
                    root.Children.Add(node);
                }
                {
                    TreeNode<string> node = new TreeNode<string>() { Data = "프로그래밍팀" };
                    node.Children.Add(new TreeNode<string>() { Data = "서버" });
                    node.Children.Add(new TreeNode<string>() { Data = "클라" });
                    node.Children.Add(new TreeNode<string>() { Data = "엔진" });
                    root.Children.Add(node);
                }
                {
                    TreeNode<string> node = new TreeNode<string>() { Data = "아트팀" };
                    node.Children.Add(new TreeNode<string>() { Data = "배경" });
                    node.Children.Add(new TreeNode<string>() { Data = "캐릭터" });
                    root.Children.Add(node);
                }

            }
            return root;

        }

        
        static void Main(string[] args)
        {
            //TreeNode<string> root = MakeTree();
            //PriorityQueue<int> pri = new PriorityQueue<int>();
            //pri.Push(1);
            //pri.Push(3);
            //pri.Push(2);
            //pri.Push(0);

            //Console.WriteLine(pri.Pop());
            //PriorityQueue<Knight> a = new PriorityQueue<Knight>();

            //Knight knight1 = new Knight(1);
            //Knight knight2 = new Knight(2);
            //Knight knight5 = new Knight(5);
            //a.Enqueue(knight2);
            //a.Enqueue(knight5);
            //a.Enqueue(knight1);


            //Console.WriteLine(a.Dequeue().ID+ "" + a.Dequeue().ID);

            Graph g = new Graph();
            g.BFS(10);




        }
    }
}
