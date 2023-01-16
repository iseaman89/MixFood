using UnityEngine;

public class Food : MonoBehaviour
{
    #region Variables
    
    protected float Duration = .95f;
    protected Vector3 Blender;
    
    [SerializeField] protected float JumpForce = .2f;
    [SerializeField] private Material _colorToCheck;
    
    public Material ColorToCheck => _colorToCheck;
    public int PoolIndex { get; set; }

    #endregion

    #region Functions
    
    public void Start() => Blender = new Vector3(-1.18985164f,1.767f,6.1808877f);

    protected void JumpInBlender()
    {
        StartCoroutine(GameController.Instance.SpawnFood(PoolIndex));
        EventController.Instance.OpenCover();
    }

    #endregion
}