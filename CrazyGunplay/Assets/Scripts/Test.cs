using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public bool isWalk;
    public bool isRun;

    private void Update()
    {
        transform.position += Vector3.one * 0.2f;
        //GetComponent<Rigidbody>().velocity = new Vector3(4, 1, 4);
    }
}
