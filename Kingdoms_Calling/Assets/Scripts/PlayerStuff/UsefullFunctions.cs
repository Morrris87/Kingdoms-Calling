/*
 * useful Functions class such as debug ray dawing feel free to add anything you need
 * Created by: Bradley Williamson
 * On: 1/15/20
 */


using UnityEngine;

namespace Complete
{
    public class UsefullFunctions
    {

        public static void DebugRay(Vector3 origin, Vector3 v, Color c)
        {
            Debug.DrawRay(origin, v * v.magnitude, c);
        }

        public static Vector3 ClampMagnitude(Vector3 v, float max)
        {

            if (v.magnitude > max)
                return v.normalized * max;
            else
                return v;

        }
        public static float RandomNumber(float min, float max)
        {
            return Random.Range(min, max);
        }
    }
}