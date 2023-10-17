using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LaserPistol : MonoBehaviour
{
    //controller
    public XRController controller;

    //Laser point
    private GameObject Laser;
    private int layerMask;
    private RaycastHit RayHit;

    //LineRender
    public LineRenderer lineRenderer;
    public BoxCollider boxCollider;
    public float LaserZPoint;
    

    //Ãæµ¹
    public bool isGrabbed;
    public GameObject SparkEffect;
    private bool isCollision;

    // Start is called before the first frame update
    void Start()
    {
        layerMask= layerMask = 1 << LayerMask.NameToLayer("Tree");
        Laser = GameObject.Find("Laser");
        controller = BuildManager.Instance.RightController.GetComponent<XRController>();
        LaserZPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {

        LaserBeam();
    }

    public void LaserBeam()
    {
        Debug.DrawRay(Laser.transform.position, Laser.transform.forward * (LaserZPoint), Color.green);
        lineRenderer.SetPosition(0, new Vector3(0, 0, LaserZPoint));
        boxCollider.size = new Vector3(0f, 0f, LaserZPoint);

        if (isGrabbed)
        {
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool press))
            {
                if (press)
                {
                    if (LaserZPoint <= 1)
                        LaserZPoint += 0.01f;
                }
                else
                {
                    LaserZPoint = 0f;
                }

            }

            if (Physics.Raycast(Laser.transform.position, Laser.transform.forward, out RayHit, LaserZPoint, layerMask))
            {
                if (RayHit.collider != null)
                {
                    StartCoroutine("InstanSpark");
                }
            }
        }
        else
        {
            LaserZPoint = 0;
        }
    }

    IEnumerator InstanSpark()
    {
        if(isCollision)
        {

            yield return new WaitForSeconds(2f);

            GameObject SparkInstan = Instantiate(SparkEffect, RayHit.point, Quaternion.identity);
            SparkInstan.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));

            Destroy(SparkInstan, 2f);

            yield return new WaitForSeconds(2f);
            isCollision = false;
        }
        

    }
    public void TrueGrabbed()
    {
        isGrabbed = true;
    }


    public void FalseGrabbed()
    {
        isGrabbed = false;
    }


}
