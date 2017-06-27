﻿using System;
using UnityEngine;
using Model;

public class CombatAnimation : SyncedAnimation<CombatAnimation, CombatAnimation>
{
	private float _flashTime = 0;
	private int _impactStage = 0;
	private int _attackerEnergy;
	private int _defenderHealth;

	private Vector3 _attackVector;
	private Vector3 _startPos;

	private float _animationTime = 0;
	private AnimationCurve AttackCurve { get { return GuiComponents.GetAttackMotion(); } }
	private float AttackTimeScale { get { return GuiComponents.GetAttackMotionTimeScale(); } }
	private float ImpactTime { get { return GuiComponents.GetAttackMotionImpactTime(); } }
	
	public CombatAnimation(Unit attacker, Unit defender, int damage) : base(attacker.Avatar, defender.Avatar)
	{
		// TODO: use damage numbers with advanced bar system
		_attackerEnergy = attacker.Energy;
		_defenderHealth = defender.Health;

		 var targetPos = defender.Position.ToVector3();
		_startPos = attacker.Position.ToVector3();
		_attackVector = (targetPos - _startPos) / 2;
	}

	public override bool ApplyAnimation(float time)
	{
		if (Sync()) 
		{
			_animationTime += time / AttackTimeScale;
			Owner.Position = _startPos + _attackVector * AttackCurve.Evaluate(_animationTime);

			if (_animationTime >= ImpactTime && _impactStage >= 0)	// impact effects!
			{
				var color = Color.black;
				float flashTime = 0;
				switch (_impactStage)
				{
					case 0:
						color = GuiComponents.GetHitPrimaryColor();
						flashTime = GuiComponents.GetHitPrimaryTime();
						break;
					case 1:
						color = GuiComponents.GetHitSecondaryColor();
						flashTime = GuiComponents.GetHitSecondaryTime();
						break;
					default:
						Target.ResetPaint();
						Target.Scale = 1;
						Owner.UpdateEnergy(_attackerEnergy);
						Target.UpdateHealth(_defenderHealth);
						_impactStage = -1;
						break;
				}
				if (_impactStage >= 0)
				{
					Target.Paint(color);
					Target.Scale = 1.1f;

					_flashTime += time;
					if (_flashTime > flashTime)
					{
						_impactStage++;
						_flashTime = 0;
					}
				}
			}
			if (_animationTime >= 1) return true;
		}
		return false;
	}
}


