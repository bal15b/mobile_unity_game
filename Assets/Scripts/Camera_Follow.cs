using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform Knight_man;

    void FixedUpdate()
    {
        transform.position = new Vector3(Knight_man.position.x, Knight_man.position.y + 2, transform.position.z);
    }
}
