using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour
{


    public GameObject miniMap;

    // Start is called before the first frame update
    void Start()
    {
        miniMap.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.N)) {
            Debug.Log("input get");
            miniMap.SetActive(!CheckStatus());
        }
    }

    private bool CheckStatus() {

        if (miniMap.activeInHierarchy) {
            return true;
        }
        return false;
    }
}
