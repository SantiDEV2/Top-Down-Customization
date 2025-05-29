using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [Header("References")]
    public Sprite icon;
    public int id;

    [Header("Variables")]
    public string title;
    public int buyPrice;
    public int salePrice;

    [Header("Slot type")]
    public SlotType slotType;
    public enum SlotType { Hat, Torso, Legs }

    [Header("Sprite Library")]
    public SpriteLibraryAsset spriteLibrary;
}
