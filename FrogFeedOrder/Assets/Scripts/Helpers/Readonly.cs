using UnityEngine;

public static class Readonly
{
    public struct TongueValue
    {
        public static readonly Vector3 tongueCreate = new Vector3(22f, 5f, 2f);
        public static readonly Vector3 tongueDelete = new Vector3(0, 0, 0);
        public static readonly Vector3 tongueCreateArrow = new Vector3(1f, .2f, .2f);
    }
    public struct ContentValue
    {
        public static readonly Vector3 contentOriginalSize = new Vector3(.75f, .75f, .75f);
        public static readonly Vector3 contentBigSize = new Vector3(1f, 1f, 1f);
        public static readonly Vector3 contentDelete = new Vector3(0, 0, 0);
    }
    
}