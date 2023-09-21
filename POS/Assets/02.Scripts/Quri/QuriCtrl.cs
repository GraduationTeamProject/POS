using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class QuriCtrl : MonoBehaviour
{
    private NavMeshAgent Nav;
    private Transform Player;
    public Animator Anim;

    void Start()
    {
        Nav = GetComponent<NavMeshAgent>();
         Player = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        Nav.SetDestination(Player.position);
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Nav.remainingDistance>=2)
        {
            Anim.SetFloat("MoveSpeed", 0.5f);
            Nav.SetDestination(Player.position);
            //transform.LookAt(Player.position - new Vector3(0, 0.25f, 0));
        }
         
        else
        {
            Anim.SetFloat("MoveSpeed", 0f);
            Nav.SetDestination(Player.position);
        }
       
    }
}
