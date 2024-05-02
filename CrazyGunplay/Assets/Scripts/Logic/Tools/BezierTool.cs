using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 贝塞尔曲线
/// </summary>
public class BezierTool
{
    /// <summary>
    /// 贝塞尔曲线的随机参数
    /// </summary>
    public class RandomBezierData
    {
        public int routeCount;  //路径点数量
        public int controlPointCount;   //控制点数量  控制贝塞尔曲线的 包括起点和终点 不能少于2个
        public Vector3 normal;  //法向量 用于确定控制点的方位
        public bool isControlSameWithNormal;    //控制点是否跟法向量同向

        /// <summary>
        /// 波动幅度 大于0
        /// </summary>
        public float range;

        public RandomBezierData()
        {
            routeCount = 20;
            controlPointCount = 2;
            range = 0;
        }
    }

    /// <summary>
    /// 根据一组点求出曲线
    /// </summary>
    /// <param name="pointList">包括起点和终点的所有控制点</param>
    /// <param name="t">0-1之间</param>
    /// <returns></returns>
    public static Vector3 GetPoint(List<Vector3> pointList, float t)
    {
        List<Vector3> newList = new List<Vector3>();
        if (pointList.Count == 2)
        {
            return Lerp(pointList[0], pointList[1], t);
        }

        for (int i = 0; i < pointList.Count - 1; i++)
        {
            newList.Add(Lerp(pointList[i], pointList[i + 1], t));
        }

        return GetPoint(newList, t);
    }

    /// <summary>
    /// 插值公式
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Vector3 Lerp(Vector3 p1, Vector3 p2, float t)
    {
        Vector3 point = p1 * (1 - t) + p2 * t;
        return point;
    }

    /// <summary>
    /// 生成一条随机的曲线
    /// </summary>
    /// <param name="startPoint"></param>
    /// <param name="endPoint"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static List<Vector3> GetRandomLine(Vector3 startPoint, Vector3 endPoint, RandomBezierData data)
    {
        float step = 1.0f/ data.routeCount;
        List<Vector3> routeList = new List<Vector3>();  //返回的路径列表
        List<Vector3> controlList = new List<Vector3>();    //控制点列表
        controlList.Add(startPoint);

        //排除起点和终点
        for (int i = 1; i < data.controlPointCount - 1; i++)
        {
            Vector3 point = Lerp(startPoint, endPoint, step * i);   //控制点平均分配
            float randomRange = Random.Range(0, data.range);

            int dire = 1;
            if (!data.isControlSameWithNormal)
            {
                dire = Random.Range(-1.0f, 1.0f) > 0 ? 1 : -1;
            }
            Vector3 randomPoint = data.normal * dire * randomRange + point;
            
            controlList.Add(randomPoint);
        }
        controlList.Add(endPoint);
        for (int i = 0; i < data.routeCount; i++)
        {
            Vector3 point = GetPoint(controlList, i * step);
            routeList.Add(point);
        }

        return routeList;
    }
}
