using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StoreButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemPrice;

    private PlayerController player;
    private Item item;
    private Button button;

    private void OnEnable()
    {
        EventManager.StartListening("ItemChange", UpdateButtonState);
    }
    private void OnDisable()
    {
        EventManager.StopListening("ItemChange", UpdateButtonState);
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        player = FindFirstObjectByType<PlayerController>();
    }

    public void Initialize(Item item)
    {
        this.item = item;
        itemIcon.sprite = item.icon;
        itemName.text = item.title;
        itemPrice.text = item.buyPrice.ToString();

        UpdateButtonState(null);
    }

    public void UpdateButtonState(object param)
    {
        button.interactable = player.coins >= item.buyPrice;
    }
}
