using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyScript : MonoBehaviour
{

    public ThirdPersonControllerScript thirdPersonController;

    private float amount = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider collider) {

        if (collider.tag == "EnemyWappon") {
            thirdPersonController.ChangeHealth(amount);
        }
    }
}
