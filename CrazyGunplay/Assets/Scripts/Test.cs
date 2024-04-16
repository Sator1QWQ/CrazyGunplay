using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject target;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            transform.rotation = Quaternion.LookRotation(target.transform.right);
            transform.right = transform.forward;
        }

    }

}
