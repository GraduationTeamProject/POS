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

    //y위치
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
        /*현 코드의 문제점:
         내가 원하는 바: raycast로 레이어가 9번인 물체(바닥)만 검출하고싶어서 
        Physics.Raycast(RightController.transform.position, RightController.transform.forward, out RaycastHit hitInfo, 10,9))
        라고 적었는데 검출이안된다. 맨 뒤 파라미터인 9를 지우고 모든 콜라이더를 검출해서 콜라이더의 레이어값을  추출하니까 Default(0)이나 
        Installable(8) 나옴.
        바닥(Layer:9)번이 검출이안되는상태. 왜그러지?/
        
         예상 : 1) ray끝에 오브젝트가 달려있어서 오브젝트가 먼저검출된다.*/
        layerMask = 1 << LayerMask.NameToLayer("Map");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(RightController.transform.position, RightController.transform.forward * MaxDistance, Color.green, 0.3f);

        //태그가 Installable인 오브젝트를 넘겨받았을때만.
        if (OriginalPrefab != null)
        {

            OriginalPrefab.GetComponent<InstallableObject>().PreViewMaterial();
            //Debug.DrawRay(OriginalPrefab.transform.position, OriginalPrefab.transform.up * (-10f), Color.green, 0.3f);
            //MaxDistance 조절

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
            //위치
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
                   
                    //텍스처 초록색(설치가능)
                    Layer = hitInfo.collider.gameObject.layer;
                    hitPosition = hitInfo.point;

                    
                    Debug.Log("hitPosition:" + hitPosition);

                    if (controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool press))
                    {
                        if (press == true)
                        {
                            //설치 이펙트
                            OriginalPrefab.GetComponent<InstallableObject>().OrigianlMaterial();
                            //위치고정과 초기화
                            OriginalPrefab.transform.position = hitPosition;
                            this.gameObject.GetComponent<BoxCollider>().enabled = true;
                            OriginalPrefab = null;
                            MaxDistance = 5f;
                        }


                    }

                }
                else if(hitInfo.collider==null)
                {
                    //텍스처 빨간색(설치불가)
                    Debug.Log("Layer가 9가아님.");
                }
            }
        }
    }

}
