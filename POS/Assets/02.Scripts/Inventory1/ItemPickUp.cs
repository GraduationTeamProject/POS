using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item Item;

    
    
    //�ڽ��� ������ �κ��丮�� �߰��ǰ� �ϴ��Լ�
    public void PickUp()
    {
        InventoryManager.Instance.Add(Item);
        InventoryManager.Instance.ListItems();
        //Destroy(gameObject);
        gameObject.SetActive(false);
       
    }
}
