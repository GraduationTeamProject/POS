using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class BuildManager : MonoBehaviour
{
    //Single Tone
    public static BuildManager Instance;
    public GameObject OriginalPrefab;

    //Instantiate Object;

    //about Controller
    public GameObject RightController;
    private XRController controller;

    int layerMask;

    [Range(2, 8)] public float MaxDistance;

    //y��ġ
    private float yPos;
    int Layer;
    Vector3 hitPosition;
    private void Awake()
    {
        Instance = this;
        controller = RightController.GetComponent<XRController>();
    }
    void Start()
    {
        //���̾��ũ ����
        layerMask = 1 << LayerMask.NameToLayer("Map");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(RightController.transform.position, RightController.transform.forward * MaxDistance, Color.green, 0.3f);

        //�±װ� Installable�� ������Ʈ�� �Ѱܹ޾�������.
        if (OriginalPrefab != null)
        {
            OriginalPrefab.GetComponent<InstallableObject>().PreViewMaterial();
            //Debug.DrawRay(OriginalPrefab.transform.position, OriginalPrefab.transform.up * (-10f), Color.green, 0.3f);
            //MaxDistance ����
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position))
            {
                if (position.y > 0)
                {
                    if (MaxDistance < 8f)
                        MaxDistance += 0.1f;
                }

                else if (position.y < 0)
                {
                    if (MaxDistance > 2f)
                        MaxDistance -= 0.1f;
                }

            }

            Debug.Log("Layer:" + Layer);
   

            OriginalPrefab.transform.position = hitPosition;

         

            if (Physics.Raycast(RightController.transform.position, RightController.transform.forward, out RaycastHit hitInfo, MaxDistance, layerMask))
            {
                if (hitInfo.collider != null)
                {

                    //�ؽ�ó �ʷϻ�(��ġ����)
                    Layer = hitInfo.collider.gameObject.layer;
                    hitPosition = hitInfo.point;


                    Debug.Log("hitPosition:" + hitPosition);

                    if (controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool press))
                    {
                        if (press == true)
                        {
                            //��ġ ����Ʈ
                            OriginalPrefab.GetComponent<InstallableObject>().OrigianlMaterial();
                            //��ġ������ �ʱ�ȭ
                            OriginalPrefab.GetComponent<BoxCollider>().enabled = true;
                            OriginalPrefab.transform.position = hitPosition;
                            
                            OriginalPrefab = null;
                            MaxDistance = 5f;
                        }


                    }

                }
                else if (hitInfo.collider == null)
                {
                    //�ؽ�ó ������(��ġ�Ұ�)
                    Debug.Log("Layer�� 9���ƴ�.");
                }
            }
        }
    }

}
