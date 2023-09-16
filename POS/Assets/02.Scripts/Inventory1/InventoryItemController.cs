using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public Item item;
    public Button RemoveButton;
    public GameObject[] Items;
    public string ItemName;
    public Transform playerPos;

    private void Start()
    {
        playerPos = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        ItemName = GetComponentInChildren<Text>().text;

    }
    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        //item = null;
        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void TakeOut()
    {
        
        Debug.Log("�������Լ� !!");
        foreach (var item in Items)
        {
            Debug.Log("���� ������ �̸� :" + ItemName.ToString() + "���Ѿ������̸�:" + item.name.ToString());
            //�̸��� ������
            if (item.gameObject.name.ToString().Contains(ItemName.ToString()))
                {
                    GameObject InstanItem = Instantiate(item);
                    InstanItem.GetComponent<MeshRenderer>().material.SetVector("_DissolveOffest", new Vector3(0, 1, 0));
                    InstanItem.GetComponent<ItemPickUp>().Item.Count--;
                    ItemController itemController = InstanItem.GetComponent<ItemController>();
                    //���Ë� �������������ϰ������..�̻��ϳ�
                    itemController.Start = true;

                //(�߰�)���� 0 �̸� �����۽���UI �Ⱥ��̰� ����
                if (InstanItem.GetComponent<ItemPickUp>().Item.Count==0)
                        RemoveItem();
                    else
                        InventoryManager.Instance.ListItems();
                InstanItem.transform.position = playerPos.position + new Vector3(0f, 0f, 1f);
            }
        }
       
    }

}
