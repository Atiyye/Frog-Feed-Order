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
    public struct Rotate
    {
        public const int left = 90;
        public const int right = 270;
        public const int down = 0;
        public const int up = 180;
    }
    
    public struct Color
    {
        public const string blue = "Blue";
        public const string green = "Green";
        public const string red = "Red";
        public const string yellow = "Yellow";
    }
    public struct Second
    {
        public const float contentsGrowth = .3f;
        public const float contentGathering = 10f;
        public const float tongueDelete = .01f;
        public const float wrongFrog = .5f;
        public const float deleteTongue = .3f;
        public const float deleteNode = 10f;
        public const float contentDelete = .07f;
        public const float delete = .08f;
    }
    
    
}