using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float moveSpeed;

    public BoxCollider col;

    private void Update()
    {

        if(Physics.BoxCast(transform.position, col.size / 2, Vector3.down, Quaternion.identity, 2))
        {
            Debug.Log("碰撞检测到了！");
        }

        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 2))
        {
            Debug.Log("射线检测到了2！==" + hit.transform.name);
        }

    }

}
