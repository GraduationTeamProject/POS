using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBox : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Item")
        {
            collision.gameObject.GetComponent<ItemPickUp>().PickUp();
            
        }
    }
}
