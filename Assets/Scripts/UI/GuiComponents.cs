using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiComponents : MonoBehaviour {
	
	private static GuiComponents _instance;

	// Use this for initialization
	private void Start() {
		_instance = this;
	}
	
	// HEALTH / ENERGY BARS
	
	public GameObject HpBar;
	public static GameObject GetHpBar() {
		return _instance.HpBar;
	}

	public GameObject EpBar;
	public static GameObject GetEpBar() {
		return _instance.EpBar;
	}
	
	// PARTICLE EFFECTS

	public float EffectHeightOffset;
	public static Vector3 GetEffectHeightOffset()
	{
		return _instance.EffectHeightOffset * ModelExtensions.Up;
	}
	
	public GameObject ExplosionEffect;
	public static GameObject GetExplosionEffect()
	{
		return _instance.ExplosionEffect;
	}
	
	public GameObject ImpactEffect;
	public static GameObject GetImpactEffect()
	{
		return _instance.ImpactEffect;
	}
	
	public GameObject DeployEffect;
	public static GameObject GetDeployEffect()
	{
		return _instance.DeployEffect;
	}

	public GameObject DeployProjectile;
	public static GameObject GetDeployProjectile()
	{
		return _instance.DeployProjectile;
	}
	
	// DEPLOY ANIMATION

	public Vector3 DeployProjectileOrigin;
	public static Vector3 GetDeployProjectileOrigin()
	{
		return _instance.DeployProjectileOrigin;
	}
	
	public float DeployEntranceTime = 0.5f;
	public static float GetDeployEntranceTime()
	{
		return _instance.DeployEntranceTime;
	}

	public float DeplotExitTime = 0.5f;
	public static float GetDeplotExitTime()
	{
		return _instance.DeplotExitTime;
	}
	
	// EXPLOSIONS
	
	public AnimationCurve ExplosionMotion = AnimationCurve.Linear(0, 0, 0, 0);
	public static AnimationCurve GetExplosionMotion()
	{
		return _instance.ExplosionMotion;
	}
	
	public float ExplosionMotionTimeScale = 1f;
	public static float GetExplosionMotionTimeScale()
	{
		return _instance.ExplosionMotionTimeScale;
	}
	
	// HIT EFFECTS

	public AnimationCurve AttackMotion = AnimationCurve.Linear(0, 0, 0, 0);
	public static AnimationCurve GetAttackMotion()
	{
		return _instance.AttackMotion;
	}
	
	public float AttackMotionImpactTime = 0.75f;	// as a percent
	public static float GetAttackMotionImpactTime()
	{
		return _instance.AttackMotionImpactTime;
	}
	
	public float AttackMotionTimeScale = 1f;
	public static float GetAttackMotionTimeScale()
	{
		return _instance.AttackMotionTimeScale;
	}
	
	public Color HitPrimaryColor = Color.yellow;
	public float HitPrimaryTime = 0.1f;
	
	public static Color GetHitPrimaryColor()
	{
		return _instance.HitPrimaryColor;
	}
	
	public static float GetHitPrimaryTime()
	{
		return _instance.HitPrimaryTime;
	}
	
	public Color HitSecondaryColor = Color.red;
	public float HitSecondaryTime = 0.1f;
	
	public static Color GetHitSecondaryColor()
	{
		return _instance.HitSecondaryColor;
	}

	public static float GetHitSecondaryTime()
	{
		return _instance.HitSecondaryTime;
	}

}
