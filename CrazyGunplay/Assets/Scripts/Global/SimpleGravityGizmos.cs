using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGravityGizmos : MonoBehaviour
{
    private SimpleGravity g;

    private void Awake()
    {
        g = GetComponent<SimpleGravity>();
    }

    private void OnDrawGizmos()
    {
        Vector3 leftStart = g.gameObject.transform.position + g.leftRaycastOffset;
        Vector3 rightStart = g.gameObject.transform.position + g.rightRaycastOffset;
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(leftStart, Vector3.down * GlobalDefine.FLOOR_RAY_DISTANCE);
        Gizmos.DrawRay(rightStart, Vector3.down * GlobalDefine.FLOOR_RAY_DISTANCE);
    }
}
