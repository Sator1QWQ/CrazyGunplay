using GameFramework.Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

public class AtlasComponent : GameFrameworkComponent
{
    private Dictionary<string, SpriteAtlas> atlasDic = new Dictionary<string, SpriteAtlas>();
    private LoadAssetCallbacks callBack;

    private void Start()
    {
        callBack = new LoadAssetCallbacks(OnLoadAsset);
    }

    public void SetSprite(Image img, string fullAtlasPath, string spriteName)
    {
        object[] obj = new object[] { img, spriteName };

        //需要预加载而非懒加载
        Module.Resource.LoadAsset(fullAtlasPath, callBack, obj);
    }

    private void OnLoadAsset(string assetName, object asset, float duration, object userData)
    {
        SpriteAtlas atlas = asset as SpriteAtlas;
        object[] obs = userData as object[];
        Sprite sp = atlas.GetSprite(obs[1].ToString());
        (obs[0] as Image).sprite = sp;
    }
}
