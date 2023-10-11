using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InstallableObject : MonoBehaviour
{
    // Start is called before the first frame update

    public Material[] OriginalMaterial;
    public Material PriviewMaterial;
    public MeshRenderer[] MeshRenderer;


    void Start()
    {
        
        PriviewMaterial = Resources.Load<Material>("HoloGram");
        MeshRenderer = GetComponentsInChildren<MeshRenderer>();
        OriginalMaterial = new Material[MeshRenderer.Length];

        for (int i = 0; i < MeshRenderer.Length; i++)
        {
            OriginalMaterial[i] = MeshRenderer[i].material;
        }

    }

    // Update is called once per frame
    void Update()
    {
       
    }

  
    public void PreViewMaterial()
    {
        Debug.Log("MeshReneder.Length:" + MeshRenderer.Length);
        MeshRenderer = GetComponentsInChildren<MeshRenderer>();
        for (int i=0;i<this.MeshRenderer.Length;i++)
        {
           
            MeshRenderer[i].material = PriviewMaterial;
        }
            
    }

    public void OrigianlMaterial()
    {
        MeshRenderer = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < this.MeshRenderer.Length; i++)
            MeshRenderer[i].material = OriginalMaterial[i];
    }
}
