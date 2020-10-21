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
        dis += Input.GetAxis("Mouse ScrollWheel") * 5;
        if (dis < 10 || dis > 40)
        {
            return;
        }
        offset = offset.normalized * dis;
    }
    private void Rotate()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 pos = transform.position;
            Vector3 rot = transform.eulerAngles;

            transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * 20);
            transform.RotateAround(target.position, Vector3.left, Input.GetAxis("Mouse Y") * 20);
            float x = transform.eulerAngles.x;
            float y = transform.eulerAngles.y;

            if (x < 20 || x > 45 || y < 0 || y > 40)
            {
                transform.position = pos;
                transform.eulerAngles = rot;
            }
            offset = transform.position - target.position;
        }
    }
}