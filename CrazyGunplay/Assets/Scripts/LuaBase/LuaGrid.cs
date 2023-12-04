using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
		
*/
public class LuaGrid : MonoBehaviour
{
	public GameObject template;

	/// <summary>
	/// 设置数量
	/// </summary>
	public void SetCount(int count)
    {
		for(int i = 0; i < count; i++)
        {
			GameObject go = Instantiate(template.gameObject);
			go.transform.SetParent(transform);
			go.SetActive(true);
        }
    }
}
