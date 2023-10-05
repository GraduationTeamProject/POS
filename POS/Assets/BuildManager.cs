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


    [Range(2, 8)] public float MaxDistance;

    //y��ġ
    private float yPos;
    int Layer;
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
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(RightController.transform.position, RightController.transform.forward * MaxDistance, Color.green, 0.3f);

        //�±װ� Installable�� ������Ʈ�� �Ѱܹ޾�������.
        if (OriginalPrefab != null)
        {
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
            OriginalPrefab.transform.position = RightController.transform.position + RightController.transform.forward * MaxDistance;
            
            if (Physics.Raycast(RightController.transform.position, RightController.transform.forward, out RaycastHit hitInfo, 10))
            {
                if (hitInfo.transform != null)
                {
                    //�ؽ�ó �ʷϻ�(��ġ����)
                    Layer = hitInfo.collider.gameObject.layer;
                    Vector3 hitPosition = hitInfo.transform.position;
                    yPos = hitPosition.y;
                    Debug.Log("hitPosition:" + hitPosition);

                    if (controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool press))
                    {
                        if (press == true)
                        {
                            //��ġ ����Ʈ
                            //��ġ������ �ʱ�ȭ
                            OriginalPrefab.transform.position = hitPosition;
                            OriginalPrefab = null;
                            MaxDistance = 5f;
                        }


                    }

                }
                else
                {
                    //�ؽ�ó ������(��ġ�Ұ�)
                }
            }
        }
    }

}
