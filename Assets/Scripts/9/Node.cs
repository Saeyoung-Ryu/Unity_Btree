using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node
{
    public GameObject currentGameObject;

    public static int level = 1;
    public static int m; //4��, 3���� �ڽ��� �ִ�� ���´�. (¦�� ������ �ֱ�!!)

    public List<Key> key = new List<Key>(); // ������ ���� ū ������� �����ϱ� (Priority Queue ���), �ִ� ���� �ִ� Ű ���� m - 1
    public List<Node> childNode = new List<Node>();

    public bool isRoot;

    public Node parentNode;
    public Node(Node node)
    {
    }


}
