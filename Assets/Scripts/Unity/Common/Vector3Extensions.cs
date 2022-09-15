using UnityEngine;

namespace Common
{
    public static class Vector3Extensions
    {
        public static Vector3 AddX(this Vector3 vector, float val) => 
            new Vector3(vector.x + val, vector.y, vector.z);

        public static Vector3 AddY(this Vector3 vector, float val) => 
            new Vector3(vector.x, vector.y + val, vector.z);

        public static Vector3 AddZ(this Vector3 vector, float val) => 
            new Vector3(vector.x, vector.y, vector.z + val);
        
        public static Vector3 SetX(this Vector3 vector, float val) => 
            new Vector3(val, vector.y, vector.z);

        public static Vector3 SetY(this Vector3 vector, float val) => 
            new Vector3(vector.x, val, vector.z);

        public static Vector3 SetZ(this Vector3 vector, float val) => 
            new Vector3(vector.x, vector.y, val);

    }
}