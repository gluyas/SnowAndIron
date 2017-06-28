
using UnityEngine;

public class DestroyAnimation : IAnimation
{
	private readonly UnitAvatar _target;
	private float _elapsed;
	private bool _triggered;
	[FMODUnity.EventRef]
	public string destroySound = "event:/Attack destroy";
	
	public DestroyAnimation(UnitAvatar target)
	{
		_target = target;
	}
	
	public bool ApplyAnimation(float time)
	{
		if (!_triggered)
		{
			var explosion = Object.Instantiate(GuiComponents.GetExplosionEffect());
			explosion.transform.position = _target.Position; //+ GuiComponents.GetEffectHeightOffset();
			_triggered = true;
		}
		
		_elapsed += time / GuiComponents.GetExplosionMotionTimeScale();
		_target.Scale = GuiComponents.GetExplosionMotion().Evaluate(_elapsed);

		if (_elapsed >= 1)
		{
			FMODUnity.RuntimeManager.PlayOneShot (destroySound, new Vector3 (0, 0, 0));
			_target.Destroy();
			return true;
		}
		return false;
	}
}
