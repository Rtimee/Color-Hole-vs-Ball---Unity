using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundCollision : MonoBehaviour
{
    #region Veriables

    [SerializeField] Transform camera;

    #endregion

    #region MonoBehaviour Callbacks

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.currentState != GameState.States.gameOver)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                camera.DOShakePosition(1f, .1f, 10, 180, false, true);
                GameManager.Instance.currentState = GameState.States.gameOver;
                StartCoroutine(GameManager.Instance.LoadCurrentLevel());
            }
            else if (other.gameObject.CompareTag("Object"))
            {
                Destroy(other.gameObject);
                GameManager.Instance.TakeObject();
                Magnet.Instance.RemoveFromList(other.attachedRigidbody);
            }
        }
    }

    #endregion
}
