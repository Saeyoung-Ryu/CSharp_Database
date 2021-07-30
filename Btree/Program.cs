using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Btree
{
    class PriorityQueue<T> where T : IComparable<T>
    {
        List<T> _heap = new List<T>();

        // O(logN)
        public void Enqueue(T data)
        {
            // 힙의 맨 끝에 새로운 데이터를 삽입한다
            _heap.Add(data);

            int now = _heap.Count - 1;
            // 도장깨기를 시작
            while (now > 0)
            {
                // 도장깨기를 시도
                int next = (now - 1) / 2;
                if (_heap[now].CompareTo(_heap[next]) > 0)
                    break; // 실패

                // 두 값을 교체한다
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                // 검사 위치를 이동한다
                now = next;
            }
        }

        // O(logN)
        public T Dequeue()
        {
            // 반환할 데이터를 따로 저장
            T ret = _heap[0];

            // 마지막 데이터를 루트로 이동한다
            int lastIndex = _heap.Count - 1;
            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex);
            lastIndex--;

            // 역으로 내려가는 도장깨기 시작
            int now = 0;
            while (true)
            {
                int left = 2 * now + 1;
                int right = 2 * now + 2;

                int next = now;
                // 왼쪽값이 현재값보다 크면, 왼쪽으로 이동
                if (left <= lastIndex && _heap[next].CompareTo(_heap[left]) > 0)
                    next = left;
                // 오른값이 현재값(왼쪽 이동 포함)보다 크면, 오른쪽으로 이동
                if (right <= lastIndex && _heap[next].CompareTo(_heap[right]) > 0)
                    next = right;

                // 왼쪽/오른쪽 모두 현재값보다 작으면 종료
                if (next == now)
                    break;

                // 두 값을 교체한다
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;
                // 검사 위치를 이동한다
                now = next;
            }

            return ret;
        }

        public int Count()
        {
            return _heap.Count;
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
        public static Node rootNode = new Node(null) { isRoot = true };

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

                        for (int i = 0; i < node.parentNode.childNode.Count; i++)
                        {
                            if(node.parentNode.childNode[i].key[0].key_value == node_left.key[0].key_value)
                            {
                                node.parentNode.childNode[i] = node_left;
                                node.parentNode.childNode.Add(node_right);
                                Arrange_node(node.parentNode);
                            }
                        }

                        int save = (int)((Node.m - 1) / 2); //0614 수정
                        Key save_key = node.key[save];
                        //node.key.Clear();


                        //node.key.Add(save_key);
                        //Arrange_key(node);

                        //node.childNode.Clear();

                        //node_left.parentNode = node.parentNode;
                        //node_right.parentNode = node.parentNode;

                        //AddChild(node_left, node);
                       // AddChild(node_right, node);

                        //Arrange_node(node);

                        node.parentNode.key.Add(save_key);
                        Arrange_key(node.parentNode);

                        //node = node.childNode[0];
                        //AddChild(node.childNode[1], node.parentNode);

                        //Arrange_node(node.parentNode);
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



                    for (int i = 0; i < node.parentNode.childNode.Count; i++)
                    {
                        if(node.parentNode.childNode[i].key[0].key_value == node.key[0].key_value)
                        {                            
                            node.parentNode.childNode.RemoveAt(i);
                            AddChild(node_left, node.parentNode);
                            AddChild(node_right, node.parentNode);
                            Arrange_node(node.parentNode);
                        }
                    }

                    //이미 키를 추가했는데도 count 가 1이라는것은 루트노드 였다는 뜻이므로,
                    //if(node.parentNode.key.Count ==0 )은 생략한다

                    //if (node.parentNode.key.Count == 2)
                    //{
                    //    if (key.key_value < node.parentNode.key[0].key_value) // 추가할라는 값 적당한 위치에 크기비교하여 삽입하기!
                    //    {
                    //        node.parentNode.childNode[0] = node_left;
                    //        AddChild(node_right, node.parentNode);
                    //        //node.parentNode.childNode.Add(node_right);
                    //        Arrange_node(node.parentNode);
                    //    }
                    //    else
                    //    {
                    //        node.parentNode.childNode[1] = node_left;
                    //        AddChild(node_right, node.parentNode);

                    //        Arrange_node(node.parentNode);
                    //    }
                    //}
                    //else if (node.parentNode.key.Count == 3)
                    //{
                    //    if (key.key_value < node.parentNode.key[0].key_value) // 추가할라는 값 적당한 위치에 크기비교하여 삽입하기!
                    //    {
                    //        node.parentNode.childNode[0] = node_left;
                    //        AddChild(node_right, node.parentNode);

                    //        Arrange_node(node.parentNode);
                    //    }
                    //    else if (key.key_value > node.parentNode.key[0].key_value && key.key_value < node.parentNode.key[1].key_value)
                    //    {
                    //        node.parentNode.childNode[1] = node_left;
                    //        AddChild(node_right, node.parentNode);

                    //        Arrange_node(node.parentNode);
                    //    }
                    //    else
                    //    {
                    //        node.parentNode.childNode[2] = node_left;
                    //        AddChild(node_right, node.parentNode);

                    //        Arrange_node(node.parentNode);
                    //    }

                    //}
                    //else if (node.parentNode.key.Count == 4)
                    //{
                    //    if (key.key_value < node.parentNode.key[0].key_value) // 추가할라는 값 적당한 위치에 크기비교하여 삽입하기!
                    //    {
                    //        node.parentNode.childNode[0] = node_left;
                    //        AddChild(node_right, node.parentNode);

                    //        Arrange_node(node.parentNode);
                    //    }
                    //    else if (key.key_value > node.parentNode.key[0].key_value && key.key_value < node.parentNode.key[1].key_value)
                    //    {
                    //        node.parentNode.childNode[1] = node_left;
                    //        AddChild(node_right, node.parentNode);

                    //        Arrange_node(node.parentNode);
                    //    }
                    //    else if (key.key_value > node.parentNode.key[1].key_value && key.key_value < node.parentNode.key[2].key_value)
                    //    {
                    //        node.parentNode.childNode[2] = node_left;
                    //        AddChild(node_right, node.parentNode);

                    //        Arrange_node(node.parentNode);
                    //    }
                    //    else
                    //    {
                    //        node.parentNode.childNode[3] = node_left;
                    //        AddChild(node_right, node.parentNode);

                    //        Arrange_node(node.parentNode);
                    //    }

                    //}



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
                    return;
                }
            }
        }

        public static void Swap_FindSavedKey(int i, Node node)
        {
            if (node.childNode[0].childNode.Count == 0)
            {
                save = node.childNode[i + 1].key[0];
                return;
            }
            else
            {
                Swap_FindSavedKey(i, node.childNode[0]);
            }
        }
        public static void Swap(Key key, Node node)
        {

            for (int i = 0; i < node.key.Count; i++)
            {
                if (key.key_value == node.key[i].key_value)
                {
                    //Swap_FindSavedKey(i, node);
                    //break;
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
                    else if (node.childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode.Count == 0)
                    {
                        save = node.childNode[i + 1].childNode[0].childNode[0].childNode[0].childNode[0].key[0];
                    }
                    else if (node.childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode.Count == 0)
                    {
                        save = node.childNode[i + 1].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].key[0];
                    }
                    else if (node.childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode.Count == 0)
                    {
                        save = node.childNode[i + 1].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].key[0];
                    }
                    else if (node.childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode.Count == 0)
                    {
                        save = node.childNode[i + 1].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].key[0];
                    }
                    else if (node.childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode.Count == 0)
                    {
                        save = node.childNode[i + 1].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].key[0];
                    }
                    else if (node.childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode.Count == 0)
                    {
                        save = node.childNode[i + 1].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].key[0];
                    }
                    else if (node.childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode.Count == 0)
                    {
                        save = node.childNode[i + 1].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].key[0];
                    }
                    else if (node.childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode.Count == 0)
                    {
                        save = node.childNode[i + 1].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].childNode[0].key[0];
                    }

                    // 0722 더 추가하기! 그리고 복붙하기!
                    node.key[i] = save;

                }


            }

            for (int i = 0; i < node.key.Count; i++)
            {
                if (node.key[i].key_value == save.key_value)
                {
                    count = i + 1;
                    Delete(new Key(node.key[i].key_value), node.childNode[i + 1]);
                }
            }

            //Swap_Delete(); // 526

        }
        public static void Swap_Delete(Key key_original, Key key_swapped, Node node)
        {

        }
        //public static void Combine(Key key, Node node)
        //{
        //    if (count == 0)
        //    {
        //        node.key.Add(node.parentNode.key[0]);
        //        node.key.Add(node.parentNode.childNode[1].key[0]);
        //        Arrange_key(node);

        //        for (int i = 0; i < node.parentNode.childNode[1].childNode.Count; i++)
        //        {
        //            //node.parentNode.childNode[1].childNode[i].parentNode = node;

        //            //node.childNode.Add(node.parentNode.childNode[1].childNode[i]);
        //            AddChild(node.parentNode.childNode[1].childNode[i], node); // 부모까지 알아서 정해주는 메소드
        //        }

        //        Arrange_node(node);

        //        node.parentNode.key.RemoveAt(0);
        //        node.parentNode.childNode.RemoveAt(1);

        //        if (node.parentNode.isRoot == true && node.parentNode.key.Count == 0)
        //        {
        //            node.isRoot = true;
        //            node.parentNode = null;
        //            rootNode = node;

        //        }
        //    }
        //    else if (count == 1)
        //    {
        //        node.key.Add(node.parentNode.key[count - 1]);
        //        node.key.Add(node.parentNode.childNode[count - 1].key[0]);
        //        Arrange_key(node);

        //        for (int i = 0; i < node.parentNode.childNode[0].childNode.Count; i++)
        //        {
        //            AddChild(node.parentNode.childNode[count - 1].childNode[i], node);
        //            //node.childNode.Add(node.parentNode.childNode[count - 1].childNode[i]);
        //        }

        //        Arrange_node(node);

        //        node.parentNode.key.RemoveAt(count - 1);
        //        node.parentNode.childNode.RemoveAt(count - 1);

        //        if (node.parentNode.isRoot == true && node.parentNode.key.Count == 0)
        //        {
        //            node.isRoot = true;
        //            node.parentNode = node;
        //            rootNode = node;

        //        }
        //    }
        //    else if (count == 2)
        //    {
        //        node.key.Add(node.parentNode.key[count - 1]);
        //        node.key.Add(node.parentNode.childNode[count - 1].key[0]);
        //        Arrange_key(node);

        //        for (int i = 0; i < node.parentNode.childNode[0].childNode.Count; i++)
        //        {
        //            AddChild(node.parentNode.childNode[count - 1].childNode[i], node);
        //            //node.childNode.Add(node.parentNode.childNode[count - 1].childNode[i]);
        //        }

        //        Arrange_node(node);

        //        node.parentNode.key.RemoveAt(count - 1);
        //        node.parentNode.childNode.RemoveAt(count - 1);

        //        if (node.parentNode.isRoot == true && node.parentNode.key.Count == 0)
        //        {
        //            node.isRoot = true;
        //            node.parentNode = node;
        //            rootNode = node;

        //        }
        //    }
        //    else if (count == 3)
        //    {
        //        node.key.Add(node.parentNode.key[count - 1]);
        //        node.key.Add(node.parentNode.childNode[count - 1].key[0]);
        //        Arrange_key(node);

        //        for (int i = 0; i < node.parentNode.childNode[0].childNode.Count; i++)
        //        {
        //            AddChild(node.parentNode.childNode[count - 1].childNode[i], node);
        //            //node.childNode.Add(node.parentNode.childNode[count - 1].childNode[i]); 0610 수정    
        //        }

        //        Arrange_node(node);

        //        node.parentNode.key.RemoveAt(count - 1);
        //        node.parentNode.childNode.RemoveAt(count - 1);

        //        if (node.parentNode.isRoot == true && node.parentNode.key.Count == 0)
        //        {
        //            node.isRoot = true;
        //            node.parentNode = node;
        //            rootNode = node;

        //        }
        //    }
        //}
        public static void Combine(Key key, Node node)
        {
            if (count == 0)
            {
                node.key.Add(node.parentNode.key[0]);
                node.key.Add(node.parentNode.childNode[1].key[0]);
                Arrange_key(node);

                for (int i = 0; i < node.parentNode.childNode[1].childNode.Count; i++)
                {
                    //node.parentNode.childNode[1].childNode[i].parentNode = node;

                    //node.childNode.Add(node.parentNode.childNode[1].childNode[i]);
                    AddChild(node.parentNode.childNode[1].childNode[i], node); // 부모까지 알아서 정해주는 메소드
                }

                Arrange_node(node);

                node.parentNode.key.RemoveAt(0);
                node.parentNode.childNode.RemoveAt(1);

                if (node.parentNode.isRoot == true && node.parentNode.key.Count == 0)
                {
                    node.isRoot = true;
                    node.parentNode = null;
                    rootNode = node;

                }
            }
            else if (count == 1)
            {
                node.key.Add(node.parentNode.key[count - 1]);
                node.key.Add(node.parentNode.childNode[count - 1].key[0]);
                Arrange_key(node);

                for (int i = 0; i < node.parentNode.childNode[0].childNode.Count; i++)
                {
                    AddChild(node.parentNode.childNode[count - 1].childNode[i], node);
                    //node.childNode.Add(node.parentNode.childNode[count - 1].childNode[i]);
                }

                Arrange_node(node);

                node.parentNode.key.RemoveAt(count - 1);
                node.parentNode.childNode.RemoveAt(count - 1);

                if (node.parentNode.isRoot == true && node.parentNode.key.Count == 0)
                {
                    node.isRoot = true;
                    node.parentNode = node;
                    rootNode = node;

                }
            }
            else if (count == 2)
            {
                node.key.Add(node.parentNode.key[count - 1]);
                node.key.Add(node.parentNode.childNode[count - 1].key[0]);
                Arrange_key(node);

                for (int i = 0; i < node.parentNode.childNode[1].childNode.Count; i++)
                {
                    AddChild(node.parentNode.childNode[count - 1].childNode[i], node);
                    //node.childNode.Add(node.parentNode.childNode[count - 1].childNode[i]);
                }

                Arrange_node(node);

                node.parentNode.key.RemoveAt(count - 1);
                node.parentNode.childNode.RemoveAt(count - 1);

                if (node.parentNode.isRoot == true && node.parentNode.key.Count == 0)
                {
                    node.isRoot = true;
                    node.parentNode = node;
                    rootNode = node;

                }
            }
            else if (count == 3)
            {
                node.key.Add(node.parentNode.key[count - 1]);
                node.key.Add(node.parentNode.childNode[count - 1].key[0]);
                Arrange_key(node);

                for (int i = 0; i < node.parentNode.childNode[2].childNode.Count; i++) //0723 수정
                {
                    AddChild(node.parentNode.childNode[count - 1].childNode[i], node);
                    //node.childNode.Add(node.parentNode.childNode[count - 1].childNode[i]); 0610 수정    
                }

                Arrange_node(node);

                node.parentNode.key.RemoveAt(count - 1);
                node.parentNode.childNode.RemoveAt(count - 1);

                if (node.parentNode.isRoot == true && node.parentNode.key.Count == 0)
                {
                    node.isRoot = true;
                    node.parentNode = node;
                    rootNode = node;

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

                if (fullBrotherNode.childNode.Count != 0)
                {
                    AddChild(fullBrotherNode.childNode[0], node);
                    //node.childNode.Add(fullBrotherNode.childNode[0]); // 0610 부모노드 오류 관련

                    fullBrotherNode.childNode.RemoveAt(0);
                }


                node.parentNode.key.RemoveAt(count);
            }
            else if (fullBrotherNode.key[0].key_value < node.key[0].key_value)
            {
                fullBrotherNode.parentNode.key.Add(fullBrotherNode.key[fullBrotherNode.key.Count - 1]);
                Arrange_key(fullBrotherNode.parentNode);
                fullBrotherNode.key.RemoveAt(fullBrotherNode.key.Count - 1);

                node.key.Add(node.parentNode.key[count]);
                Arrange_key(node);

                if (fullBrotherNode.childNode.Count != 0)
                {
                    AddChild(fullBrotherNode.childNode[fullBrotherNode.childNode.Count - 1], node);
                    //node.childNode.Add(fullBrotherNode.childNode[fullBrotherNode.childNode.Count - 1]);

                    fullBrotherNode.childNode.RemoveAt(fullBrotherNode.childNode.Count - 1);

                }

                node.parentNode.key.RemoveAt(count);
            }

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
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//        

        class Program
        {
            static void Main(string[] args)
            {
                //10 ~ 160 까지 순서대로 Insert 해보기
                //160 ~ 10 까지 순서대로 Insert 해보기
                //몇개 골라서 Delete 해보기 (루트노드, 리프노드, 일반노드 각각 한개씩)

                Btree.SetOrder(4);
                rootNode.isRoot = true;

                for (int i = 1; i <= 200; i++)
                {
                    Btree.Insert(new Key(i), rootNode);
                }

                Btree.Delete(new Key(96), rootNode);




                Console.WriteLine(rootNode.key[0].key_value);
                //Btree.Insert(new Key(30), rootNode);
                //Btree.Insert(new Key(50), rootNode);
                //Btree.Insert(new Key(40), rootNode);
                //Btree.Insert(new Key(60), rootNode);
                //Btree.Insert(new Key(70), rootNode);
                //Btree.Insert(new Key(80), rootNode);
                //Btree.Insert(new Key(10), rootNode);
                //Btree.Insert(new Key(15), rootNode);
                //Btree.Insert(new Key(100), rootNode);
                //Btree.Insert(new Key(90), rootNode); // 오류 발생!! 해결완료 0614
                //Btree.Insert(new Key(35), rootNode); // 오류 발생!! 해결완료 0614
                //Btree.Insert(new Key(11), rootNode);
                //Btree.Insert(new Key(12), rootNode);
                //Btree.Insert(new Key(13), rootNode);
                //Btree.Insert(new Key(14), rootNode);

                //Btree.Insert(new Key(10), rootNode);
                //Btree.Insert(new Key(20), rootNode);
                //Btree.Insert(new Key(30), rootNode);
                //Btree.Insert(new Key(40), rootNode);
                //Btree.Insert(new Key(50), rootNode);
                //Btree.Insert(new Key(60), rootNode);
                //Btree.Insert(new Key(70), rootNode);
                //Btree.Insert(new Key(80), rootNode);
                //Btree.Insert(new Key(90), rootNode);
                //Btree.Insert(new Key(100), rootNode);
                //Btree.Insert(new Key(110), rootNode);
                //Btree.Insert(new Key(120), rootNode);
                //Btree.Insert(new Key(130), rootNode); // 오류! 루트노드의 자식노드가 3개 있어야 하지만 2개밖에없음! 491번줄 수정시도, 수정완료
                //Btree.Insert(new Key(140), rootNode);
                //Btree.Insert(new Key(150), rootNode);
                //Btree.Insert(new Key(160), rootNode);
                //Btree.Insert(new Key(170), rootNode);

                //Btree.Insert(new Key(170), rootNode);
                //Btree.Insert(new Key(160), rootNode);
                //Btree.Insert(new Key(150), rootNode);
                //Btree.Insert(new Key(140), rootNode);
                //Btree.Insert(new Key(130), rootNode);
                //Btree.Insert(new Key(120), rootNode);
                //Btree.Insert(new Key(110), rootNode);
                //Btree.Insert(new Key(100), rootNode);
                //Btree.Insert(new Key(90), rootNode);
                //Btree.Insert(new Key(80), rootNode);
                //Btree.Insert(new Key(70), rootNode);
                //Btree.Insert(new Key(60), rootNode);
                //Btree.Insert(new Key(50), rootNode); 
                //Btree.Insert(new Key(40), rootNode);
                //Btree.Insert(new Key(30), rootNode);
                //Btree.Insert(new Key(20), rootNode);
                //Btree.Insert(new Key(10), rootNode);

                //Btree.Delete(new Key(10), rootNode);
                //Btree.Delete(new Key(40), rootNode);
                //Btree.Delete(new Key(120), rootNode);
                //Btree.Delete(new Key(80), rootNode);
            }
        }
    }
}
