using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blender : MonoBehaviour
{
    #region Variables
    
    private readonly List<Food> _foodsInMixer = new List<Food>();
    private Color _tempColor;
    private Vector3 _tempTransform;
    private readonly ColorsOperations colorsOperations = new ColorsOperations();
    private float _liquadMax = 1;
    private float _liquadCurrent = .4f;
    private int _index;
    private int _foodAmount;
    private bool _canMix = true;
    private readonly int _sideColor = Shader.PropertyToID("_SideColor");
    private readonly int _baseColor = Shader.PropertyToID("_BaseColor");
    private readonly int _fill = Shader.PropertyToID("_Fill");

    [SerializeField] private MeshRenderer _mixSubstance;
    [SerializeField] private Button _mixButton;

    #endregion

    #region Functions

    private void Update() => _mixButton.interactable = _foodAmount != 0;

    private void OnEnable()
    {
        EventController.OnLevelRestarted += LevelReloaded;
        EventController.OnNextLevel += LevelReloaded;
    }

    private void OnDestroy()
    {
        EventController.OnLevelRestarted -= LevelReloaded;
        EventController.OnNextLevel -= LevelReloaded;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out Food food);
        if (food == null) return;
        EventController.Instance.ShakeMixer(1, .5f);
        EventController.Instance.CloseCover();
        _tempColor = food.ColorToCheck.GetColor(_baseColor);
        colorsOperations.AddColors(_tempColor.r, _tempColor.g, _tempColor.b);
        _foodAmount++;
        _foodsInMixer.Add(food);
        if (_foodAmount <= 6) return;
        var tempFood = _foodsInMixer[_index]; 
        tempFood.gameObject.SetActive(false);
        _index++;
    }

    public void MixIt()
    {
        if (!_canMix) return;
        _canMix = false;

        _mixSubstance.material.SetColor(_sideColor, colorsOperations.MixAllColors(_foodAmount));

        colorsOperations.CalculatePercent();
        EventController.Instance.ShakeMixer(5, .5f);
        StartCoroutine(FillBecher());
        StartCoroutine(HUD.Instance.ShowGameEndPanel());
        GameController.Instance.ChangeMedalPrice();
    }

    private IEnumerator FillBecher()
    {
        while (_liquadCurrent < _liquadMax)
        {
            yield return new WaitForSeconds(.01f);
            _liquadCurrent += .005f;
            _mixSubstance.material.SetFloat(_fill, _liquadCurrent);
        }
    }

    private void LevelReloaded()
    {
        _liquadCurrent = .4f;
        _foodAmount = 0;
       colorsOperations.ResetAllColors();
        _canMix = true;
        _mixSubstance.material.SetFloat(_fill, _liquadCurrent);
        _foodsInMixer.Clear();
    }

    #endregion
}