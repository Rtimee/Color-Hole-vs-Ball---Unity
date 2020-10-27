using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    #region Veriables

    [SerializeField] MeshCollider meshCollider;
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] Transform circle;
    [SerializeField] float holeRadius;
    [SerializeField] [Range(0, 1f)] float movingSpeed;
    [SerializeField] Vector2 moveLimits;

    Transform holeCenter;
    Mesh mesh;

    int holeVerticesCount;
    float x, y;
    Vector3 touch, targetPos;

    List<int> holeVertices;
    List<Vector3> offsets;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        holeVertices = new List<int>();
        offsets = new List<Vector3>();
        holeCenter = transform;
        mesh = meshFilter.mesh;
    }

    private void Start()
    {
        FindHoleVertices();
        CircleAnimation();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && GameManager.Instance.currentState == GameState.States.gameStarted)
        {
            MoveHoleCenter();
            UpdateHoleVertices();
        }
    }

    #endregion

    #region Private Methods

    void CircleAnimation()
    {
        circle.DORotate(new Vector3(90, 0, -90), .5f).SetEase(Ease.Linear).From(new Vector3(90, 0, 0)).SetLoops(-1, LoopType.Incremental);
    }

    void FindHoleVertices()
    {
        for (int i = 0; i < mesh.vertexCount; i++)
        {
            float distance = Vector3.Distance(holeCenter.position, mesh.vertices[i]);
            if(distance < holeRadius)
            {
                holeVertices.Add(i);
                offsets.Add(mesh.vertices[i] - holeCenter.position);
            }
        }
        holeVerticesCount = holeVertices.Count;
    }

    void MoveHoleCenter()
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        touch = Vector3.Lerp(holeCenter.position, holeCenter.position + new Vector3(x, 0, y), movingSpeed);
        targetPos = new Vector3(Mathf.Clamp(touch.x, -moveLimits.x, moveLimits.x), 0, Mathf.Clamp(touch.z, -moveLimits.y, moveLimits.y));
        holeCenter.position = targetPos;
    }

    void UpdateHoleVertices()
    {
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < holeVertices.Count; i++)
        {
            vertices[holeVertices[i]] = holeCenter.position + offsets[i];
        }

        // Update Mesh
        mesh.vertices = vertices;
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    #endregion
}
