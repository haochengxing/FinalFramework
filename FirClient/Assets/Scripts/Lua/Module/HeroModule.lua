local HeroModule = class("HeroModule")

function HeroModule:Initialize()
	self.netMgr = MgrCenter:GetManager(ManagerNames.Network)
	self.mHeroDataList = {}
	local tableMgr = MgrCenter:GetManager(ManagerNames.Table)
	local items = tableMgr.npcTable:GetItems()
	local iter = items:GetEnumerator()

	while iter:MoveNext() do
		local npcItem = iter.Current
		if not npcItem.isMainCharacter then
			local item = {
				name = npcItem.name,
				itemid = npcItem.itemid,
			}
			table.insert(self.mHeroDataList, item)
		end
	end
end

function HeroModule:GetDataListSize()
	return #self.mHeroDataList
end

function HeroModule:GetDataByIndex(index)
	return self.mHeroDataList[index]
end

function HeroModule:ReqHeroList(userid)
    local sendData = {
        userid = userid,
    }
    self.netMgr:SendMessage("pb_user.ReqHeroList", sendData)
end

function HeroModule:ResHeroList(data)
	local res = table.deepcopy(data)
	if type(res.hero_list) == 'table' then
		self.hero_list = res.hero_list
		print(#self.hero_list)
		for index, hero in ipairs(self.hero_list) do
			print(hero.hero_id,hero.entry)
		end
	end
end

return HeroModule