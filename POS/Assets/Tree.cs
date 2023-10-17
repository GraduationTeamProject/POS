using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Logs;
    public Transform InstanPosition;
    public GameObject SparkEffect;
    
    //private로 곧바꿀거임
    public float Hit;
    private bool isColliding = false;
    private Collider LaserCollider;

    void Start()
    {
        Hit = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Hit == 3)
        {
            // Perform the tree falling action
            this.gameObject.GetComponent<Rigidbody>().mass = 10;
            this.gameObject.transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                Quaternion.Euler(new Vector3(0, 0, -80f)),
                Time.deltaTime);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Laser")
        //{
        //    if (Hit < 3)
        //    {
        //        ++Hit;
        //        GameObject Log = Instantiate(Logs, InstanPosition.position, Quaternion.identity);
        //        Log.GetComponent<Rigidbody>().AddForce(transform.up * 500f);
        //        Log.GetComponent<Rigidbody>().AddForce(transform.right * (20f));
        //    }

        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Laser") && !isColliding)
        {
            isColliding = true;
            LaserCollider = other;
            StartCoroutine("HitTree");
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        isColliding = false;
    }

    private IEnumerator HitTree()
    {
        if (Hit < 3)
        {
            if (isColliding)
            {
                // 1초마다 증가
                yield return new WaitForSeconds(1f);

                Hit++;

                // Create the log object&&Hit Object
                if(Hit<3)
                {
                    //충돌위치(Slash effect)
                    Vector3 cp = LaserCollider.bounds.ClosestPoint(transform.position);
                    GameObject SparkInstan = Instantiate(SparkEffect, cp, Quaternion.identity);
                    SparkInstan.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));

                    Destroy(SparkInstan, 2f);

                    GameObject Log = Instantiate(Logs, InstanPosition.position, Quaternion.identity);
                    Log.GetComponent<Rigidbody>().AddForce(transform.up * Random.Range(300f,500f));
                    Log.GetComponent<Rigidbody>().AddForce(transform.right * Random.Range(-50f,50f));
                }
               
                isColliding = false;
            }
        }

        
    }
}
