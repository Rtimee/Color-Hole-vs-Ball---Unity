using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Veriables

    public static UIManager Instance;
    public Image progressBar;

    public GameObject infoScreen;
    public GameObject gameScreen;
    public GameObject nextLevelScreen;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    #region Private Methods

    public void StartGame()
    {
        infoScreen.SetActive(false);
        gameScreen.SetActive(true);
    }

    public void NextLevel()
    {
        gameScreen.SetActive(false);
        nextLevelScreen.SetActive(true);
    }

    #endregion
}
