--选择角色时的model
MHero = Class.Create("MHero", Object)

function MHero:ctor()
    self.selectHeroDic = {}
end

--设置游戏模式
function MHero:SetMode(mode)
    self.mode = mode
end

--当前选中的英雄
function MHero:SetSelectPlayer(playerId, heroId)
    Debug.Log("选中了==" ..tostring(playerId) )
    self.selectHeroDic[playerId] = heroId
end

function MHero:ClearSelectHero()
    self.selectHeroDic = {}
end

function MHero:Dispose()
    self:ClearSelectHero()
    self.mode = nil
end

MHero.Instance = MHero:new()