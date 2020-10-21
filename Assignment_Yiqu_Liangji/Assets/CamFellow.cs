using UnityEngine;
using System.Collections;

public class CamFellow : MonoBehaviour
{
    public Transform target;
    Vector3 offset;
    // Use this for initialization
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
        Rotate();
        Scale();
    }
    private void Scale()
    {
        float dis = offset.magnitude;
        dis -= Input.GetAxis("Mouse ScrollWheel") * 5;
        if (dis < 10 || dis > 40)
        {
            return;
        }
        offset = offset.normalized * dis;
    }
    private void Rotate() 
    {
        var mouse_x = Input.GetAxis("Mouse X");
        var mouse_y = -Input.GetAxis("Mouse Y");
        if (Input.GetMouseButton(1))
        {
            transform.Translate(Vector3.left*(mouse_x*15f)*Time.deltaTime);
            transform.Translate(Vector3.up*(mouse_y*15f)*Time.deltaTime);
            transform.RotateAround(target.transform.position, Vector3.up, mouse_x*5);
            transform.RotateAround(target.transform.position, transform.right, mouse_y*5);
        }
    }

}