require "Configs.Config.Character"

--角色C层
CCharacterController = {}
function CCharacterController.Init()
    for k, v in pairs(Character) do
        Debug.Log("k==" .. tostring(k) .. ", v==" .. tostring(v))
    end
end