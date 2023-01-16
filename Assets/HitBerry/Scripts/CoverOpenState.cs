using DG.Tweening;
using UnityEngine;

public class CoverOpenState : State
{
    public CoverOpenState(BlenderAnimation blenderAnimation) => _blenderAnimation = blenderAnimation;

    #region Variables

    private readonly BlenderAnimation _blenderAnimation;
    private readonly Tween[] _openTweens  = new Tween[2];

    #endregion

    #region Functions

    public override void Enter()
    {
        _openTweens[0] = 
            _blenderAnimation.Deckel.transform.DOLocalMove(new Vector3(0.047f,0.342f,0.001f)
                , _blenderAnimation.Delay / 2);
        _openTweens[1] =
            _blenderAnimation.Deckel.transform.DORotateQuaternion(_blenderAnimation.DeckelOpen.transform.rotation,
                _blenderAnimation.Delay / 2);
    }

    public override void Exit()
    {
        foreach (var closeTween in _openTweens)
        {
            closeTween.Kill();
        }
    }

    #endregion
}