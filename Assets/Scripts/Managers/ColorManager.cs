using UnityEngine;

public class ColorManager : MonoBehaviour
{
    #region Veriables

    [SerializeField] Material groundMaterial;
    [SerializeField] SpriteRenderer fadeSprite;
    [SerializeField] Camera camera;

    [SerializeField] Color groundColor;
    [SerializeField] Color fadeSpriteColor;
    [SerializeField] Color cameraColor;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        groundMaterial.color = groundColor;
        fadeSprite.color = fadeSpriteColor;
        camera.backgroundColor = cameraColor;
    }

    #endregion
}
