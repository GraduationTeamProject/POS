using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuriInventory : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isOpen;
    private bool isRotating = false;
    private bool isTriggerEnter;
    private Quaternion InitRotation;
    private Quaternion TargetRotation;
    private float rotationSpeed = 2f;
    void Start()
    {
        InitRotation = Quaternion.Euler(0.0f, 0.0f,0.0f);
        TargetRotation = Quaternion.Euler(-90f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {

        if (isRotating)
        {
            // 회전 중에만 보간
            transform.localRotation = Quaternion.Lerp(transform.localRotation, isOpen ? TargetRotation : InitRotation, rotationSpeed * Time.deltaTime);

        }
        else if(isRotating&&!isOpen)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, InitRotation, rotationSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("충돌!");

            // 회전 중이 아닐 때만 isOpen 토글
            if (!isRotating)
            {
                isOpen = !isOpen;
                isRotating = true; // 회전 시작
            }
            else if(isRotating)
            {
                isOpen = !isOpen;
               
            }
        }
    }

}
