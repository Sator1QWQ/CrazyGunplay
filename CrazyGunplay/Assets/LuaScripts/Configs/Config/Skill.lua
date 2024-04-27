require "Configs.Text.Skill_Text"
--***********************代码由工具生成***********************--
Skill = {
	[1001] = {
		id = 1001,
		name = Skill_Text.name_4,
		type = 1,
		isBigSkill = false,
		mode = 1,
		values = {
			[1] = 201,
		},
		castTarget = 2,
		targetMode = nil,
		compare = nil,
		targetValue = nil,
		targetNum = nil,
		beforeCast = 0,
		afterCast = 0,
		activeCondition = {
			[1] = 0,
		},
		skillDuration = -1,
		coolingTime = 10,
		canUseNum = 2,
	},
	[1002] = {
		id = 1002,
		name = Skill_Text.name_5,
		type = 1,
		isBigSkill = false,
		mode = 4,
		values = {
		},
		castTarget = 2,
		targetMode = nil,
		compare = nil,
		targetValue = nil,
		targetNum = nil,
		beforeCast = 0.5,
		afterCast = 0,
		activeCondition = {
			[1] = 0,
		},
		skillDuration = 3.5,
		coolingTime = 8,
		canUseNum = 5,
	},
	[1003] = {
		id = 1003,
		name = Skill_Text.name_6,
		type = 1,
		isBigSkill = true,
		mode = 2,
		values = {
		},
		castTarget = 4,
		targetMode = 2,
		compare = 4,
		targetValue = nil,
		targetNum = 1,
		beforeCast = 1,
		afterCast = 0,
		activeCondition = {
			[1] = 1,
			[2] = 30,
		},
		skillDuration = 10,
		coolingTime = 30,
		canUseNum = 1,
	},
}
Skill.Count = 3