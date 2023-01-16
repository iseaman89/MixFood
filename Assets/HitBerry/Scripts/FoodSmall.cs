using DG.Tweening;
using UnityEngine;

public class FoodSmall: Food, IFoodAnimations
{
    #region Functions
    
    private void OnMouseDown()
    {
        JumpAnimation();
        JumpInBlender();
    }

    public void JumpAnimation()
    {
        transform.DORotate(Vector3.zero, 0);
        
        transform.DOJump(Blender, JumpForce, 1, Duration).SetEase(Ease.OutQuad);

        transform.DOPunchRotation(new Vector3(0, 360, 0), Duration, 0, 2);
    }

    #endregion
}