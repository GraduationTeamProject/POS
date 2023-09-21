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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            other.gameObject.GetComponent<ItemPickUp>().PickUp();

        }
    }
}
