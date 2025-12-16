using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject droppedModel;
    public GameObject placedModel;
    
    public int count;
    public int maxCount;
}