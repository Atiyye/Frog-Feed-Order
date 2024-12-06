using UnityEngine;

public static class Consts 
{
    public struct Type
    {
        public const string frog = "Frog";
        public const string arrow = "Arrow";
        public const string grape = "Grape";
    }
    
    public struct Count
    {
        public const int colorCount = 4;
        public const int typeCount = 3;
        public const int gridMinChild = 2;
        
    }
    public struct PosNumber
    {
        public const float yPos = 0.02f;
        public const float zPos = 0.01f;
    }
    public struct Rotate
    {
        public const int left = 90;
        public const int right = 270;
        public const int down = 0;
        public const int up = 180;
    }
    
    public struct Coordinates
    {
        public const int start = 0;
    }
    
    public struct Color
    {
        public const string blue = "Blue";
        public const string green = "Green";
        public const string red = "Red";
        public const string yellow = "Yellow";
    }
    
    public struct TongueValue
    {
        public static readonly Vector3 create = new Vector3(22f, 5f, 2f);
        public static readonly Vector3 delete = new Vector3(22f, 5f, 2f);
    }
}