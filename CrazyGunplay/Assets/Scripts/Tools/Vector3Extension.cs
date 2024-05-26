using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extension
{
    /// <summary>
    /// 忽略y轴
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static Vector3 GetVector3NotY(this Vector3 vector)
    {
        Vector3 v = new Vector3(vector.x, 0, vector.z);
        return v;
    }
}
