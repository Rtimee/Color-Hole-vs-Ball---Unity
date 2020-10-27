using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Magnet : MonoBehaviour
{
    #region Veriables

    public static Magnet Instance;

    [SerializeField] float magnetForce;

    List<Rigidbody> affectedRigidbodies;
    Transform magnet;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        Instance = this;
        affectedRigidbodies = new List<Rigidbody>();
        magnet = transform;
    }

    private void FixedUpdate()
    {
        foreach (Rigidbody rb in affectedRigidbodies)
        {
            rb.AddForce((magnet.position - rb.position) * magnetForce * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Object") || other.transform.CompareTag("Obstacle"))
        {
            AddToList(other.attachedRigidbody);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.CompareTag("Object") || other.transform.CompareTag("Obstacle"))
        {
            RemoveFromList(other.attachedRigidbody);
        }
    }

    #endregion

    #region Private Methods

    void AddToList(Rigidbody rb)
    {
        affectedRigidbodies.Add(rb);
    }

    public void RemoveFromList(Rigidbody rb)
    {
        affectedRigidbodies.Remove(rb);
    }

    #endregion
}