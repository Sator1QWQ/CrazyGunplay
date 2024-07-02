using GameFramework.Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 音效模块
/// </summary>
public class AudioComponent : GameFrameworkComponent
{
    //缓存
    //虽然GF框架中Reource加载完有做缓存，但是每次调用加载时会执行很多操作，新增Task 所以每次调用LoadAsset也会有性能消耗
    private Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();
    private AudioSource backGroundSource;   //bgm

    private void Start()
    {
        backGroundSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioSource source, string assetName)
    {
        if(audioDic.ContainsKey(assetName))
        {
            //还没加载完
            if(audioDic[assetName] == null)
            {
                return;
            }
            source.clip = audioDic[assetName];
            source.Play();
            return;
        }

        audioDic.Add(assetName, null);
        LoadAssetCallbacks callBack = new LoadAssetCallbacks(OnLoadAsset);
        Module.Resource.LoadAsset(assetName, callBack, source);
    }

    private void OnLoadAsset(string assetName, object asset, float duration, object userData)
    {
        AudioClip clip = asset as AudioClip;
        audioDic[assetName] = clip;
        AudioSource source = userData as AudioSource;
        source.clip = clip;
        source.Play();
    }
}
