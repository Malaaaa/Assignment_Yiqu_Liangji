using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapcam : MonoBehaviour
{
    public Transform Player;

    // Update is called once per frame
    void Update()
    {
        float posX = Player.transform.position.x;    
        float posZ = Player.transform.position.z;    
        float OffsetY = Player.transform.position.y + 20;  
        Vector3 pos = new Vector3(posX, OffsetY, posZ);
        transform.position = pos;
    }
}
