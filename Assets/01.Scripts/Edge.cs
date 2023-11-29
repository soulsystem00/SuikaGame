using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Edge : MonoBehaviour
{
    [SerializeField] private Vector3 viewPortPos;

    private void Update()
    {
        transform.position = Camera.main.ViewportToWorldPoint(viewPortPos);
    }

}
