using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node
{
    public GameObject currentGameObject;

    public static int level = 1;
    public static int m; //4차, 3개의 자식을 최대로 갖는다. (짝수 차수만 넣기!!)

    public List<Key> key = new List<Key>(); // 작은것 에서 큰 순서대로 나열하기 (Priority Queue 사용), 최대 들어갈수 있는 키 수는 m - 1
    public List<Node> childNode = new List<Node>();

    public bool isRoot;

    public Node parentNode;
    public Node(Node node)
    {
    }


}
