using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeZone : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject DirectHand;
    public GameObject PokeHand;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            DirectHand.SetActive(false);
            PokeHand.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DirectHand.SetActive(true);
            PokeHand.SetActive(false);
        }
    }
}
