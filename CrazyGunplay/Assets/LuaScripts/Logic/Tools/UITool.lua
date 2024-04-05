UITool = {}
local _P = {}
UITool.openingList = {} --开启时的界面列表
--Module.Event:Subscribe(GFRuntime.OpenUIFormSuccessEventArgs.EventId, _P.OnOpenUIForm)
--Module.Event:Subscribe(98765, nil)

function UITool.ShowTip()

end

function UITool.ShowMessageWindow()

end

function UITool.OpenUIForm(panelName, isDoTween)
    UITool.openingList[panelName] = {isDoTween = isDoTween}
    local path = GlobalDefine.UIPath .. panelName .. ".prefab"
    UI:OpenUIForm(path, "NormalGroup")
end

--加载ui成功后调用
function _P.OnOpenUIForm(sender, e)
    local name = e.UIForm.UIFormAssetName
    local openData = UITool.openingList[name]
    if openData then
        print("生成界面成功")
        if openData.isDoTween then
            e.UIForm.Logic.transform:DOScale(1.5, 1)    
        end
        UITool.openingList[name] = nil
    end
end