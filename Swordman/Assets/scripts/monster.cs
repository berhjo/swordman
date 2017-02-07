using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class monster : MonoBehaviour
{
    public GameObject player;
    public AudioClip[] footsounds;
    public Transform eyes;

    private UnityEngine.AI.NavMeshAgent nav;
    private AudioSource sound;
    private Animator anim;
    private string state = "idle";
    private bool alive = true;

	// Use this for initialization
	void Start ()
    {
        //navigation for the monster
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //footstep for the monster
        sound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        nav.speed = 1.2f;
        anim.speed = 1.2f;
	}
    //sound for footstep
    public void footstep(int _num)
    {
        sound.clip = footsounds[_num];
        sound.Play();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.DrawLine(eyes.position, player.transform.position, Color.green);
        if (alive)
        {

            anim.SetFloat("velocity", nav.velocity.magnitude);

            //idle
            if(state == "idle")
            {
                //pick a random place to walk to//
                Vector3 randomPos = Random.insideUnitSphere * 20f;
                NavMeshHit navHit;
                NavMesh.SamplePosition(transform.position + randomPos, out navHit, 20f, NavMesh.AllAreas);
                nav.SetDestination(navHit.position);
                state = "walk";
            }

            //walk//
            if(state == "walk")
            {
                if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
                {
                    state = "idle";
                }
            }

            //walk to player
            //nav.SetDestination(player.transform.position);
        }
	}
}
