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
    void Start()
    {
        TASKBOX.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Textinputer.Gettask != null) {
            task= Textinputer.Gettask.GetTask();
            if(task)
            {
                TASK1.text = textFile.text;
            }
        }
    }
    public void ManageTaskbox(){
        if(TASKBOX.activeSelf){
            TASKBOX.SetActive(false);
        }else{
            TASKBOX.SetActive(true);
        }
    }
}
