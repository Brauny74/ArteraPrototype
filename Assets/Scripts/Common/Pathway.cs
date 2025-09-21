using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace TopDownShooter
{
    public class Pathway : MonoBehaviour
    {
        public List<Transform> PathPoints = new List<Transform>();

        public Transform GetNextPoint(Transform point)
        {
            for (int i = 0; i < PathPoints.Count; i++)
            {
                if(PathPoints[i] == point)
                {
                    if (i == PathPoints.Count - 1)
                        return PathPoints[0];
                    else
                        return PathPoints[i + 1];
                }
            }
            return null;
        }

        public Transform GetClosestPoint(Vector3 position)
        {
            Transform closest = null;
            foreach (Transform point in PathPoints)
            {
                if (closest == null || Vector3.Distance(point.position, position) < Vector3.Distance(position, closest.position))
                {
                    closest = point;
                }
            }
            return closest;
        }
    }
}