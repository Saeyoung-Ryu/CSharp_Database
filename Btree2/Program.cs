using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Btree2
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
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ//
        public static void Split(Key key, Node node)
        {

            int count = node.key.Count;

            if (node.isRoot == true && node.key.Count == Node.m - 1) // 루트노드를 쪼개는 상황일떄
            {
                Node.level++;

                if (node.childNode.Count == 0) // 루트노드가 자식이 없는 상태에서 쪼개질때
                {

                    Node node_left = new Node(node);

                    Node node_right = new Node(node);

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

                    Node node_left = new Node(node);

                    Node node_right = new Node(node);

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
                    Node node_left = new Node(node);

                    Node node_right = new Node(node);

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

                    Node node_left = new Node(node);

                    Node node_right = new Node(node);




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

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public static bool IsEnough(Node node)
        {
            if (node.key.Count < (int)(Node.m / 2))
            {
                return false;
            }
            return true;
        }
        public static void Combine(Key key, Node node)
        {

        }
        public static void Borrow(Key key, Node node)
        {

        }
        public static void Remove(Key key, Node node)
        {

        }
        public static void Swap(Key key, Node node)
        {

        }
        public static void CheckFull(Key key, Node node, int index)
        {
            if (node.isRoot == false)
            {
                if (IsEnough(node) == true) // 현재 노드가 부유하다면 그 아래 노드로 이동
                {
                    node = node.childNode[index]; // ?
                    Look(key, node);
                }
                else // 현재 노드가 부유하지 않다면
                {
                    //형제노드도 결핍하면 Combine(), 형제노드 부유하면 Borrow()
                    //위 두 함수중 하나로 부유하게 만들어준 다음에 Look() 호출

                }
            }

        }
        public static void CheckFull(Key key, Node node)
        {
            if (IsEnough(node) == true)
            {

            }
            else
            {
                //형제노드도 결핍하면 Combine(), 형제노드 부유하면 Borrow()
                //만약 그 값이 내부노드 안에있다면 Swap함수를 호출하고, 루트노드에 존재했다면 Remove함수를 호출한다!

            }
        } //노드안에 찾고자하는 키값이 존재하여 더이상 하향탐색을 할 필요가 없을때!
        public static void Look(Key key, Node node)
        {
            //주의할점은 루트노드가 결핍해도, 그 루트노드는 풍족상태로 만들지 않고 그냥 넘어간다!!
            if (node.key.Count == 1)
            {
                if (node.key[0].key_value > key.key_value)
                {
                    node = node.childNode[0];
                }
                else if (node.key[0].key_value < key.key_value)
                {
                    node = node.childNode[1];
                }

                else if (node.key[0].key_value == key.key_value) // 키 값이 루트노드 안에 포함되어 있다면
                {
                    if (node.isRoot == true && node.childNode.Count != 0) // 내부 루트노드일떄
                    {
                        Swap(key, node);
                    }
                    else if (node.isRoot == true && node.childNode.Count == 0) // 루트노드 겸 리프노드일경우
                    {
                        Remove(key, node);
                    }
                }
            }
            else if (node.key.Count == 2) // 완성본
            {
                if (node.key[0].key_value > key.key_value)
                {
                    CheckFull(key, node, 0);
                }
                else if (node.key[0].key_value < key.key_value && node.key[1].key_value > key.key_value)
                {
                    CheckFull(key, node, 1);

                }
                else if (node.key[1].key_value < key.key_value)
                {
                    CheckFull(key, node, 2);

                }

                else if (node.key[0].key_value == key.key_value || node.key[1].key_value == key.key_value)
                {
                    if (node.childNode.Count != 0) // 내부노드일떄
                    {
                        CheckFull(key, node);
                    }
                    else if (node.childNode.Count == 0) // 리프노드일때
                    {
                        CheckFull(key, node);
                    }
                }
            }
            else if (node.key.Count == 3)
            {

            }
        }



        public static void Delete(Key key, Node node)
        {
            Look(key, node);
        }

        class Test
        {
            public static bool Isvalue5(int value)
            {
                if(value == 5)
                {
                    return true;
                }
                return false;
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                
                if(Test.Isvalue5(4) == true)
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }






            }
        }
    }
}

