using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item Item;

    private Vector3 InitScale;
    private Vector3 TargetScale;
    private float ScaleSpeed=20f;
    private void Start()
    {
        InitScale = transform.localScale;
        TargetScale = new Vector3(0.07f, 0.07f, 0.07f);
    }

    //박스에 넣으면 인벤토리에 추가되게 하는함수
    public void PickUp()
    {
        InventoryManager.Instance.Add(Item);
        InventoryManager.Instance.ListItems();


        StartCoroutine(ResizeScale());
        Debug.Log("localscale:" + transform.localScale);
        StartCoroutine(DestoryItem());

        
        //gameObject.SetActive(false);
       
    }

    IEnumerator ResizeScale()
    {
        while(transform.localScale!=TargetScale)
        {
            yield return null;

            InitScale = Vector3.Lerp(InitScale, TargetScale, ScaleSpeed * Time.deltaTime);
            transform.localScale = InitScale;
        }
    }

    IEnumerator DestoryItem()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
