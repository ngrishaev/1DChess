using UnityEngine;

namespace Common
{
    public static class BoundExtensions
    {
        public static bool IsOverlapInZ(this Bounds bounds, Vector3 coordinate)
        {
            return (coordinate.x <= bounds.max.x &&
                    coordinate.x >= bounds.min.x &&
                    coordinate.y <= bounds.max.y &&
                    coordinate.y >= bounds.min.y);
        }
        
    }
}