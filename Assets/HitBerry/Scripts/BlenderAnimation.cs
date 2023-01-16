using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

    public class BlenderAnimation : MonoBehaviour
    {
        #region Variables

        private Vector3 _originalRotation;
        private Vector3 _originalScale;
        private Vector3 _originalScaleButton;
        private StateMachine _stateMachine;
        
        [SerializeField] private Transform _deckelOpen;
        [SerializeField] private Transform _deckelClose;
        [SerializeField] private GameObject _deckel;
        [SerializeField] private float _delay = 1f;
        [SerializeField] private Button _mixButton;
        
        public Transform DeckelClose => _deckelClose;
        public Transform DeckelOpen => _deckelOpen;
        public GameObject Deckel => _deckel;
        public float Delay => _delay;

        #endregion

        #region Functions

        private void Start()
        {
            _stateMachine = new StateMachine();
            _stateMachine.Initialize(new CoverCloseState(this));
            
            _originalRotation = transform.localEulerAngles;
            _originalScale = transform.localScale;
            _originalScaleButton = _mixButton.transform.localScale;
        }

        private void OnEnable()
        {
            EventController.OnCoverOpened += CoverOpened;
            EventController.OnCoverClosed += CoverClosed;
            EventController.OnMixerShake += ShakeMixer;
        }

        private void OnDisable()
        {
            EventController.OnCoverOpened -= CoverOpened;
            EventController.OnCoverClosed -= CoverClosed;
            EventController.OnMixerShake -= ShakeMixer;
        }

        private void CoverOpened() => _stateMachine.ChangeState(new CoverOpenState(this));

        private void CoverClosed() => _stateMachine.ChangeState(new CoverCloseState(this));
        
        private void ShakeMixer(int loops, float frequence)
        {
            transform.DORotate(new Vector3(2, _originalRotation.y + 2, 2), frequence).OnComplete(() =>
            {
                transform.DORotate(
                    new Vector3(-2, _originalRotation.y - 2, -2), frequence).OnComplete(() =>
                {
                    transform.DORotate(_originalRotation, frequence);
                });
            }).SetLoops(loops);
            transform.DOScale(new Vector3(_originalScale.x + .1f, _originalScale.y + .1f,
                _originalScale.z + .1f), frequence).OnComplete(() =>
            {
                transform.DOScale(
                    new Vector3(_originalScale.x - .1f, _originalScale.y - .1f, _originalScale.z - .1f),
                    frequence).OnComplete(() =>
                {
                    transform.DOScale(_originalScale, frequence);
                });
            }).SetLoops(loops);
            
            _mixButton.transform.DOScale(new Vector3(_originalScaleButton.x + .1f, 
                    _originalScaleButton.y + .1f, _originalScaleButton.z + .1f), 
                frequence / 2).OnComplete(() =>
            {
                _mixButton.transform.DOScale(
                    new Vector3(_originalScaleButton.x - .1f, _originalScaleButton.y - .1f, 
                        _originalScaleButton.z - .1f), frequence / 2).OnComplete(() =>
                {
                    _mixButton.transform.DOScale(Vector3.one, frequence / 2);
                });
            }).SetLoops(loops);
        }
        #endregion
    }