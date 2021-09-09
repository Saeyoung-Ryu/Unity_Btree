using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectPool_manager : MonoBehaviour
{
    public static int numberofData;

    public static ObjectPool_manager objectPool_Manager_instance; 
    public static Queue<GameObject> objectpool = new Queue<GameObject>();
    public static Queue<GameObject> buttonpool = new Queue<GameObject>();

    [SerializeField]
    private GameObject Node_prefab;
    [SerializeField]
    private GameObject Button_prefab;

    public GameObject objectpool_manager_;

    public GameObject ViewPort_Content;

    void Awake()
    {
        if(objectPool_Manager_instance == null)
        {
            objectPool_Manager_instance = this;
        }

        numberofData = 1500;
    }
    void Start()
    {
        Instantiate_(numberofData * 2);
        Instantiate_Button(numberofData * 2);
    }

    void Update()
    {
        
    }

    void Instantiate_(int objectpool_count)
    {
        for (int i = 0; i < objectpool_count; i++)
        {
            GameObject g1 = Instantiate(Node_prefab, transform);
            g1.SetActive(false);
            g1.transform.SetParent(objectpool_manager_.transform, false);
            objectpool.Enqueue(g1);
        }
    }

    void Instantiate_Button(int objectpool_count)
    {
        for (int i = 0; i < objectpool_count; i++)
        {
            GameObject g1 = Instantiate(Button_prefab, transform);
            g1.SetActive(false);
            g1.transform.SetParent(objectpool_manager_.transform, false);
            buttonpool.Enqueue(g1);
        }
    }
    public static GameObject Borrow_withoutTransformSet() // 이 함수를 실행하면, SetParent 와 SetActice(true) 해주기!
    {
        GameObject g1 = objectpool.Dequeue();
        

        return g1;
    }
    public static GameObject Borrow_ObjectPool(GameObject wheretoPut_transform)
    {
        GameObject g1 = objectpool.Dequeue();
        g1.SetActive(true);
        g1.transform.SetParent(wheretoPut_transform.transform, false);

        return g1;
    }

    public static void Return_ObjectPool(GameObject gameObject_toRetrun)
    {
        gameObject_toRetrun.GetComponent<Node_GameObject>().currentNode = null;
        gameObject_toRetrun.GetComponent<Node_GameObject>().childGameObject.Clear(); //초기화시키는 과정
        gameObject_toRetrun.SetActive(false);
        gameObject_toRetrun.transform.SetParent(objectPool_Manager_instance.objectpool_manager_.transform, false);
        objectpool.Enqueue(gameObject_toRetrun);
    }

    public static List<int> btn_list = new List<int>(); // 0802

    public static GameObject Borrow_Button()
    {
        GameObject g1 = buttonpool.Dequeue();
        //btn_list.Add(g1); // 0802
        g1.transform.SetParent(objectPool_Manager_instance.ViewPort_Content.transform, false);
        g1.SetActive(true);

        return g1;
    }
    
    public static void Return_Button(GameObject gameObject_toReturn)
    {
        gameObject_toReturn.SetActive(false);
        gameObject_toReturn.transform.SetParent(objectPool_Manager_instance.objectpool_manager_.transform, false);
        buttonpool.Enqueue(gameObject_toReturn);
    }
}
