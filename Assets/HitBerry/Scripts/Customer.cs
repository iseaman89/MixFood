using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    #region Variables

    private Animator _animator;
    private Rigidbody _rigidbody;
        
    [SerializeField] private float _delay = 10;
    [SerializeField] private float _jumpforce;
    [SerializeField] private RectTransform _orderPanel;
    [SerializeField] private RawImage _orderLiquad;
        
    #endregion

    #region Functions

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start() => StartCoroutine(ShowOrder());

    private void OnEnable() => EventController.OnLevelDone += LevelDone;

    private void OnDisable() => EventController.OnLevelDone -= LevelDone;

    private void LevelDone()
    {
        _animator.Play("Jump");
        Jump();
        HideOrder();
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpforce);
        transform.DOPunchRotation(new Vector3(0, 360, 0), _delay, 0, 0).OnComplete(Jump);
    }
        
    private IEnumerator ShowOrder()
    {
        yield return new WaitForSeconds(_delay);
        _orderPanel.DOScale(Vector3.one, _delay).SetEase(Ease.OutExpo);
        _orderLiquad.color = GameController.Instance.CurrentColor;
    }

    private void HideOrder() => _orderPanel.DOScale(Vector3.zero, 0).SetEase(Ease.OutExpo);

    #endregion
}