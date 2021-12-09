using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetingSystem : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    private RaycastHit hit;

    private IEnemy _previousClickable;

    public bool untargeted = false;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Mouse hovering

        //Checking if mouse is over UI
        if (IsPointerOverUIObject())
        {
            Debug.Log("Clicked UI");
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            IEnemy clickable = hit.transform.GetComponent<IEnemy>();

            if (clickable != _previousClickable && !Input.GetMouseButton(0)) //The latter check is so that you cannot free look while press-hovering enemy (maybe changing later?)
            {
                if (clickable != null)
                    clickable.OnHoverEnter();

                if (_previousClickable != null)
                    _previousClickable.OnHoverExit();

                _previousClickable = clickable;
            }
        }

        //Targeting enemy when clicked
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit) && hit.collider != null)
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    _player.currentTarget = hit.transform;
                    _player.targetRenderer = _player.currentTarget.GetComponent<Renderer>();
                    untargeted = false;
                    _player.targetRenderer.material.color = Color.red;
                }
                if (hit.transform.CompareTag("Interactable"))
                {
                    hit.transform.GetComponent<Interactable>().CheckDistance(_player.transform);
                }
            }
        }

        //Untarget using Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_player.isAttacking)
                _player.StopAttack();

            untargeted = true;
            _player.currentTarget = null;
            _player.targetRenderer.material.color = Color.yellow;
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
