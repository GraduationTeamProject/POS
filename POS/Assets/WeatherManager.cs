using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isRain;
    private float RotationTime;
    public GameObject Rain;
    void Start()
    {
        RotationTime = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRain)
        {
            Rain.SetActive(true);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(-6f, -30, 0), RotationTime * Time.deltaTime);
        }
        else
        {
            Rain.SetActive(false);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(50f, -30, 0), RotationTime * Time.deltaTime);
        }
    }

   
}
