using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 目标选择器
/// </summary>
public class TargetSelector : IReference
{
    protected float maxValue;
    protected float minValue;

    public PlayerEntity MaxPlayer { get; private set; }
    public PlayerEntity MinPlayer { get; private set; }

    private bool isInit = false;

    protected TargetSelectMode SelectMode { get; private set; }
    protected CompareType CompareType { get; private set; }

    public void Init(TargetSelectMode mode, CompareType compareType)
    {
        SelectMode = mode;
        CompareType = compareType;
    }

    /// <summary>
    /// 根据目标获取数据
    /// </summary>
    /// <param name="mode"></param>
    /// <returns></returns>
    public float GetValueBySelectMode(PlayerEntity player, PlayerEntity other)
    {
        if(SelectMode == TargetSelectMode.Distance)
        {
            return Vector3.Distance(player.Entity.transform.position, other.Entity.transform.position);
        }
        else if(SelectMode == TargetSelectMode.HP)
        {
            return other.Data.beatBackValue;
        }

        return 0;
    }

    public bool Compare(PlayerEntity curPlayer, float curValue, float targetValue)
    {
        UpdateMinMax(curPlayer, curValue);
        if(CompareType == CompareType.Greater && curValue > targetValue)
        {
            return true;
        }
        if(CompareType == CompareType.Less && curValue < targetValue)
        {
            return true;
        }
        if(CompareType == CompareType.Equal && curValue == targetValue)
        {
            return true;
        }

        return false;
    }

    private void UpdateMinMax(PlayerEntity curPlayer, float curValue)
    {
        if (!isInit)
        {
            maxValue = curValue;
            minValue = curValue;
            isInit = true;
        }

        if (CompareType == CompareType.Max && curValue >= maxValue)
        {
            maxValue = curValue;
            MaxPlayer = curPlayer;
        }
        else if (CompareType == CompareType.Min && curValue <= minValue)
        {
            minValue = curValue;
            MinPlayer = curPlayer;
        }
    }

    public void Clear()
    {
        maxValue = 0;
        minValue = 0;
        isInit = false;
        CompareType = default;
    }
}
