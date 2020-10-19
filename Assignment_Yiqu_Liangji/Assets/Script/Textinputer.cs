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


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if(textFile!=null)
        textLines=(textFile. text. Split('\n'));    
    }
    void Update()
    {
        thetext.text = textLines[currentline];
        if(Input.GetMouseButtonDown(0))
        {
            currentline +=1;
        }
        if(currentline>endline){
            Textbox.SetActive(false);
        }
    }
}
