using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Apple;

public class PatrollingEnemy : MonoBehaviour, IStompable
{

    private Rigidbody rB;   


    //patrol mode variables
    public Transform[] patrolPoints;

    private NavMeshAgent enemyAgent;
    public Transform target;
    private Animator enemyAnimator;
    private int targetPoint;

    public GameObject player;

    //enemy sound variables
    AudioSource audioSource;
    public AudioClip footstepsWalk;
    public AudioClip stomped;
    private bool walking;





    void Start()
    {
        rB = GetComponent<Rigidbody>();
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        targetPoint = Random.Range(0, 6);
        enemyAgent.speed = 2.5f;
        audioSource = GetComponent<AudioSource>();

        enemyAgent.SetDestination(patrolPoints[targetPoint].position);
        audioSource.loop = true;
        walking = true;
    }

    void Update ()
    {
        
        //keep track of sound states
        soundSpeedController();
        //once timer hits zero, set chaseOn true and call chasemode

        if (Vector3.Distance(enemyAgent.transform.position, patrolPoints[targetPoint].position) < 0.5f)
        {
            Debug.Log("Target hit, changing");
            targetPoint = changeTargetInt();
            SetPosition();
        }

    }

    //functions from iStompable
    void IStompable.Die()
    {
        Destroy(gameObject);
    }

    void IStompable.OnStomped()
    {
        //instantiate vfx (when we add that in)
        //PlaySoundOnce(stomped);
    }

    void soundSpeedController()
    {
        float noiseTimer = 0.0f;

        if (walking && noiseTimer <= 0)
        {
            PlaySoundOnce(footstepsWalk);
            noiseTimer = 2.0f;
            noiseTimer -= Time.deltaTime;
        }
    }

    void SetPosition()
    {
        enemyAgent.SetDestination(patrolPoints[targetPoint].position);
    }
    int changeTargetInt()
    {
        int newVal = Random.Range(0,6);
        return newVal;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            enemyAgent.isStopped = true;
        }
    }

   
    public void PlaySoundOnce(AudioClip clip)
    {
        //audioSource.PlayOneShot(clip);
    }
}


