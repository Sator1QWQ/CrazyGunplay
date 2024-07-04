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

    public void SetSprite(Image img, string fullAtlasPath, string spriteName, float height)
    {
        object[] obj = new object[] { img, spriteName, height };

        //需要预加载而非懒加载
        Module.Resource.LoadAsset(fullAtlasPath, callBack, obj);
    }

    private void OnLoadAsset(string assetName, object asset, float duration, object userData)
    {
        SpriteAtlas atlas = asset as SpriteAtlas;
        object[] obs = userData as object[];
        Sprite sp = atlas.GetSprite(obs[1].ToString());
        Image img = obs[0] as Image;
        img.sprite = sp;

        if (sp == null)
        {
            Debug.LogError("没有图片！assetName==" + assetName + ", sprite==" + obs[1].ToString());
            return;
        }

        //设置img长宽比与sprite一致
        float imgHeight = (float)obs[2];
        img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, imgHeight);
        float adapter = sp.rect.width / sp.rect.height;
        float imgWidth = imgHeight * adapter;
        img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, imgWidth);
    }
}
