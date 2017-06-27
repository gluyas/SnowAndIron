
using UnityEngine;

public class DestroyAnimation : IAnimation
{
	private readonly UnitAvatar _target;
	private float _elapsed;
	private bool _triggered;
	
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
			_target.Destroy();
			return true;
		}
		return false;
	}
}
