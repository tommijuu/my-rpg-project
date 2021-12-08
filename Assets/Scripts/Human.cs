using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : Interactable, IEnemy
{

    public float timer;

    private NavMeshAgent navMeshAgent;

    private NavMeshPath navPath;

    private Vector3 movePoint;

    private bool inCoroutine;

    private bool validPath;

    public CameraController cameraControl;

    public int ID { get; set; }

    void Start()
    {
        ID = 0;
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        navPath = new NavMeshPath();
    }
    void Update()
    {
        if (!inCoroutine)
        {
            StartCoroutine(DoSometing());
        }
    }

    private Vector3 GetNewPos()
    {
        float x = Random.Range(-2, 2);
        float z = Random.Range(-2, 2);

        Vector3 pos = gameObject.transform.position + new Vector3(x, 0, z);
        return pos;
    }

    private void getNewPath()
    {
        movePoint = GetNewPos();
        navMeshAgent.SetDestination(movePoint);
    }

    private IEnumerator DoSometing()
    {
        inCoroutine = true;
        yield return new WaitForSeconds(timer);
        getNewPath();
        validPath = navMeshAgent.CalculatePath(movePoint, navPath);

        while (!validPath)
        {
            yield return new WaitForSeconds(0.01f);
            getNewPath();
            validPath = navMeshAgent.CalculatePath(movePoint, navPath);
        }
        inCoroutine = false;
    }

    //not used
    private void GetNewPath()
    {
        movePoint = GetNewPos();
        navMeshAgent.SetDestination(movePoint);
    }

    public void OnLeftClick()
    {

    }

    public void OnRightClickDown()
    {

    }

    public void OnHoverEnter()
    {
        Debug.Log("Hovering enemy");
        cameraControl.isHovering = true;

    }

    public void OnHoverExit()
    {
        Debug.Log("Stopped hovering enemy");
        cameraControl.isHovering = false;
    }
}
