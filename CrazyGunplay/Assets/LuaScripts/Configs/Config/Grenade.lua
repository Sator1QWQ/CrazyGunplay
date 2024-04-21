require "Configs.Text.Grenade_Text"
Grenade = {
	[201] = {
		id = 201,
		desc = Grenade_Text.desc_4,
		bulletId = 1005,
		isSkill = true,
		rangeType = 1,
		radius = 1.5,
		fireRate = 0.5,
		delay = 1.5,
		buffId = 101,
		buffValue = 10,
		buffDuration = 2,
		effectPath = Grenade_Text.effectPath_4,
	},
	[202] = {
		id = 202,
		desc = Grenade_Text.desc_5,
		bulletId = 1003,
		isSkill = false,
		rangeType = 1,
		radius = 1.5,
		fireRate = 0.5,
		delay = 2,
		buffId = -1,
		buffValue = nil,
		buffDuration = nil,
		effectPath = Grenade_Text.effectPath_5,
	},
}
Grenade.Count = 2