using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Textinputer : MonoBehaviour
{
    public GameObject Textbox;
    public Text thetext;
    public TextAsset textFile;
    public string[] textLines;
    public int currentline;
    public int endline;
    public GameObject player;
    public GameObject NPC;
    public float SpeakDistance;


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
        Debug.Log(SpeakDistance);   
        if(SpeakDistance < 2f) {
            Debug.Log("come in");
            Textbox.SetActive(true);
            thetext.text = textLines[currentline];
            if(Input.GetMouseButtonDown(0))
            {
                currentline +=1;
            }
            if(currentline>=endline)
            {
                Textbox.SetActive(false);
            }
        }
    }
}
