using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(LayerMask.GetMask("Clickable"));
        //ClickTarget();
    }

    //Moved to Player.cs

    //public void ClickTarget()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //        if (Physics.Raycast(ray, out hit) && hit.collider != null)
    //        {
    //            if (hit.transform.CompareTag("Enemy"))
    //            {
    //                player.myTarget = hit.transform;
    //                player.targetRenderer = player.myTarget.GetComponent<Renderer>();
    //                player.targetRenderer.material.color = Color.red;
    //            }
    //            else
    //            {
    //                player.myTarget = null;
    //                player.targetRenderer.material.color = Color.yellow;
    //            }
    //        }
    //    }
    //}
}
