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
        CreateItembutton("none", Items.GetCost(Items.Itemtype.none), 0);
        CreateItembutton("pitchfork", Items.GetCost(Items.Itemtype.Pitchfork), 1);

        Hide();
    }


    private void CreateItembutton(string itemname, int itemcost, int PostionIndex) //creates istance of templates for every item stored in items with thier prices for the shop UI
    {
        Transform shopitemTransform = Instantiate(Shopitem, Contianer);
        shopitemTransform.gameObject.SetActive(true);
        RectTransform shopitemRectTransform = shopitemTransform.GetComponent<RectTransform>();

        float shopitemheight = 160f;
        shopitemRectTransform.anchoredPosition = new Vector2(0, -shopitemheight * PostionIndex);

        shopitemTransform.Find("shopitem name").GetComponent<TextMeshProUGUI>().SetText(itemname);
        shopitemTransform.Find("shopitem cost").GetComponent<TextMeshProUGUI>().SetText(itemcost.ToString());
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

}
