using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Textinputer : MonoBehaviour
{
    public static Textinputer Gettask;
    public GameObject Textbox;
    public Text thetext;
    public TextAsset TaskFile;
    public TextAsset RewardFile;
    public string[] textLines;
    public int currentline = 0;
    public int endline = 3;
    public GameObject player;
    public GameObject NPC;
    public float SpeakDistance;
    public bool task=false;
    public bool reward = false;
    public ThirdPersonControllerScript Taskfinished;
    public GameObject notask;
    public GameObject done;



    private void Awake()
    {
        Gettask = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(TaskFile!=null){
            textLines=(TaskFile. text. Split('\n'));
        } 
        Textbox.SetActive(false);
        notask.SetActive(true);
        done.SetActive(false);
    }
    void Update()
    {
        reward = Taskfinished.Reward;
        if(RewardFile != null && reward){
            Debug.Log("Reward comein");
            textLines=(TaskFile. text. Split('\n'));
            currentline=0;
            endline =2;
            notask.SetActive(false);
            done.SetActive(true);    
        }
        GameObject currentObject = EventSystem.current.currentSelectedGameObject;
        SpeakDistance = Vector3.Distance(NPC.transform.position, player.transform.position);     
        if(SpeakDistance < 2f && currentline < endline && Input.GetMouseButtonDown(0) && !task) {
            Textbox.SetActive(true);
            thetext.text = textLines[currentline];
            if(Input.GetMouseButtonDown(0))
            {
                currentline +=1;
            }
            if(currentline >= endline)
            {
                task=true;
                Textbox.SetActive(false);
            }           

        }
    }
    public bool GetTask(){
        return task;
    }
}
