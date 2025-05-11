using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [Header("References")]
    public Sprite icon;

    [Header("Variables")]
    public string title;
    public int buyPrice;
    public int salePrice;
    public bool isOwned;
}
