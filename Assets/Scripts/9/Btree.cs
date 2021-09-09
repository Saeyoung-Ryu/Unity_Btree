using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

public class Btree : MonoBehaviour
{
    public static Btree instance_Btree = new Btree();

    public static Node nodechanged; // ��� Ž���ҋ� ����        
    public static int count; // ������尡 ������� �ƴ��� Ȯ���Ҷ�����        
    public static int key_count = 1;
    public static Key save;
    public static Node fullBrotherNode;
    public static Node rootNode = new Node(null) { isRoot = true };

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
    void Awake()
    {
        if(instance_Btree == null)
        {
            instance_Btree = this;
        }
    }
    public static void SetOrder(int value)
    {
        Node.m = value;
    } // ���� ��Ʈ������ ���ϴ� �Լ�(¦�� ���� �ϴ�)
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
    } // ��� ���� Ű �� ����������� ����
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


    } // �ڽ� ��� ����������� ����
      //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
    public static void Split(Key key, Node node)
    {

        int count = node.key.Count;

        if (node.isRoot == true && node.key.Count == Node.m - 1) // ��Ʈ��带 �ɰ��� ��Ȳ�ϋ�
        {
            Node.level++;

            if (node.childNode.Count == 0) // ��Ʈ��尡 �ڽ��� ���� ���¿��� �ɰ�����
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
                else if (Node.m % 2 != 0) // Ȧ���϶� ��尪 �ִ� ��� (�߰����� ��ū �ְ� ���� �ö�)
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




                if (node.key.Count == 1) // Insert �Լ���� ���� �ִ�. ������忡�� �߰�
                {
                    if (key.key_value < node.key[0].key_value) // �߰��Ҷ�� �� ������ ��ġ�� ũ����Ͽ� �����ϱ�!
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
                    if (key.key_value < node.key[0].key_value) // �߰��Ҷ�� �� ������ ��ġ�� ũ����Ͽ� �����ϱ�!
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
                    if (key.key_value < node.key[0].key_value) // �߰��Ҷ�� �� ������ ��ġ�� ũ����Ͽ� �����ϱ�!
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
                    if (key.key_value < node.key[0].key_value) // �߰��Ҷ�� �� ������ ��ġ�� ũ����Ͽ� �����ϱ�!
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
            else // ��Ʈ��尡 �ڽ��� �մ� ���¿��� �ɰ�����
            {

                Node node_left = new Node(node) { parentNode = node.parentNode };

                Node node_right = new Node(node) { parentNode = node.parentNode };

                if (Node.m % 2 == 0)
                {
                    for (int i = 0; i < (int)(Node.m / 2 - 0.5); i++) // Ű�� �Ҵ��ϱ�(���� ��忡)
                    {
                        node_left.key.Add(node.key[i]);
                        Arrange_key(node_left);
                    }

                    for (int i = count - 1; i > Node.m / 2 - 0.5; i--) // Ű�� �Ҵ��ϱ�(������ ��忡)
                    {
                        node_right.key.Add(node.key[i]);
                        Arrange_key(node_right);
                    }
                }
                else if (Node.m % 2 != 0)
                {
                    for (int i = 0; i < (int)(Node.m / 2); i++) // Ű�� �Ҵ��ϱ�(���� ��忡)
                    {
                        node_left.key.Add(node.key[i]);
                        Arrange_key(node_left);
                    }

                    for (int i = count - 1; i > Node.m / 2; i--) // Ű�� �Ҵ��ϱ�(������ ��忡)
                    {
                        node_right.key.Add(node.key[i]);
                        Arrange_key(node_right);
                    }
                }

                if (Node.m % 2 == 0) // ¦�� �����϶�
                {
                    for (int i = 0; i < Node.m / 2; i++) // �ڽĳ�� �Ҵ��ϱ�(���� ��忡)
                    {
                        //node_left.childNode.Add(node.childNode[i]);
                        AddChild(node.childNode[i], node_left);
                        Arrange_node(node_left);
                    }

                    for (int i = Node.m - 1; i >= Node.m / 2; i--) // �ڽĳ�� �Ҵ��ϱ�(������ ��忡)
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
                else if (Node.m % 2 != 0) // Ȧ�� �����϶� -0.5 ���ֱ�!
                {
                    for (int i = 0; i < (int)(Node.m / 2) + 1; i++) // �ڽĳ�� �Ҵ��ϱ�(���� ��忡)
                    {
                        //node_left.childNode.Add(node.childNode[i]);
                        AddChild(node.childNode[i], node_left);
                        Arrange_node(node_left);
                    }

                    for (int i = Node.m - 1; i > (int)(Node.m / 2); i--) // �ڽĳ�� �Ҵ��ϱ�(������ ��忡)
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
        else if (node.isRoot == false && node.key.Count == Node.m - 1) // ��Ʈ��尡 �ƴ� ��带 �ɰ���. �̰��� ���ο� ���� ��带 �����ϴ°��� �ƴ� ���� �θ��忡 Ű�� �����ؾߵ�
        {
            if (node.childNode.Count != 0) // ������尡 �ƴѰ� �ɰ���
            {
                Node node_left = new Node(node) { parentNode = node.parentNode };

                Node node_right = new Node(node) { parentNode = node.parentNode };

                if (Node.m % 2 == 0)
                {
                    for (int i = 0; i < (int)(Node.m / 2 - 0.5); i++) // Ű�� �Ҵ��ϱ�(���� ��忡)
                    {
                        node_left.key.Add(node.key[i]);
                        Arrange_key(node_left);
                    }

                    for (int i = count - 1; i > Node.m / 2 - 0.5; i--) // Ű�� �Ҵ��ϱ�(������ ��忡)
                    {
                        node_right.key.Add(node.key[i]);
                        Arrange_key(node_right);
                    }
                }
                else if (Node.m % 2 != 0)
                {
                    for (int i = 0; i < (int)(Node.m / 2); i++) // Ű�� �Ҵ��ϱ�(���� ��忡)
                    {
                        node_left.key.Add(node.key[i]);
                        Arrange_key(node_left);
                    }

                    for (int i = count - 1; i > Node.m / 2; i--) // Ű�� �Ҵ��ϱ�(������ ��忡)
                    {
                        node_right.key.Add(node.key[i]);
                        Arrange_key(node_right);
                    }
                }


                if (Node.m % 2 == 0) // ¦�� �����϶�
                {
                    for (int i = 0; i < Node.m / 2; i++) // �ڽĳ�� �Ҵ��ϱ�(���� ��忡)
                    {
                        //node_left.childNode.Add(node.childNode[i]);
                        AddChild(node.childNode[i], node_left);
                        Arrange_node(node_left);
                    }

                    for (int i = Node.m - 1; i >= Node.m / 2; i--) // �ڽĳ�� �Ҵ��ϱ�(������ ��忡)
                    {
                        //node_right.childNode.Add(node.childNode[i]);
                        AddChild(node.childNode[i], node_right);
                        Arrange_node(node_right);
                    }

                    for (int i = 0; i < node.parentNode.childNode.Count; i++)
                    {
                        if (node.parentNode.childNode[i].key[0].key_value == node_left.key[0].key_value)
                        {
                            node.parentNode.childNode[i] = node_left;
                            node.parentNode.childNode.Add(node_right);
                            Arrange_node(node.parentNode);
                        }
                    }

                    int save = (int)((Node.m - 1) / 2); //0614 ����
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
                else if (Node.m % 2 != 0) // Ȧ�� �����϶�
                {
                    for (int i = 0; i < Node.m / 2; i++) // �ڽĳ�� �Ҵ��ϱ�(���� ��忡)
                    {
                        //node_left.childNode.Add(node.childNode[i]);
                        AddChild(node.childNode[i], node_left);
                        Arrange_node(node_left);
                    }

                    for (int i = Node.m - 1; i > Node.m / 2; i--) // �ڽĳ�� �Ҵ��ϱ�(������ ��忡)
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
            else // ��������϶�
            {

                Node node_left = new Node(node) { parentNode = node.parentNode };

                Node node_right = new Node(node) { parentNode = node.parentNode };




                if (Node.m % 2 == 0)
                {
                    for (int i = 0; i < (int)(Node.m / 2 - 0.5); i++) // Ű�� �Ҵ��ϱ�(���� ��忡)
                    {
                        node_left.key.Add(node.key[i]);
                        Arrange_key(node_left);
                    }

                    for (int i = count - 1; i > Node.m / 2 - 0.5; i--) // Ű�� �Ҵ��ϱ�(������ ��忡)
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
                    for (int i = 0; i < Node.m / 2 - 0.5; i++) // Ű�� �Ҵ��ϱ�(���� ��忡)
                    {
                        node_left.key.Add(node.key[i]);
                        Arrange_key(node_left);
                    }

                    for (int i = count - 1; i > Node.m / 2; i--) // Ű�� �Ҵ��ϱ�(������ ��忡)
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
                    if (node.parentNode.childNode[i].key[0].key_value == node.key[0].key_value)
                    {
                        node.parentNode.childNode.RemoveAt(i);
                        AddChild(node_left, node.parentNode);
                        AddChild(node_right, node.parentNode);
                        Arrange_node(node.parentNode);
                    }
                }

                //�̹� Ű�� �߰��ߴµ��� count �� 1�̶�°��� ��Ʈ��� ���ٴ� ���̹Ƿ�,
                //if(node.parentNode.key.Count ==0 )�� �����Ѵ�

                //if (node.parentNode.key.Count == 2)
                //{
                //    if (key.key_value < node.parentNode.key[0].key_value) // �߰��Ҷ�� �� ������ ��ġ�� ũ����Ͽ� �����ϱ�!
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
                //    if (key.key_value < node.parentNode.key[0].key_value) // �߰��Ҷ�� �� ������ ��ġ�� ũ����Ͽ� �����ϱ�!
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
                //    if (key.key_value < node.parentNode.key[0].key_value) // �߰��Ҷ�� �� ������ ��ġ�� ũ����Ͽ� �����ϱ�!
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
                    if (key.key_value < node.parentNode.key[0].key_value) // �߰��Ҷ�� �� ������ ��ġ�� ũ����Ͽ� �����ϱ�!
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
                    if (key.key_value < node.parentNode.key[0].key_value) // �߰��Ҷ�� �� ������ ��ġ�� ũ����Ͽ� �����ϱ�!
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
                    if (key.key_value < node.parentNode.key[0].key_value) // �߰��Ҷ�� �� ������ ��ġ�� ũ����Ͽ� �����ϱ�!
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
                    if (key.key_value < node.parentNode.key[0].key_value) // �߰��Ҷ�� �� ������ ��ġ�� ũ����Ͽ� �����ϱ�!
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
        if (node.childNode.Count != 0) // �ڽĳ�尡 �������� ����
        {
            if (node.key.Count == 1) // ����� key ���� 1���ϋ�
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

            else if (node.key.Count == 2) // ����� key ���� 2���ϋ�
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

            else if (node.key.Count == 3) // ����� key ���� 3���ϋ�. (�ϴ��� 4���� �ҰŴϱ� 3�������� ���� ����ϴ�)
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

            else if (node.key.Count == 4) // ����� key ���� 4���ϋ�. (5���� �ҰŴϱ� 4��)
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
        if (node.childNode.Count == 0 && node.isRoot == true) //�ڽ��� ���� ��Ʈ��� �� �ϳ��� �ִ� ����(�ʱ� ����) �ڽ��� �����Ƿ� gothrough �Լ� ȣ������ �ʴ´�
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
        else if (node.childNode.Count != 0 && node.isRoot == true)//�ڽ��� �ִ� ��Ʈ���
        {
            if (node.key.Count == Node.m - 1) // �� á�ٸ�
            {
                Split(key, node); // ** ���⼭�� ���ϸ� �ȵȴ�. ADD�Լ� ���� ���� �ȵ� ���� split ����!!
                GoThrough(key, node);
            }
            else // �� ��á�ٸ�
            {
                GoThrough(key, node);
            }
        }
        else if (node.childNode.Count != 0 && node.isRoot == false) // �Ϲݳ���϶�(�ڽ� ����)
        {
            if (node.key.Count == Node.m - 1) // �� á�ٸ�
            {
                Split(key, node);
                GoThrough(key, node);
            }
            else // �� ��á�ٸ�
            {
                GoThrough(key, node);
            }
        }
        else if (node.childNode.Count == 0 && node.isRoot == false) // ��������ϋ�(�ڽ� ����), ���⼭ ADD �ϱ�
        {
            if (node.key.Count < Node.m - 1)
            {
                //Btree.instance_Btree.StartCoroutine(Btree.instance_Btree.ChangeColor(node));
                
                node.key.Add(key);
                Arrange_key(node);
            }
            else if (node.key.Count == Node.m - 1)
            {
                Split(key, node);

            }
        }
    }
    IEnumerator ChangeColor(Node node)
    {
        Color color1;
        color1= new Color(0, 0, 170, 20);

        Color color2;
        color2 = new Color(0, 0, 170, 180);

        Debug.Log("�������");
        node.currentGameObject.GetComponent<Image>().color = color2;
        yield return new WaitForSeconds(0.3f);
        node.currentGameObject.GetComponent<Image>().color = color1;
        Debug.Log("���ೡ");
    }

    public static void AddChild(Node node_toAdd, Node current_node) // ���ο� ��带 �����԰� ���ÿ�, �װ��� �θ� ��嵵 �����ش�.
    {
        current_node.childNode.Add(node_toAdd);
        Arrange_node(current_node);

        node_toAdd.parentNode = current_node;
    } // �θ������ ��������
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
    public static void Swap_FindSavedKey(int i, Node node, Node originalnode)
    {
        if (node.childNode[0].childNode.Count == 0)
        {
            save = originalnode.childNode[i + 1].key[0];
        }
        else
        {
            Swap_FindSavedKey(i, node.childNode[0], originalnode);
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

                // 0722 �� �߰��ϱ�! �׸��� �����ϱ�!
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
                AddChild(node.parentNode.childNode[1].childNode[i], node); // �θ���� �˾Ƽ� �����ִ� �޼ҵ�
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

            for (int i = 0; i < node.parentNode.childNode[2].childNode.Count; i++) //0723 ����
            {
                AddChild(node.parentNode.childNode[count - 1].childNode[i], node);
                //node.childNode.Add(node.parentNode.childNode[count - 1].childNode[i]); 0610 ����    
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
        if (fullBrotherNode.key[0].key_value > node.key[0].key_value) // ù��° Ű�� �θ������� ����Ҷ�
        {
            fullBrotherNode.parentNode.key.Add(fullBrotherNode.key[0]);
            Arrange_key(fullBrotherNode.parentNode);
            fullBrotherNode.key.RemoveAt(0);

            node.key.Add(node.parentNode.key[count]);
            Arrange_key(node);

            if (fullBrotherNode.childNode.Count != 0)
            {
                AddChild(fullBrotherNode.childNode[0], node);
                //node.childNode.Add(fullBrotherNode.childNode[0]); // 0610 �θ��� ���� ����

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
    public static void Delete(Key key, Node node) // �����Լ�
    {
        if (node.isRoot == true) // ��Ʈ�����
        {
            if (ContainsKey(key, node) == true) // ���ȿ� key ���� �ִٸ�
            {
                if (key_count == 1) // ������ �ȵȻ���!
                {
                    if (node.childNode.Count == 0) // ���������
                    {
                        Remove(key, node);
                    }
                    else // ������尡 �ƴ϶��
                    {
                        Swap(key, node);
                    }
                }
                else // ������ �Ȼ���!
                {
                    //key_count--; // (����)
                    FindNext(key, node);
                }
            }
            else
            {
                FindNext(key, node);
            }
        }
        else // ��Ʈ��尡 �ƴ϶��
        {
            if (node.key.Count < (int)((Node.m) / 2)) // ���� ��尡 ǳ������ �ʴٸ�
            {
                if (IsBrotherNodeFull(key, node) == true) // ������尡 ǳ���ϴٸ�
                {
                    Borrow(key, node);
                    if (ContainsKey(key, node) == true) // ���ȿ� key ���� �ִٸ�
                    {
                        if (key_count == 1) // ������ �ȵȻ���
                        {
                            if (node.childNode.Count == 0) // ���������
                            {
                                Remove(key, node);
                            }
                            else // ������尡 �ƴ϶��
                            {
                                Swap(key, node); // �ٽ� �����ϴ� �޼ҵ� �����!
                            }
                        }
                        else // ������ �Ȼ���
                        {

                            FindNext(key, node);
                        }
                    }
                    else // ���ȿ� key ���� ���ٸ�
                    {
                        FindNext(key, node);
                    }
                }
                else if (IsBrotherNodeFull(key, node) == false) // ������嵵 �����ϴٸ�
                {
                    Combine(key, node);
                    if (ContainsKey(key, node) == true) // ���ȿ� key ���� �ִٸ�
                    {
                        if (node.childNode.Count == 0) // ���������
                        {
                            Remove(key, node);
                        }
                        else // ������尡 �ƴ϶��
                        {
                            Swap(key, node); // �ٽ� �����ϴ� �޼ҵ� �����!
                        }
                    }
                    else // ���ȿ� key ���� ���ٸ�
                    {
                        FindNext(key, node);
                    }
                }
            }
            else // ���� ��尡 ǳ���ϴٸ�
            {
                if (key_count == 1)
                {
                    if (ContainsKey(key, node) == true) // ���ȿ� key ���� �ִٸ�
                    {
                        if (node.childNode.Count == 0) // ���������
                        {
                            Remove(key, node);
                        }
                        else // ������尡 �ƴ϶��
                        {
                            Swap(key, node); // �ٽ� �����ϴ� �޼ҵ� �����!
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

    
}
