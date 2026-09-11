using UnityEngine;

[CreateAssetMenu(fileName = "NewBuildingData", menuName = "Building System/Building Data")]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    public int cost; // Ціна будівлі
    public GameObject prefab;
}