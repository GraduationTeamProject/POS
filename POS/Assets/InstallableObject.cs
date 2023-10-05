using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InstallableObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject RayInteractor;
    public GameObject DirectInteractor;
    public GameObject PokeInteractor;

    public Material OriginalMaterial;
    private Material PriviewMaterial;
    public MeshRenderer MeshRenderer;


    void Start()
    {
        PriviewMaterial = Resources.Load<Material>("Green");
        //RayInteractor = GameObject.Find("RightHand Ray controller");
        //DirectInteractor = GameObject.Find("RightHand Controller");
        //PokeInteractor = GameObject.Find("RightHand Poke controller");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
    public void PreViewMaterial()
    {
        MeshRenderer.material = PriviewMaterial;
    }

    public void OrigianlMaterial()
    {
        MeshRenderer.material = PriviewMaterial;
    }
}
