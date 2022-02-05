using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMy : MonoBehaviour
{
    private void Start()
    {
        Invoke("NavSurface",2f);
    }
    void NavSurface()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
