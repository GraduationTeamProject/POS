using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    /*�� ��ũ��Ʈ�� �����۽��Կ� ����Ǵ� ��ũ��Ʈ��, 
     �κ��丮 UI�� ǥ�õǴ� �������� �����ϰų�,
    �������� ����Ʈ�� ���ϰų�, �������� ������ �Լ��� �ۼ��Ǿ��ֽ��ϴ�.*/
    public Item item;       
    public Button RemoveButton;
    public GameObject[] Items;
    public string ItemName;
    public Transform playerPos;

    private GameObject InstanItem;

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


                    InstanItem = Instantiate(item);
                    Debug.Log("InstanItem layer:" + InstanItem.layer);
                    if(item.layer==8)
                        BuildManager.Instance.OriginalPrefab = InstanItem;
                
                   

                    

                //��ġ���� ������Ʈ�� �ƴҶ���
                if (InstanItem.layer!=8)
                {
                    //���̴� ȿ��
                    if (InstanItem.GetComponent<MeshRenderer>() != null)
                        InstanItem.GetComponent<MeshRenderer>().material.SetVector("_DissolveOffest", new Vector3(0, 1, 0));
                    else
                        InstanItem.GetComponentInChildren<MeshRenderer>().material.SetVector("_DissolveOffest", new Vector3(0, 1, 0));
                }
                   

                //�������� ������ ������ �ٿ��� �ϱ� ���� ó��
                InstanItem.GetComponent<ItemPickUp>().Item.Count--;
                    ItemController itemController = InstanItem.GetComponent<ItemController>();
                    itemController.Start = true;

                //(�߰�)���� 0 �̸� �����۽���UI �Ⱥ��̰� ����
                if (InstanItem.GetComponent<ItemPickUp>().Item.Count==0)
                        RemoveItem();
                    else
                        InventoryManager.Instance.ListItems();
                if(InstanItem.layer!=8)
                    InstanItem.transform.position = playerPos.position + new Vector3(0f, 0f, 1f);
            }
        }
       
    }

}
