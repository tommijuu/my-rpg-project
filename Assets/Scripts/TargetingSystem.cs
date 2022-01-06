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

    //void Start()
    //{
    //    playerCombatController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>();
    //}
    private void Start()
    {
        untargeted = false;
        isHoveringNPC = false;
    }

    void Update()
    {
        if (playerCombatController != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Mouse hovering

            //Checking if mouse is over UI, used in the future for example to click spells (they can be used through keyboard bindings too of course)
            if (IsPointerOverUIObject())
            {
                Debug.Log("Hovering UI");
            }

            //Targeting an NPC when clicked
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit) && hit.collider != null)
                {
                    if (hit.transform.CompareTag("HostileNPC"))
                    {
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (playerCombatController.isAttacking)
                    playerCombatController.StopAttack();

                playerCombatController.lastTarget = playerCombatController.currentTarget;
                untargeted = true;

                playerCombatController.currentTarget = null;
                playerCombatController.targetRenderer.material.color = Color.yellow;
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

    //Returns true if a UI element is hovered
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
