using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region Veriables

    public static GameManager Instance;
    public GameState.States currentState;

    [SerializeField] GameObject confettiFx;

    GameObject[] objects;
    float objectCount;
    float currentCount;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        objects = GameObject.FindGameObjectsWithTag("Object");
        objectCount = objects.Length;
    }

    #endregion

    #region Private Methods

    public void TakeObject()
    {
        currentCount +=1;
        if (currentCount >= objectCount)
        {
            confettiFx.SetActive(true);
            UIManager.Instance.Invoke("NextLevel", 1.25f);
            currentState = GameState.States.gameOver;
        }
        UIManager.Instance.progressBar.DOFillAmount((currentCount / objectCount), .4f);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator LoadCurrentLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        UIManager.Instance.StartGame();
        currentState = GameState.States.gameStarted;
    }

    #endregion
}
