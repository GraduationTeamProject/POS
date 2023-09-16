using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item Item;
    private MeshRenderer Mesh;

    public bool Start = false;

    private void Update()
    {
        if(Start)
        {
            Start = false;
            StartCoroutine(SpawnMeshRenderer());
        }
    }

    public IEnumerator SpawnMeshRenderer()
    {
        Debug.Log("Ω¶¿Ã¥ıΩ««‡");
        Rigidbody rigid = this.gameObject.GetComponent<Rigidbody>();
        Mesh = this.gameObject.GetComponent<MeshRenderer>();

        rigid.isKinematic = true;
        float f = 1f;
        Mesh.material.SetVector("_DissolveOffest", new Vector3(0, f, 0));
        //float materials = Mesh.material.parent.GetVector("_DissolveOffset").y;

        while (f > -1f)
        {
           
            yield return new WaitForSeconds(0.003f);

            f -= 0.01f;

            Mesh.material.SetVector("_DissolveOffest", new Vector3(0, f, 0));
          

        }
        rigid.isKinematic = false;
        
    }
}
