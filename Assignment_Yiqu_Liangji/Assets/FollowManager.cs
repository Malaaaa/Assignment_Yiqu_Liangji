using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowManager : MonoBehaviour
{
    public bool followable;         
    public bool stop;               
    public bool lookat;             
    public float speed;            
    void Start()
    {
        followable = true;
        lookat = true;
        stop = false;
    }

}