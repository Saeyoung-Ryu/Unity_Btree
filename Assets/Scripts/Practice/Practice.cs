using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Practice : MonoBehaviour
{
    public GameObject white;
    public GameObject black;

    public GameObject house;

    Queue<GameObject> gameObjects_queue = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        black.transform.SetParent(house.transform, false);
        white.transform.SetParent(house.transform, false);

        gameObjects_queue.Enqueue(white);
        gameObjects_queue.Enqueue(black);
    }
}
