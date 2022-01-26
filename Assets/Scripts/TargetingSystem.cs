using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetingSystem : MonoBehaviour
{
    public PlayerCombatController playerCombatController;

    //private RaycastHit _hit;

    //private IEnemy _previousClickable;

    public bool untargeted = false;

    public bool isHoveringNPC;

    public bool rightClickedOrAttacking;

    public bool toolTipActive;
    public string hoverName;

    //void Start()
    //{
    //    playerCombatController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>();
    //}
    private void Start()
    {
        untargeted = false;
        isHoveringNPC = false;
        rightClickedOrAttacking = false;
    }

    private void OnGUI()
    {
        //Making an initial tooltip appear when toolTipActive is true
        if (toolTipActive)
            GUI.Label(new Rect(Input.mousePosition.x - 100, Screen.height - Input.mousePosition.y, 100, 50), "" + hoverName);
    }

    void Update()
    {
        if (playerCombatController != null)
        {
            ////Checking if mouse is over UI, used in the future for example to click spells (they can be used through keyboard bindings too of course)
            //if (IsPointerOverUIObject())
            //{
            //    Debug.Log("Hovering UI");
            //}

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);



            //Targeting an NPC when clicked
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(ray, out hit) && hit.collider != null)
                {
                    if (hit.transform.CompareTag("HostileNPC"))
                    {
                        if (Input.GetMouseButtonDown(1))
                        {
                            Debug.Log("Target right clicked");
                            rightClickedOrAttacking = true;
                        }

                        Debug.Log(hit.transform);
                        playerCombatController.currentTarget = hit.transform;
                        playerCombatController.enemyStats = playerCombatController.currentTarget.GetComponent<EnemyStats>(); //Search EnemyStats script for PlayerCombatController
                        playerCombatController.targetRenderer = playerCombatController.currentTarget.GetComponent<Renderer>();
                        untargeted = false;
                        playerCombatController.targetRenderer.material.color = Color.red;
                    }
                    if (hit.transform.CompareTag("Interactable")) //Interactable is a base class for NPCs and items
                    {
                        hit.transform.GetComponent<Interactable>().CheckDistance(playerCombatController.transform);
                    }
                }
            }

            //Untarget using Escape
            if (Input.GetKeyDown(KeyCode.Escape) && playerCombatController.currentTarget)
            {
                rightClickedOrAttacking = false;

                if (playerCombatController.isAttacking)
                    playerCombatController.StopAttack();

                playerCombatController.lastTarget = playerCombatController.currentTarget;
                untargeted = true;

                playerCombatController.currentTarget = null;
                playerCombatController.targetRenderer.material.color = Color.yellow;
            }


            //Tooltip logic
            Ray rayHover = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitHover;

            if (Physics.Raycast(rayHover, out hitHover))
            {
                if (hitHover.transform.CompareTag("HostileNPC")) //TODO: add friendly NPCs here as well
                {
                    toolTipActive = true;
                    hoverName = hitHover.transform.GetComponent<EnemyStats>().enemyName;
                }
                else
                {
                    toolTipActive = false;
                }
            }


            //else if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            //{
            //    IEnemy clickable = hit.transform.GetComponent<IEnemy>();

            //    if (clickable != _previousClickable && !Input.GetMouseButton(0)) //The latter check is so that you cannot free look while press-hovering enemy (maybe changing later?)
            //    {
            //        if (clickable != null)
            //            clickable.OnHoverEnter();

            //        if (_previousClickable != null)
            //            _previousClickable.OnHoverExit();

            //        _previousClickable = clickable;
            //    }
            //}
        }
    }

    ////Returns true if a UI element is hovered
    //private bool IsPointerOverUIObject()
    //{
    //    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
    //    eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //    List<RaycastResult> results = new List<RaycastResult>();
    //    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
    //    return results.Count > 0;
    //}
}
