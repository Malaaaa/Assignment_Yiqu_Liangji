using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyScript : MonoBehaviour
{

    public ThirdPersonControllerScript thirdPersonController;

    private float amount = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void onTriggerEnter(Collider collider) {
        Debug.Log(collider.name);

        if (collider.name == "Goblin_rouge") {
            Debug.Log("Attacked");
            thirdPersonController.ChangeHealth(amount);
        }
    }
}
