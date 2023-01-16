using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

   public class HUD : MonoBehaviour
    {
        #region Variables

        private bool _pauseMenuOpened;
        
        [SerializeField] private float _delay = 1f;
        [SerializeField] private List<Sprite> _medalSprites;
        [SerializeField] private Button _NextLevelButton;
        [SerializeField] private TextMeshProUGUI _percentText;
        [SerializeField] private TextMeshProUGUI _gameEndLabel;
        [SerializeField] private RectTransform _gameEndPanel;
       
        [SerializeField] private GameObject _medal;
        [SerializeField] private ParticleSystem _starsParticleSystem;
        
        public static HUD Instance { get; private set; }

        #endregion

        #region Functions

        private void Awake() => Instance = this;
        
        
        public void RestartButton()
        {
            EventController.Instance.RestartLevel();
            HideGameEndPanel();
        }

        public void NextLevelButton()
        {
            HideGameEndPanel();
            EventController.Instance.NextLevel();
        }

        public IEnumerator ShowGameEndPanel()
        {
            _percentText.text = 0 + StringData.Percent;
            yield return new WaitForSeconds(_delay);
            ChangeTopLabel();
            _gameEndPanel.DOScale(Vector3.one, _delay).SetEase(Ease.OutBack).OnComplete(() =>
            {
                StartCoroutine(ChangePercent());
            });
        }

        private void ChangeTopLabel()
        {
            _gameEndLabel.text = GameController.Instance.CurrentPercent switch
            {
                < 90 => StringData.LabelLose[Random.Range(0, 3)],
                >= 90 and < 95 => StringData.LabelWinBronze,
                >= 95 and < 98 => StringData.LabelWinSilver,
                >= 98 => StringData.LabelWinGold
            };
        }

        private void HideGameEndPanel()
        {
            _gameEndPanel.DOScale(Vector3.zero, _delay).SetEase(Ease.InBack).OnComplete(ResetMedal);
        }

        public IEnumerator ShowMedal(int medalPrice)
        {
            yield return new WaitForSeconds(2.5f);
            StartCoroutine(ShowStars());
            _medal.GetComponent<RectTransform>().DOLocalMove(
                new Vector3(44, _medal.transform.localPosition.y, 0), _delay).SetEase(Ease.OutBack);
            _medal.transform.DOScale(Vector3.one, _delay);
            _medal.GetComponent<Image>().sprite = _medalSprites[medalPrice];
        }

        private IEnumerator ShowStars()
        {
            yield return new WaitForSeconds(_delay - .2f);
            _starsParticleSystem.Play();
        }

        private void ResetMedal()
        {
            _starsParticleSystem.Stop();
            _medal.GetComponent<RectTransform>().DOLocalMove(
                new Vector3(-4000, _medal.transform.localPosition.y, 0), .1f);
            _medal.transform.localScale = new Vector3(10, 10, 1);
        }

        private IEnumerator ChangePercent()
        {
            _NextLevelButton.interactable = false;
            float percent = 0;
            
            for (int i = 0; i <= GameController.Instance.CurrentPercent; i++)
            {
                yield return new WaitForSeconds(.01f);
                _percentText.text = (int)percent + StringData.Percent;
                percent += 1f;
            }

            if (GameController.Instance.CurrentPercent >= 90)
            {
                _NextLevelButton.interactable = true;
                EventController.Instance.LevelDone();
            }
            _percentText.text = GameController.Instance.CurrentPercent + StringData.Percent;
        }
        #endregion
    }