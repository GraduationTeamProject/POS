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
        /*�� �ڵ��� ������:
         ���� ���ϴ� ��: raycast�� ���̾ 9���� ��ü(�ٴ�)�� �����ϰ�; 
        Physics.Raycast(RightController.transform.position, RightController.transform.forward, out RaycastHit hitInfo, 10,9))
        ��� �����µ� �����̾ȵȴ�. �� �� �Ķ������ 9�� ����� ��� �ݶ��̴��� �����ؼ� �ݶ��̴��� ���̾��  �����ϴϱ� Default(0)�̳� 
        Installable(8) ����.
        �ٴ�(Layer:9)���� �����̾ȵǴ»���. �ֱ׷���?/
        
         ���� : 1) ray���� ������Ʈ�� �޷��־ ������Ʈ�� ��������ȴ�.*/
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
            //��ġ
            //OriginalPrefab.transform.position = new Vector3(
            //    RightController.transform.position.x + RightController.transform.forward.x * MaxDistance,
            //    yPos,
            //    RightController.transform.position.z + RightController.transform.forward.z * MaxDistance);

            OriginalPrefab.transform.position = hitPosition;

            //if (Physics.Raycast(OriginalPrefab.transform.position, OriginalPrefab.transform.up*(-1),out RaycastHit hit,10,9))
            //{
            //    yPos = hit.point.y;
            //}

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
                            OriginalPrefab.transform.position = hitPosition;
                            this.gameObject.GetComponent<BoxCollider>().enabled = true;
                            OriginalPrefab = null;
                            MaxDistance = 5f;
                        }


                    }

                }
                else if(hitInfo.collider==null)
                {
                    //�ؽ�ó ������(��ġ�Ұ�)
                    Debug.Log("Layer�� 9���ƴ�.");
                }
            }
        }
    }

}
