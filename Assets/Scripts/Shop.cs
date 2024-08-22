using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    private Transform Contianer;
    private Transform Shopitem;
    private Shopinterface playershoper;

    private void Awake() //sets up the layout of the shop and makes it hidden when starting the game
    {
        Contianer = transform.Find("Contianer");
        Shopitem = Contianer.Find("Shopitem");
        Shopitem.gameObject.SetActive(false);
    }

    private void Start()//Creates the item buttons in the shop useing the CreateItembutton class for it's template and setup from decending order
    {
        CreateItembutton("Tomato Crop", Items.GetCost(Items.Itemtype.TomatoCrop), 0);
        CreateItembutton("Wheat Crop", Items.GetCost(Items.Itemtype.WheatCrop), 1);
        CreateItembutton("Carrot Crop", Items.GetCost(Items.Itemtype.CarrotCrop), 2);
        CreateItembutton("Corn Crop", Items.GetCost(Items.Itemtype.CornCrop), 3);

        Hide();
    }


    private void CreateItembutton(string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(Shopitem, Contianer);
        shopItemTransform.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 160f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        TextMeshProUGUI nameText = shopItemTransform.Find("shopitem name").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI costText = shopItemTransform.Find("shopitem cost").GetComponent<TextMeshProUGUI>();
        Button button = shopItemTransform.GetComponent<Button>();

        nameText.SetText(itemName);
        costText.SetText(itemCost.ToString());

        button.onClick.RemoveAllListeners(); // Clear existing listeners
        button.onClick.AddListener(() => PurchaseItem(itemName, itemCost)); // Add new listener
    }

    private void PurchaseItem(string itemName, int itemCost)
    {
        if (playershoper.SpendCoins(itemCost))
        {
            // Assuming you have some way to map itemName to Itemtype
            Items.Itemtype itemType = GetItemTypeByName(itemName);

            if (itemType.ToString().Contains("Crop"))
            {
                // This adds seeds to the player's inventory
                playershoper.AddSeed(itemName, 1);
            }
            else
            {
                // Handle other item types if needed
                playershoper.Boughtitem(itemType);
            }
        }
        else
        {
            Debug.Log("Not enough coins to purchase " + itemName);
        }
    }

    private void itempurchase(Items.Itemtype itemtype) // actives the interface and allows the player to buy the item
    {
        playershoper.Boughtitem(itemtype);
    }

    public void show(Shopinterface playershoper) //Shows the UI if it's the player charcter through the interface
    {
        this.playershoper = playershoper;
        gameObject.SetActive(true);
    }

    public void Hide()//hides the UI
    {
        gameObject.SetActive(false);
    }
    private Items.Itemtype GetItemTypeByName(string itemName)
    {
        switch (itemName)
        {
            case "Tomato Crop": return Items.Itemtype.TomatoCrop;
            case "Wheat Crop": return Items.Itemtype.WheatCrop;
            case "Carrot Crop": return Items.Itemtype.CarrotCrop;
            case "Corn Crop": return Items.Itemtype.CornCrop;
            // Add other mappings as needed
            default: return Items.Itemtype.none;
        }
    }   

}
