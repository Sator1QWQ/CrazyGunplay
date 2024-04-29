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
    private List<Weapon> loadingList = new List<Weapon>(); //实体加载中的武器列表
    private PlayerEntity mPlayerEntity;
    public Weapon CurrentWeapon { get; private set; }
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
        CurrentSlot = 0;
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

        Module.Entity.HideEntity(CurrentWeapon.EntityId);
        Weapon weapon = mWeaponDic[weaponSlot];
        Change(weaponSlot, weapon);

        if(!Module.Entity.IsLoadingEntity(CurrentWeapon.EntityId))
        {
            Debug.Log("没在loading show一次");
            Module.Entity.ShowEntity(CurrentWeapon.EntityId, typeof(WeaponEntity), CurrentWeapon.Config.path, "Weapon", weapon);
        }
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
    /// 添加或者切换武器槽
    /// </summary>
    /// <param name="weaponSlot"></param>
    /// <param name="weaponId"></param>
    public void AddOrChangeSlot(int weaponSlot, int weaponId)
    {
        if(mWeaponDic.ContainsKey(weaponSlot))
        {
            ChangeSlot(weaponSlot);
            return;
        }

        Weapon weapon = Module.Weapon.NewWeapon(mPlayerEntity, weaponId);
        mWeaponDic.Add(weaponSlot, weapon);   //武器对象是当前帧生成的，但是武器实体不一定
        if (CurrentWeapon != null)
        {
            Module.Entity.HideEntity(CurrentWeapon.EntityId);
        }
        Change(weaponSlot, weapon);
    }

    private void Change(int slot, Weapon weapon)
    {
        CurrentWeapon = weapon;
        CurrentSlot = slot;
    }

    /// <summary>
    /// 该武器槽是否有武器
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
    public bool HasWeapon(int slot)
    {
        return mWeaponDic.ContainsKey(slot);
    }

    public Weapon GetWeapon(int slot)
    {
        return mWeaponDic[slot];
    }

    public void RemoveWeapon(int slot)
    {
        Weapon weapon = mWeaponDic[slot];
        Module.Entity.HideEntity(weapon.Entity.Entity);
        mWeaponDic.Remove(slot);

        if(CurrentSlot == slot)
        {
            ChangeSlot(0);  //移除武器后默认为第0把
        }
        
    }

    public void Clear()
    {

    }
}
