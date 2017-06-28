﻿using System;
using UnityEngine;
using Model;
 using Object = UnityEngine.Object;

public class CombatAnimation : SyncedAnimation<CombatAnimation, CombatAnimation>
{
	private float _flashTime = 0;
	private int _impactStage = 0;
	private int _attackerEnergy;
	private int _defenderHealth;
	private int _damage;
	
	[FMODUnity.EventRef]
	public string chargeSound = "event:/Attack charge";
	[FMODUnity.EventRef]
	public string impactSound = "event:/Attack impact";
	private Vector3 _attackVector;
	private Vector3 _startPos;

	private GameObject _impactEffect;

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

		_damage = damage;
	}

	bool _once;

	public override bool ApplyAnimation(float time)
	{
		if (Sync())
		{
			if (_damage == 0) return true;	// don't hit!
			
			if (!_once) {
				
				FMODUnity.RuntimeManager.PlayOneShot (chargeSound, new Vector3 (0, 0, 0));

				_once = true;
			}

			_animationTime += time / AttackTimeScale * Owner.MoveSpeed * 2;
			Owner.Position = _startPos + _attackVector * AttackCurve.Evaluate(_animationTime);

			if (_animationTime >= ImpactTime && _impactStage >= 0)	// impact effects!
			{
				if (_impactEffect == null)
				{
					FMODUnity.RuntimeManager.PlayOneShot (impactSound, new Vector3 (0, 0, 0));
					_impactEffect = Object.Instantiate(GuiComponents.GetImpactEffect());
					_impactEffect.transform.position = Owner.Position;
					_impactEffect.transform.position += GuiComponents.GetEffectHeightOffset();
				}
				
				var color = Color.black;
				float flashTime = 0;
				switch (_impactStage)
				{
				case -1:
					break;
				case 0:
					Utils.Print ("1");
						color = GuiComponents.GetHitPrimaryColor();
						flashTime = GuiComponents.GetHitPrimaryTime();
						break;
					case 1:
					Utils.Print ("2");
						color = GuiComponents.GetHitSecondaryColor();
						flashTime = GuiComponents.GetHitSecondaryTime();
						break;
					default:
					Utils.Print ("3");
						Target.ResetPaint();
						Target.Scale = 1;
						Owner.UpdateEnergy(_attackerEnergy);
						Target.UpdateHealth(_defenderHealth);
						_impactStage = -1;
						break;
				}
				if (_impactStage >= 0)
				{
					Utils.Print ("4");
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


