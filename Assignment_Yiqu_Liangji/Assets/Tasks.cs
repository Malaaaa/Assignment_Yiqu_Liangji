using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tasks : MonoBehaviour
{
    public GameObject TASKBOX;
    public Text TASK1;
    public TextAsset textFile;
    public bool task;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        task= Textinputer.Gettask.gettask();
        if(task)
        {
            TASK1.text = textFile. text;
        }
        
    }

}
