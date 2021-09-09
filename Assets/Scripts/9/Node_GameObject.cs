using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node_GameObject : MonoBehaviour
{

    public Node currentNode;

    public GameObject childNode_house; // 위치 지정

    public List<GameObject> childGameObject = new List<GameObject>();

    public List<Text> key_text;

    public List<GameObject> lines;

    void Start()
    {
        
    }


    void Update()
    {
        //SetChildNode();
        MatchChildNode();
        SetCurrentGameObject();
        MatchKeys();
        SetLines();
    }

    

    void SetChildNode()
    {
        
        if(currentNode.childNode.Count != childGameObject.Count)
        {
            if(currentNode.childNode.Count == childGameObject.Count + 1) // 1개 추가해야할떄
            {
                GameObject g1 = ObjectPool_manager.Borrow_ObjectPool(childNode_house);

                childGameObject.Add(g1);
            }
            else if(currentNode.childNode.Count == childGameObject.Count + 2) // 2개 추가해야할떄
            {
                GameObject g1 = ObjectPool_manager.Borrow_ObjectPool(childNode_house);

                childGameObject.Add(g1);

                GameObject g2 = ObjectPool_manager.Borrow_ObjectPool(childNode_house);

                childGameObject.Add(g2);
            }
            else if (currentNode.childNode.Count == childGameObject.Count + 3) // 3개 추가해야할떄
            {
                GameObject g1 = ObjectPool_manager.Borrow_ObjectPool(childNode_house);

                childGameObject.Add(g1);

                GameObject g2 = ObjectPool_manager.Borrow_ObjectPool(childNode_house);

                childGameObject.Add(g2);

                GameObject g3 = ObjectPool_manager.Borrow_ObjectPool(childNode_house);

                childGameObject.Add(g3);
            }
            else if (currentNode.childNode.Count == childGameObject.Count + 4) // 4개 추가해야할떄
            {
                GameObject g1 = ObjectPool_manager.Borrow_ObjectPool(childNode_house);

                childGameObject.Add(g1);

                GameObject g2 = ObjectPool_manager.Borrow_ObjectPool(childNode_house);

                childGameObject.Add(g2);

                GameObject g3 = ObjectPool_manager.Borrow_ObjectPool(childNode_house);

                childGameObject.Add(g3);

                GameObject g4 = ObjectPool_manager.Borrow_ObjectPool(childNode_house);

                childGameObject.Add(g4);
            }

            else if(currentNode.childNode.Count == childGameObject.Count - 1) // 1개 빼야할떄
            {
                ObjectPool_manager.Return_ObjectPool(childGameObject[childGameObject.Count - 1]);
                childGameObject.RemoveAt(childGameObject.Count - 1);
            }
            else if (currentNode.childNode.Count == childGameObject.Count - 2) // 2개 빼야할떄
            {
                ObjectPool_manager.Return_ObjectPool(childGameObject[childGameObject.Count - 1]);
                childGameObject.RemoveAt(childGameObject.Count - 1);

                ObjectPool_manager.Return_ObjectPool(childGameObject[childGameObject.Count - 1]);
                childGameObject.RemoveAt(childGameObject.Count - 1);
            }
            else if (currentNode.childNode.Count == childGameObject.Count - 3) // 3개 빼야할떄
            {
                ObjectPool_manager.Return_ObjectPool(childGameObject[childGameObject.Count - 1]);
                childGameObject.RemoveAt(childGameObject.Count - 1);

                ObjectPool_manager.Return_ObjectPool(childGameObject[childGameObject.Count - 1]);
                childGameObject.RemoveAt(childGameObject.Count - 1);

                ObjectPool_manager.Return_ObjectPool(childGameObject[childGameObject.Count - 1]);
                childGameObject.RemoveAt(childGameObject.Count - 1);
            }
            else if (currentNode.childNode.Count == childGameObject.Count - 4) // 4개 빼야할떄
            {
                ObjectPool_manager.Return_ObjectPool(childGameObject[childGameObject.Count - 1]);
                childGameObject.RemoveAt(childGameObject.Count - 1);

                ObjectPool_manager.Return_ObjectPool(childGameObject[childGameObject.Count - 1]);
                childGameObject.RemoveAt(childGameObject.Count - 1);

                ObjectPool_manager.Return_ObjectPool(childGameObject[childGameObject.Count - 1]);
                childGameObject.RemoveAt(childGameObject.Count - 1);

                ObjectPool_manager.Return_ObjectPool(childGameObject[childGameObject.Count - 1]);
                childGameObject.RemoveAt(childGameObject.Count - 1);
            }
        }
    }

    void MatchChildNode()
    {
        if(currentNode.childNode.Count == childGameObject.Count)
        {
            for (int i = 0; i < currentNode.childNode.Count; i++)
            {
                childGameObject[i].GetComponent<Node_GameObject>().currentNode = currentNode.childNode[i];
            }
        }
        else
        {
            SetChildNode();
        }
        
    }

    void SetCurrentGameObject()
    {
        currentNode.currentGameObject = this.gameObject;
    }

    void MatchKeys()
    {
        for (int i = 0; i < currentNode.key.Count; i++)
        {
            key_text[i].text = currentNode.key[i].key_value.ToString();
        }

        for (int i = Node.m - 2; i > currentNode.key.Count - 1; i--)
        {
            key_text[i].text = "";
        }
    }

    void SetLines()
    {
        if(currentNode.childNode.Count == 0)
        {
            lines[0].SetActive(false);
            lines[1].SetActive(false);
            lines[2].SetActive(false);
            lines[3].SetActive(false);
        }
        else if (currentNode.childNode.Count == 1)
        {
            lines[0].SetActive(true);
            lines[1].SetActive(false);
            lines[2].SetActive(false);
            lines[3].SetActive(false);
        }
        else if (currentNode.childNode.Count == 2)
        {
            lines[0].SetActive(true);
            lines[1].SetActive(true);
            lines[2].SetActive(false);
            lines[3].SetActive(false);
        }
        else if (currentNode.childNode.Count == 3)
        {
            lines[0].SetActive(true);
            lines[1].SetActive(true);
            lines[2].SetActive(true);
            lines[3].SetActive(false);
        }
        else if (currentNode.childNode.Count == 4)
        {
            lines[0].SetActive(true);
            lines[1].SetActive(true);
            lines[2].SetActive(true);
            lines[3].SetActive(true);
        }
    }
}
