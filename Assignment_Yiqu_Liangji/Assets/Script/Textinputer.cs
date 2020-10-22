using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            textLines=(TaskFile.text.Split('\n'));
        } 
        Textbox.SetActive(false);
        notask.SetActive(true);
        done.SetActive(false);
    }
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        NPC = GameObject.FindGameObjectWithTag("NPC");
        SpeakDistance = Vector3.Distance(NPC.transform.position, player.transform.position); 
        reward = Taskfinished.Reward;
        Debug.Log(reward);
        if(reward && SpeakDistance > 4f){
            textLines = (RewardFile.text.Split('\n'));
            currentline = 0;
            endline = 3;
            notask.SetActive(false);
            done.SetActive(true);    
        }
        if (reward && Input.GetMouseButtonDown(0) && SpeakDistance < 2f && currentline < endline) {

            Textbox.SetActive(true);
            thetext.text = textLines[currentline];
            if(Input.GetMouseButtonDown(0))
            {
                Debug.Log("clicked conversation");

                currentline +=1;
            }
            if(currentline >= endline)
            {
                Textbox.SetActive(false);
            }         
        }
        // GameObject currentObject = EventSystem.current.currentSelectedGameObject;
        if(SpeakDistance < 4f && currentline < endline && Input.GetMouseButtonDown(0) && !task) {
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
