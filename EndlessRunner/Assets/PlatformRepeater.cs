using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRepeater : MonoBehaviour
{
    public Transform cam;

    float length;

    // Start is called before the first frame update
    void Start()
    {
        length = GetComponent<Renderer>().bounds.size.z;
    }

    // Update is called once per frame
    void Update()
    {
        if( cam.position.z > transform.position.z + length )
        {
            transform.Translate(Vector3.forward * length * 2);
        }
    }
}
