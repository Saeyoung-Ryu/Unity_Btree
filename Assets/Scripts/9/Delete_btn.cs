using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Delete_btn : MonoBehaviour
{
    public int number;

    [SerializeField]
    private Text text;

    public static List<GameObject> number_list = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        number = UI_Manager.InsertedData.Dequeue();
        ObjectPool_manager.btn_list.Add(number);
        UI_Manager.numberofData_count++;
        text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Change_BtnColor_whenSearched();
        text.text = number.ToString();
    }

    public void Change_BtnColor_whenSearched()
    {
        if(this.number == UI_Manager.searched_number)
        {
            this.GetComponent<Image>().color = new Color32(75, 196, 221, 255);
            StartCoroutine(changeColor());
        }
    }

    IEnumerator changeColor()
    {
        yield return new WaitForSeconds(1f);
        this.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        UI_Manager.searched_number = 0;
    }
    public void Delete_Button()
    {
        int a = int.Parse(this.text.text);
        Btree.Delete(new Key(a), Btree.rootNode);

        ObjectPool_manager.Return_Button(this.gameObject);
        UI_Manager.numberofData_count--;

        for (int i = 0; i < ObjectPool_manager.btn_list.Count; i++)
        {
            if(a == ObjectPool_manager.btn_list[i])
            {
                ObjectPool_manager.btn_list.RemoveAt(i);
            }
        }
    }
}
