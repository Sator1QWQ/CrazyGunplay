require "Configs.Text.Grenade_Text"
Grenade = {
	[201] = {
		id = 201,
		desc = Grenade_Text.desc_4,
		bulletId = 1005,
		isSkill = true,
		rangeType = 2,
		radius = 1.5,
		fireRate = 0.5,
		delay = 1.5,
		maxBeatBackValue = 0,
		buffId = 101,
		buffValue = 0.3,
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
		maxBeatBackValue = 10,
		buffId = -1,
		buffValue = -1,
		buffDuration = -1,
		effectPath = Grenade_Text.effectPath_5,
	},
}
Grenade.Count = 2