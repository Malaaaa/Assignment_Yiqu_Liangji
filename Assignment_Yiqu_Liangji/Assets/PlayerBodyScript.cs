using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyScript : MonoBehaviour
{

    public ThirdPersonControllerScript thirdPersonController;

    private float amount = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other) {

        Debug.Log(other.name);
        thirdPersonController.ChangeHealth(amount);
        
    }
}
