using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 每个玩家都会有WeaponManager
/// </summary>
public class WeaponManager
{
    //key:武器槽索引
    private Dictionary<int, Weapon> mWeaponDic = new Dictionary<int, Weapon>();
    private PlayerEntity mPlayerEntity;

    private Weapon mLastWeapon;
    public Weapon CurrentWeapon { get; private set; }

    private int mLastSlot;
    public int CurrentSlot { get; private set; }    //当前槽

    public WeaponManager(PlayerEntity playerEntity)
    {
        mPlayerEntity = playerEntity;
    }

    public void OnUpdate()
    {
        if(CurrentWeapon != null)
        {
            CurrentWeapon.OnUpdate();
        }
    }

    public void InitWeapon(int weaponId)
    {
        Weapon weapon = Module.Weapon.NewWeapon(mPlayerEntity, weaponId);
        mWeaponDic.Add(0, weapon);   //武器对象是当前帧生成的，但是武器实体不一定
        CurrentWeapon = weapon;
        mLastWeapon = CurrentWeapon;
        CurrentSlot = 0;
        mLastSlot = CurrentSlot;
    }

    /// <summary>
    /// 切换武器槽
    /// </summary>
    /// <param name="weaponSlot"></param>
    public void ChangeSlot(int weaponSlot)
    {
        if(!mWeaponDic.ContainsKey(weaponSlot))
        {
            Debug.Log($"当前武器槽[{weaponSlot}]中没有武器");
            return;
        }

        Module.Entity.HideEntity(mLastWeapon.Entity.Entity.Id);
        Weapon weapon = mWeaponDic[weaponSlot];
        Change(weaponSlot, weapon);
        Module.Entity.ShowEntity(CurrentWeapon.Entity.Entity.Id, typeof(WeaponEntity), CurrentWeapon.Entity.Entity.EntityAssetName, CurrentWeapon.Entity.Entity.EntityGroup.Name, weapon);
    }

    /// <summary>
    /// 切换当前槽内的武器对象
    /// </summary>
    /// <param name="weaponSlot"></param>
    /// <param name="weaponId"></param>
    public void ChangeWeapon(int weaponSlot, int weaponId)
    {
        
    }

    /// <summary>
    /// 添加并且切换武器槽
    /// </summary>
    /// <param name="weaponSlot"></param>
    /// <param name="weaponId"></param>
    public void AddAndChangeSlot(int weaponSlot, int weaponId)
    {
        if(mWeaponDic.ContainsKey(weaponSlot))
        {
            ChangeSlot(weaponSlot);
            return;
        }

        Weapon weapon = Module.Weapon.NewWeapon(mPlayerEntity, weaponId);
        mWeaponDic.Add(weaponSlot, weapon);   //武器对象是当前帧生成的，但是武器实体不一定
        ChangeSlot(weaponSlot);
    }

    private void Change(int slot, Weapon weapon)
    {
        mLastWeapon = CurrentWeapon;
        CurrentWeapon = weapon;
        mLastSlot = CurrentSlot;
        CurrentSlot = slot;
    }

    public void Clear()
    {

    }
}
