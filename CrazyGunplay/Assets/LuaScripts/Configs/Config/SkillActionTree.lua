require "Configs.Text.SkillActionTree_Text"
--***********************代码由工具生成***********************--
SkillActionTree = {
	[301] = {
		id = 301,
		actionType = 2,
		actionValue = {
			[1] = 201,
		},
		conditionType = 1,
		delayTime = 0,
		timerContinueType = 4,
		continueTime = nil,
		areaId = -1,
		areaTriggerType = 1,
		areaTriggerCount = 0,
		areaTriggerInterval = 0,
		buffIdList = {
		},
		buffValueList = {
		},
		buffContinueList = {
		},
		getHitType = 3,
		nextActionList = {
			[1] = -1,
		},
	},
	[302] = {
		id = 302,
		actionType = 5,
		actionValue = {
		},
		conditionType = 1,
		delayTime = 0,
		timerContinueType = 3,
		continueTime = nil,
		areaId = -1,
		areaTriggerType = 1,
		areaTriggerCount = 0,
		areaTriggerInterval = 0,
		buffIdList = {
		},
		buffValueList = {
		},
		buffContinueList = {
		},
		getHitType = 3,
		nextActionList = {
			[1] = -1,
		},
	},
	[303] = {
		id = 303,
		actionType = 3,
		actionValue = {
			[1] = 101,
		},
		conditionType = 1,
		delayTime = 0,
		timerContinueType = 4,
		continueTime = nil,
		areaId = 5003,
		areaTriggerType = 4,
		areaTriggerCount = 0,
		areaTriggerInterval = 0,
		buffIdList = {
			[1] = 110,
			[2] = 109,
		},
		buffValueList = {
			[1] = 4,
			[2] = 20,
		},
		buffContinueList = {
			[1] = 0,
			[2] = 5,
		},
		getHitType = 2,
		nextActionList = {
			[1] = 304,
		},
	},
	[304] = {
		id = 304,
		actionType = 3,
		actionValue = {
			[1] = 103,
		},
		conditionType = 1,
		delayTime = 2,
		timerContinueType = 4,
		continueTime = nil,
		areaId = 5003,
		areaTriggerType = 4,
		areaTriggerCount = 0,
		areaTriggerInterval = 0,
		buffIdList = {
			[1] = 110,
		},
		buffValueList = {
			[1] = 8,
		},
		buffContinueList = {
			[1] = 0,
		},
		getHitType = 2,
		nextActionList = {
			[1] = -1,
		},
	},
}
SkillActionTree.Count = 4