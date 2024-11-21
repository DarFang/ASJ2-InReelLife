using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : NPCInteractTrigger
{
    bool isOpen = false;
    public override void Interact()
    {
        isOpen =  !isOpen;
        if (isOpen)
        {
            FindObjectOfType<Shop>().OpenShop();
            GameManager.Instance.DisablePlayerInput();
        }
        else
        {
            FindObjectOfType<Shop>().CloseShop();
            GameManager.Instance.EnablePlayerInput();
        }
    }
}
