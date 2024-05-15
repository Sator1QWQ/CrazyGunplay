require "Configs.Text.Skill_Text"
--***********************代码由工具生成***********************--
Skill = {
	[1001] = {
		id = 1001,
		name = Skill_Text.name_4,
		type = 1,
		isBigSkill = false,
		skillAction = 1,
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
		coolingTiming = 2,
		coolingTime = 10,
		expressionList = {
			[1] = 2001,
		},
		canUseNum = 2,
	},
	[1002] = {
		id = 1002,
		name = Skill_Text.name_5,
		type = 1,
		isBigSkill = false,
		skillAction = 4,
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
		coolingTiming = 1,
		coolingTime = 8,
		expressionList = {
			[1] = 2010,
		},
		canUseNum = 5,
	},
	[1003] = {
		id = 1003,
		name = Skill_Text.name_6,
		type = 1,
		isBigSkill = true,
		skillAction = 2,
		values = {
			[1] = 101,
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
			[2] = 0,
		},
		skillDuration = 8,
		coolingTiming = 1,
		coolingTime = 5,
		expressionList = {
			[1] = 2020,
			[2] = 2021,
		},
		canUseNum = 1,
	},
}
Skill.Count = 3