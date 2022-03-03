using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCHostileHuman : Interactable, IEnemy
{
    //Each hostile enemy types will have their unique stats, yet to be implemented
    //Currently the script just moves the "Human", actually a box, randomly in a set range

    //private NavMeshAgent _navMeshAgent;

    //private NavMeshPath _navPath;

    //private Vector3 _movePoint;

    //private bool _inCoroutine;

    //private bool _validPath;

    public enum State
    {
        Roaming,
        Chasing,
        Attacking
    }

    public State state;

    public bool isGrounded, navMeshSet;

    public TargetingSystem targetingSystem;

    public int ID { get; set; }

    public float moveTimer = 4f;
    public float movementSpeed;

    public GameObject target;

    public bool inCombat;

    public float aggroRange, attackingRange;

    //public State currentState;

    //public int minMoveRange = -2;
    //public int maxMoveRange = 2;
    void Awake()
    {
        state = State.Roaming;
    }
    void Start()
    {
        //navMeshSet = false;

        isGrounded = false;
        ID = 0; //Human type's id is 0

    }
    void Update()
    {
        //if (isGrounded && !navMeshSet)
        //{
        //    SetNavMeshStuff();
        //}

        //if (!_inCoroutine/* && navMeshSet*/)
        //{
        //    StartCoroutine(Move());
        //}
        //RunStateMachine();
        switch (state)
        {
            case State.Roaming:
                if (moveTimer > 0)
                {
                    transform.Translate(Vector3.forward * movementSpeed);
                    moveTimer -= Time.deltaTime;
                }
                else
                {
                    Wander();
                }
                SearchForTarget();
                break;
            case State.Chasing:
                FollowTarget();
                break;
            case State.Attacking:
                Attack();
                break;
        }

        //if (target == null)
        //{
        //    SearchForTarget();
        //}
        //else
        //{
        //    if (Vector3.Distance(target.transform.position, transform.position) <= aggroRange)
        //        state = State.Chasing;
        //}
    }


    //private void RunStateMachine()
    //{
    //    State nextState = currentState?.RunCurrentState(); //? means that if the current state is not null run it, if it is, ignore it

    //    if (nextState != null)
    //    {
    //        SwitchToNextState(nextState);
    //    }
    //}

    //private void SwitchToNextState(State nextState)
    //{
    //    currentState = nextState;
    //}

    private void Wander()
    {
        moveTimer = 4f; //reset timer
        transform.eulerAngles = new Vector3(0, UnityEngine.Random.Range(0, 360), 0);
    }

    private void SearchForTarget()
    {
        //Detecting player via sphere cast
        Vector3 center = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Collider[] hitColliders = Physics.OverlapSphere(center, aggroRange);
        int i = 0;

        while (i < hitColliders.Length)
        {
            if (hitColliders[i].transform.CompareTag("Player")) //player found within aggro range, change to chasing
            {
                target = hitColliders[i].transform.gameObject;
                state = State.Chasing;
                break; //Player found, no need to search for more for now
            }
            i++;
        }
    }

    void OnDrawGizmos() //to show the aggro range of the enemy
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

    private void FollowTarget()
    {
        Vector3 targetPos = target.transform.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);

        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance > attackingRange) //player not yet within attacking range, so move
        {
            transform.Translate(Vector3.forward * movementSpeed);
        }
        else //if player is in attacking range, change state to attacking
        {
            state = State.Attacking;
        }

        if (Vector3.Distance(target.transform.position, transform.position) > aggroRange) //if player not in aggro range, change state to roaming
        {
            //TODO: Return to spawn point here first
            state = State.Roaming;
        }
    }

    private void Attack()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance > attackingRange) //if player not in melee range, change state to chasing
        {
            state = State.Chasing;
        }
        else
        {
            Debug.Log("Enemy is within attack range and ATTACKING");
        }
    }

    //public void SetNavMeshStuff()
    //{
    //    _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    //    _navPath = new NavMeshPath();

    //    navMeshSet = true;
    //}

    //private IEnumerator Move()
    //{
    //    _inCoroutine = true;
    //    yield return new WaitForSeconds(moveTimer);

    //    GetNewPath();

    //    _validPath = _navMeshAgent.CalculatePath(_movePoint, _navPath); //check that the gotten path is valid

    //    while (!_validPath) //if not, get a new one
    //    {
    //        Debug.Log("Not a valid path. Getting a new path.");
    //        yield return new WaitForSeconds(0.01f);
    //        GetNewPath();
    //        _validPath = _navMeshAgent.CalculatePath(_movePoint, _navPath);
    //    }
    //    _inCoroutine = false;
    //}

    //private void GetNewPath()
    //{
    //    _movePoint = GetNewPos();
    //    _navMeshAgent.SetDestination(_movePoint);
    //}

    //private Vector3 GetNewPos()
    //{
    //    float x = Random.Range(minMoveRange, maxMoveRange);
    //    float z = Random.Range(minMoveRange, maxMoveRange);

    //    Vector3 pos = gameObject.transform.position + new Vector3(x, 0, z);
    //    return pos;
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    //The functions below are to help with tooltips etc in the future
    public void OnLeftClick()
    {

    }

    public void OnRightClickDown()
    {

    }

    public void OnHoverEnter()
    {
        Debug.Log("Hovering enemy");
        targetingSystem.isHoveringNPC = true;
    }

    public void OnHoverExit()
    {
        Debug.Log("Stopped hovering enemy");
        targetingSystem.isHoveringNPC = false;
    }
}
