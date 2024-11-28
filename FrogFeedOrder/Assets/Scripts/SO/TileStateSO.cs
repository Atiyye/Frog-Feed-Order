using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Tile State")] 
public class TileStateSO : ScriptableObject
{
    public Material tileMaterial;
    public Material objectMaterial;
    public GameObject tileContent;
    public string objectType;
}
