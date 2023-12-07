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
	public LuaItem template;
	private int curSelect = -1;
	private List<LuaItem> itemList = new List<LuaItem>();

	/// <summary>
	/// 设置数量
	/// </summary>
	public void SetCount(int count)
    {
		for(int i = 0; i < count; i++)
        {
			GameObject go = Instantiate(template.gameObject);
			go.transform.SetParent(transform, false);
			go.SetActive(true);
			go.name = i.ToString();
			LuaItem item = go.GetComponent<LuaItem>();
			item.SetIndex(i);
			itemList.Add(item);
		}
    }

	/// <summary>
	/// 单选模式
	/// </summary>
	/// <param name="index"></param>
	public void Select(int index)
    {
		if(itemList.Count <= index)
        {
			Debug.LogError("item数量和索引不匹配！count==" + itemList.Count + ", index==" + index);
			return;
        }

		//之前的取消选择
		if(curSelect != -1 && curSelect != index)
        {
			LuaItem lastItem = itemList[curSelect];
			if (lastItem.isSelect)
            {
				lastItem.SelectChange();
			}
        }

		curSelect = index;
		LuaItem curItem = itemList[curSelect];
		curItem.SelectChange();
	}

	/// <summary>
	/// 当前选中的item
	/// </summary>
	public int GetCurSelect()
    {
		for(int i = 0; i < itemList.Count; i++)
        {
			if(itemList[i].isSelect)
            {
				return i;
            }
		}

		return -1;
	}
}
