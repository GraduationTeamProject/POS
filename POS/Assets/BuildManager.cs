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
        //레이어마스크 지정
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
   

            OriginalPrefab.transform.position = hitPosition;

         

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
                            OriginalPrefab.GetComponent<BoxCollider>().enabled = true;
                            OriginalPrefab.transform.position = hitPosition;
                            
                            OriginalPrefab = null;
                            MaxDistance = 5f;
                        }


                    }

                }
                else if (hitInfo.collider == null)
                {
                    //텍스처 빨간색(설치불가)
                    Debug.Log("Layer가 9가아님.");
                }
            }
        }
    }

}
