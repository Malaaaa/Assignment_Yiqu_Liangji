using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour
{

    public float timeAlive = 0f;
    public float limitTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > limitTime) {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider collider) {

        if (collider.tag == "Enemy" || collider.tag == "EnemyWappon") {
            gameObject.SetActive(false);
        }
    }
}
