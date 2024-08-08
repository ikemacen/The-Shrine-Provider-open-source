using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoptrigger : MonoBehaviour
{
    [SerializeField] private Shop uishop;
    private void OnTriggerEnter(Collider player) //shows the shop once the player is within the trigger field
    {
        Shopinterface playershopper = player.GetComponent<Shopinterface>();
        if (playershopper != null)
        {
            uishop.show(playershopper);
        }
    }

    private void OnTriggerExit(Collider player) //makes the shop disappear when the player leaves the field
    {
        Shopinterface playershopper = player.GetComponent<Shopinterface>();
        if (playershopper != null)
        {
            uishop.Hide();
        }
    }
}
