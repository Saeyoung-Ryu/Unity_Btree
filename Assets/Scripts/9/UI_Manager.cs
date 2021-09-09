using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class UI_Manager : MonoBehaviour
{
    public static UI_Manager uimanager_instance;

    [SerializeField]
    private InputField SetOrder_Inputfield;
    [SerializeField]
    private InputField Insert_inputField;
    [SerializeField]
    private InputField Delete_inputField;
    [SerializeField]
    private GameObject stop_btn;
    [SerializeField]
    private GameObject SetOrder;
    [SerializeField]
    private GameObject Insert;
    [SerializeField]
    private GameObject Delete;
    [SerializeField]
    private Slider insertSpeedSlider;
    [SerializeField]
    private Text numberofData;
    [SerializeField]
    private GameObject scroll_View;
    [SerializeField]
    private GameObject numberofData_gameobject;
    [SerializeField]
    private Scrollbar scrollbar_speed;
    [SerializeField]
    private GameObject panel_fornodes;

    [SerializeField] // 0802
    private ScrollRect scrollRect;
    [SerializeField]
    private RectTransform contentPanel;
    [SerializeField]
    private InputField InputField_search;

    public static int numberofData_count;

    public float data_insert_speed = 1.1f;
    public float data_insertspeed;
    public static int searched_number;

    public GameObject rootNode;

    Queue<int> RandQueue = new Queue<int>();

    public static Queue<int> InsertedData = new Queue<int>();

    //public float insertSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        if(uimanager_instance == null)
        {
            uimanager_instance = this;
        }

     
        
        RandQueue = RandNum(ObjectPool_manager.numberofData - 1); // 위에서 먼저 데이터 생성해주기!
    }

    // Update is called once per frame
    void Update()
    {
        rootNode.GetComponent<Node_GameObject>().currentNode = Btree.rootNode;
        rootNode.GetComponent<Node_GameObject>().currentNode.currentGameObject = rootNode;
        //SetSpeed();
        MatchNumberofData();
    }

    //void SetSpeed()
    //{
    //    insertSpeed = insertSpeedSlider.value;
    //}

    public void Search_btn() // 0802
    {
        searched_number = int.Parse(InputField_search.text);
        //searched_number = a;
        for (int i = 0; i < ObjectPool_manager.btn_list.Count; i++)
        {
            if(ObjectPool_manager.btn_list[i] == searched_number)
            {

                contentPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, i * 30, 0);
                //RectTransform 내부안에서의 위치를 바꿀때는 anchoredPosition 사용하기!

                InputField_search.text = "";
            }
        }
    }
    

    public void MatchNumberofData()
    {
        numberofData.text = numberofData_count.ToString();
    }
    public void SetOrder_Btn()
    {
        Btree.SetOrder(int.Parse(SetOrder_Inputfield.text));
        
        SetOrder.SetActive(false);
        Insert.SetActive(true);
        //Delete.SetActive(true);
    }

    static Queue<int> RandNum(int number)
    {
        System.Random rand = new System.Random();
        Queue<int> intQueue = new Queue<int>();

        while (intQueue.Count != number)
        {
            int a = rand.Next(number);

            if (!intQueue.Contains(a))
            {
                intQueue.Enqueue(a);
            }
        }
        return intQueue;
    }

    

    IEnumerator Insert_Num(int num)
    {
        for (int i = 1; i < num - 1; i++)
        {
            int a = RandQueue.Dequeue();
            InsertedData.Enqueue(a);

            GameObject g1 = ObjectPool_manager.Borrow_Button();
            Delete_btn.number_list.Add(g1);
            Btree.Insert(new Key(a), Btree.rootNode);
            //yield return new WaitForSeconds(0.3f);
            yield return new WaitForSeconds(data_insert_speed - scrollbar_speed.value); // 0727 속도조절기능 수정
            
        }
        
    }

    public void Insert_Btn()
    {
        panel_fornodes.SetActive(true);
        rootNode.SetActive(true);
        stop_btn.SetActive(true);
        scroll_View.SetActive(true);
        numberofData_gameobject.SetActive(true);
        //scrollbar_speed.GetComponent<GameObject>().SetActive(true); 아래꺼랑 이거랑 똑같음!
        scrollbar_speed.gameObject.SetActive(true);
        StartCoroutine(Insert_Num(ObjectPool_manager.numberofData / 2));

        //Btree.Insert(new Key(int.Parse(Insert_inputField.text)), Btree.rootNode);
        //Insert_inputField.text = "";

        //for (int i = 1; i <= 1000; i++)
        //{
        //    Btree.Insert(new Key(i), Btree.rootNode);
        //}

    }


    public void Stop_Btn()
    {
        StopAllCoroutines();
    }

    public void Delete_Btn() // insertedData 리스트에서 임의의 수 가지고와서 delete 하기!
    {
        int a = int.Parse(Delete_inputField.text);
        Btree.Delete(new Key(a), Btree.rootNode);

        for (int i = 0; i < Delete_btn.number_list.Count; i++)
        {
            if(Delete_btn.number_list[i].GetComponent<Delete_btn>().number == a)
            {
                UI_Manager.numberofData_count--;
                ObjectPool_manager.Return_Button(Delete_btn.number_list[i]);
                Delete_btn.number_list.RemoveAt(i);
                break;                
            }
        }

        Delete_inputField.text = "";
    }
}
