using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Apple;

public class SeekingEnemy : MonoBehaviour
{
    private float turnTimer = 2.0f; //subject to change
    private float attackCD = 1.75f;
    private float rotationSpeed = 5.0f;

    private float moveSpeed = 3.5f;
    private Rigidbody rB;   

    //based upon the layer system in the editor

    public float detectionRadius = 5.0f;

    //patrol mode variables

    private NavMeshAgent enemyAgent;
    public Transform target;
    public LayerMask targetMask;
    private Animator enemyAnimator;
    private int targetPoint;

    public GameObject player;

    //enemy sound variables
    AudioSource audioSource;
    public AudioClip footstepsWalk;

    public bool pursuit;



    void Start()
    {
        rB = GetComponent<Rigidbody>();
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        enemyAgent.speed = 2.5f;
        audioSource = GetComponent<AudioSource>();

        audioSource.loop = true;
    }

    void Update ()
    {
        //check if player is in cone, if so start timer
        LookForPlayer();
        //keep track of sound states
        //soundSpeedController();
        //once timer hits zero, set chaseOn true and call chasemode
        if (pursuit)
        {
            enemyAgent.SetDestination(target.position);
        }
        else {
            rB.velocity = Vector3.forward * moveSpeed * Time.deltaTime;
            if (turnTimer < 0.0f)
            {
                Quaternion targetRotation = Quaternion.Euler(0, 90f * rotationSpeed * Time.deltaTime, 0);
                rB.MoveRotation(targetRotation * transform.rotation);
                turnTimer = 2.0f;
            }
        }
    }

    /*void soundSpeedController()
    {
        float noiseTimer = 0.0f;

        if (walking && noiseTimer <= 0)
        {
            PlaySoundOnce(footstepsWalk);
            noiseTimer = 2.0f;
            noiseTimer -= Time.deltaTime;
        }
    } */

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            attackCD -= Time.deltaTime;
            while(attackCD > 0)
            {
                enemyAgent.isStopped = true;
            }
            attackCD = 1.75f;
            enemyAgent.isStopped = false;
        }
    }

    private void LookForPlayer()
    {

        if (player == null)
        {
            return;
        }

        Vector3 enemyPosition = transform.position;

        if (Physics.CheckSphere(transform.position, detectionRadius, targetMask))
        {
            pursuit = true;
        }
    }

     

    public void PlaySoundOnce(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}


