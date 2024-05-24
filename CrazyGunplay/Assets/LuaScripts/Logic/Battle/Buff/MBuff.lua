MBuff = Class.Create("MBuff", Object)

function MBuff:ctor()
    self.buffDefaultData = {
        moveSpeedScale = 1,
        getBeatScale = 1,   --受伤的倍率
        hpAdd = 0,
        fireRateScale = 1,
        attackScale = 1,  --增加伤害的倍率
    }
end

function MBuff:InitPlayerBuff(player)
    player.buffData = {
        moveSpeedScale = self.buffDefaultData.moveSpeedScale,
        getBeatScale = self.buffDefaultData.getBeatScale,
        hpAdd = self.buffDefaultData.hpAdd,
        fireRateScale = self.buffDefaultData.fireRateScale,
        attackScale = self.buffDefaultData.attackScale,
    }
    player.buffDic = {}    --存放了buffId列表
end

function MBuff:ChangeBuffData()

end

MBuff.Instance = MBuff.new()