using GameFramework.Event;
using GameFramework.Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public class PreloadData
{
    public string path;
    public PreloadType type;    //预加载类型
    public object[] values;
    public bool isLoaded;
}

/// <summary>
/// 预加载组件
/// </summary>
public class PreloadComponent : GameFrameworkComponent
{
    private List<PreloadData> dataList = new List<PreloadData>();
    private LoadAssetCallbacks assetCallBack;

    private void Start()
    {
        assetCallBack = new LoadAssetCallbacks(OnLoadAsset);
    }

    /// <summary>
    /// 添加预加载资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="data"></param>
    public void AddPreload(PreloadData data)
    {
        dataList.Add(data);
    }

    private void Update()
    {
        bool isAllLoaded = true;
        foreach(PreloadData data in dataList)
        {
            if(!data.isLoaded)
            {
                isAllLoaded = false;
                break;
            }
        }

        //全部加载完成
        if (isAllLoaded && dataList.Count > 0)
        {
            Module.Event.FireNow(this, PreloadFinishEventArgs.Create());
            foreach(PreloadData data in dataList)
            {
                if(data.type == PreloadType.Entity)
                {
                    Module.Entity.HideEntity((int)(long)data.values[0]);
                }
            }
            dataList.Clear();
            Module.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntity);
        }
    }

    /// <summary>
    /// 开始预加载
    /// 场景加载完后调用
    /// </summary>
    public void StartPreload()
    {
        Module.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntity);

        foreach (PreloadData data in dataList)
        {
            if(data.type == PreloadType.Entity || data.type == PreloadType.PlayerEntity)
            {
                int v0 = (int)(long)data.values[0];
                System.Type v1 = (System.Type)data.values[1];
                string v2 = (string)data.values[2];
                object v3 = data.values.Length >= 4 ? data.values[3] : null;
                Module.Entity.ShowEntity(v0, v1, data.path, v2, v3);
            }
            else if(data.type == PreloadType.OtherAssets)
            {
                Module.Resource.LoadAsset(data.path, assetCallBack);
            }
        }
    }

    private void OnShowEntity(object sender, GameEventArgs e)
    {
        ShowEntitySuccessEventArgs args = e as ShowEntitySuccessEventArgs;

        foreach(PreloadData data in dataList)
        {
            object[] objs = data.values;
            int id = (int)(long)objs[0];
            if(id == args.Entity.Id)
            {
                //找到了
                data.isLoaded = true;
                break;
            }
        }
    }

    private void OnLoadAsset(string assetName, object asset, float duration, object userData)
    {
        foreach(PreloadData data in dataList)
        {
            //只需加载一次
            if(data.path.Equals(assetName))
            {
                data.isLoaded = true;
                break;
            }
        }
    }
}
