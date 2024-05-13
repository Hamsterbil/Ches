using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, speed, 0);
    }
}
