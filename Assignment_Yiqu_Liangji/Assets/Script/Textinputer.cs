using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Textinputer : MonoBehaviour
{
    public static Textinputer Gettask;
    public GameObject Textbox;
    public Text thetext;
    public TextAsset textFile;
    public string[] textLines;
    public int currentline = 0;
    public int endline;
    public GameObject player;
    public GameObject NPC;
    public float SpeakDistance;
    public bool task=false;



    private void Awake()
    {
        Gettask = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(textFile!=null)
        textLines=(textFile. text. Split('\n'));
        Textbox.SetActive(false);
    }
    void Update()
    {

        SpeakDistance = Vector3.Distance(NPC.transform.position, player.transform.position);     
        if(SpeakDistance < 2f&&currentline<endline&&Input.GetMouseButtonDown(0)) {
            Textbox.SetActive(true);
            thetext.text = textLines[currentline];
            if(Input.GetMouseButtonDown(0))
            {
                currentline +=1;
            }
            if(currentline>=endline)
            {
                task=true;
                Textbox.SetActive(false);
            }           

        }
    }
    public bool gettask(){
        return task;
    }
}
