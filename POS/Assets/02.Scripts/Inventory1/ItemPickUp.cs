using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item Item;

    
    
    //박스에 넣으면 인벤토리에 추가되게 하는함수
    public void PickUp()
    {
        InventoryManager.Instance.Add(Item);
        InventoryManager.Instance.ListItems();
        //Destroy(gameObject);
        gameObject.SetActive(false);
       
    }
}
