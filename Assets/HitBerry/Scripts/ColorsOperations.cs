using System;
using UnityEngine;

public class ColorsOperations
{
    #region Variables

    private float _colorR;
    private float _colorG;
    private float _colorB;
    private Color _mixColor;

    #endregion

    #region Functions
        
    public void AddColors(float r, float g, float b)
    {
        _colorR += r; 
        _colorG += g; 
        _colorB += b;
    }

    public Color MixAllColors(int foodAmount)
    {
        _mixColor.r = _colorR / foodAmount;
        _mixColor.g = _colorG / foodAmount;
        _mixColor.b = _colorB / foodAmount;
        return _mixColor;
    }

    public void ResetAllColors()
    {
        _colorR = 0; 
        _colorG = 0; 
        _colorB = 0;
    }
        
    public void CalculatePercent()
    {
        float r = Math.Abs(GameController.Instance.CurrentColor.r - _mixColor.r); 
        float g = Math.Abs(GameController.Instance.CurrentColor.g - _mixColor.g);
        float b = Math.Abs(GameController.Instance.CurrentColor.b - _mixColor.b);
        float perr = GameController.Instance.CurrentColor.r / 100;
        float perg = GameController.Instance.CurrentColor.r / 100;
        float perb = GameController.Instance.CurrentColor.r / 100;

        float result;
        Math.Round(result= ((100 - (r / perr)) + (100 - (g / perg)) + (100 - (b / perb))) / 3);
        GameController.Instance.CurrentPercent = (int)result + 1;
    }
        
    #endregion
}