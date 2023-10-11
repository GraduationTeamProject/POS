using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InstallableObject : MonoBehaviour
{
    // Start is called before the first frame update

    public Material[] OriginalMaterial;
    public Material PriviewMaterial;
    public MeshRenderer[] MeshRenderer;


    void Awake()
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

        StartCoroutine(InstallShader());
    }

    IEnumerator InstallShader()
    {
        
        Debug.Log("½¦ÀÌ´õ½ÇÇà");

       
       
       
        float f = 1f;
        for (int i = 0; i < this.MeshRenderer.Length; i++)
            MeshRenderer[i].material.SetVector("_DissolveOffest", new Vector3(0, f, 0));
        
        //float materials = Mesh.material.parent.GetVector("_DissolveOffset").y;

        while (f > -1f)
        {

            yield return new WaitForSeconds(0.003f);

            f -= 0.01f;

            for (int i = 0; i < this.MeshRenderer.Length; i++)
                MeshRenderer[i].material.SetVector("_DissolveOffest", new Vector3(0, f, 0));


        }

       
    }
}
