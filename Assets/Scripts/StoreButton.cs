using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StoreButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemPrice;

    public void Initialize(Item item)
    {
        itemIcon.sprite = item.icon;
        itemName.text = item.title;
        itemPrice.text = item.buyPrice.ToString();
    }
}
