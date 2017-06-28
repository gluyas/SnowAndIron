using Model;
using UnityEngine;

public class DeployAnimation : IAnimation
{
	public Vector3 ProjectileOrigin
	{
		get { return GuiComponents.GetDeployProjectileOrigin(); }
	}

	private GameObject _projectile;
	private bool _impactTriggered;
	private float _elapsed;
	
	private UnitAvatar _unit;
	[FMODUnity.EventRef]
	public string deploySound;
	public DeployAnimation(UnitAvatar unit)
	{
		_unit = unit;
		deploySound = _unit.inSound;
	}
	
	public bool ApplyAnimation(float time)
	{
		if (!_impactTriggered && _projectile == null)
		{
			_unit.Scale = 0;
			_projectile = Object.Instantiate(GuiComponents.GetDeployProjectile());
			_projectile.transform.position = ProjectileOrigin + GuiComponents.GetEffectHeightOffset();
		}

		if (!_impactTriggered)	// first stage : entrace time
		{
			_projectile.transform.position = Vector3.Lerp(ProjectileOrigin, _unit.Position, _elapsed);
			_elapsed += time / GuiComponents.GetDeployEntranceTime();
			
			if (_elapsed >= 1)	// set up for second stage
			{
				FMODUnity.RuntimeManager.PlayOneShot (deploySound, new Vector3(0,0,0));
				var smoke = Object.Instantiate(GuiComponents.GetDeployEffect());
				smoke.transform.position = _unit.Position;
				
				Object.Destroy(_projectile);
				
				_impactTriggered = true;
				_elapsed = 0;
			}
		}
		else 					// second stage : unit scale time
		{
			_unit.Scale = _elapsed;
			_elapsed += time / GuiComponents.GetDeplotExitTime();
			
			if (_elapsed >= 1)
			{
				_unit.Scale = 1;
				return true;
			}
		}
		return false;
	}
}