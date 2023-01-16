using UnityEngine;
using DG.Tweening;

public class CoverCloseState : State
{
    public CoverCloseState(BlenderAnimation blenderAnimation) => _blenderAnimation = blenderAnimation;

    #region Variables

    private readonly BlenderAnimation _blenderAnimation;
    private readonly Tween[] _closeTweens  = new Tween[2];

    #endregion

    #region Functions

    public override void Enter()
    {
        _closeTweens[0] =
            _blenderAnimation.Deckel.transform.DOLocalMove(new Vector3(0.001f,0.254f,-0.007f), 
                _blenderAnimation.Delay / 2);
        _closeTweens[1] = 
            _blenderAnimation.Deckel.transform.DORotateQuaternion(_blenderAnimation.DeckelClose.transform.rotation,
                _blenderAnimation.Delay / 2);
    }

    public override void Exit()
    {
        foreach (var closeTween in _closeTweens)
        {
            closeTween.Kill();
        }
    }

    #endregion
}