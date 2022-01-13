local HeroMsgHandler = class("HeroMsgHandler")

function HeroMsgHandler:Initialize()
    self.moduleMgr = MgrCenter:GetManager(ManagerNames.Module)
    self.heroModule = self.moduleMgr:GetModule(ModuleNames.Hero)
end

function HeroMsgHandler:OnRecvHeroList(data)
    if type(data) ~= 'table' then return end
    if self.heroModule then
        self.heroModule:ResHeroList(data)
    end
end

HeroMsgHandler.MsgFuncs = 
{
    ["pb_user.ResHeroList"] = HeroMsgHandler.OnRecvHeroList,
}

return HeroMsgHandler