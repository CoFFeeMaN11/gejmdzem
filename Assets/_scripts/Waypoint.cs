using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour {

#if UNITY_EDITOR
    public Map map;
#endif
    // Use this for initialization
    void Start () {
        
	}
	// Update is called once per frame
	void Update () {
        SnapToGrid();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "3-512.png", true);
    }

    public void SnapToGrid()
    {
#if UNITY_EDITOR
        Vector3 newPos = map.ToVector(map.FromVector(transform.position));
        transform.position = newPos;
#endif
    }
}
