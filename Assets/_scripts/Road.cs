using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Road : MonoBehaviour {

    public List<Waypoint> WayPoints = new List<Waypoint>();

    public void AddWaypoint(Vector3 pos, Map _map)
    {
        var waypoint = new GameObject(string.Format("Waypoint{0:00}", WayPoints.Count)).AddComponent<Waypoint>();
        waypoint.transform.position = pos;
        waypoint.transform.parent = transform;
        waypoint.map = _map;
        WayPoints.Add(waypoint);
    }

    private void OnDrawGizmos()
    {
        if (WayPoints == null || WayPoints.Count < 2) return;
        for (int i = 1; i < WayPoints.Count; i++)
            Gizmos.DrawLine(WayPoints[i - 1].transform.position, WayPoints[i].transform.position);

    }

}
