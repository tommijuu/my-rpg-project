using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCHostileHuman : Interactable, IEnemy
{
    //Each hostile enemy types will have their unique stats, yet to be implemented
    //Currently the script just moves the "Human", actually a box, randomly in a set range

    private NavMeshAgent _navMeshAgent;

    private NavMeshPath _navPath;

    private Vector3 _movePoint;

    private bool _inCoroutine;

    private bool _validPath;

    public TargetingSystem targetingSystem;

    public int ID { get; set; }

    public float moveTimer = 4f;

    public int minMoveRange = -2;
    public int maxMoveRange = 2;

    void Start()
    {
        ID = 0; //Human type's id is 0
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        _navPath = new NavMeshPath();
    }
    void Update()
    {
        if (!_inCoroutine)
        {
            StartCoroutine(Move());
        }
    }

    private IEnumerator Move()
    {
        _inCoroutine = true;
        yield return new WaitForSeconds(moveTimer);

        GetNewPath();

        _validPath = _navMeshAgent.CalculatePath(_movePoint, _navPath); //check that the gotten path is valid

        while (!_validPath) //if not, get a new one
        {
            Debug.Log("Not a valid path. Getting a new path.");
            yield return new WaitForSeconds(0.01f);
            GetNewPath();
            _validPath = _navMeshAgent.CalculatePath(_movePoint, _navPath);
        }
        _inCoroutine = false;
    }

    private void GetNewPath()
    {
        _movePoint = GetNewPos();
        _navMeshAgent.SetDestination(_movePoint);
    }

    private Vector3 GetNewPos()
    {
        float x = Random.Range(minMoveRange, maxMoveRange);
        float z = Random.Range(minMoveRange, maxMoveRange);

        Vector3 pos = gameObject.transform.position + new Vector3(x, 0, z);
        return pos;
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
