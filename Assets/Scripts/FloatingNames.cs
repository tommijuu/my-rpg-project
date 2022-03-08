using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingNames : MonoBehaviour
{
    private GameObject parentObject;

    void Start()
    {
        parentObject = gameObject.transform.parent.gameObject; //for future to set up name automatically etc.

        gameObject.GetComponent<TextMesh>().text = parentObject.GetComponent<EnemyStats>().enemyName;
        //Debug.Log(parentObject.GetComponent<EnemyStats>().enemyName);
    }

    void Update()
    {
        if (Camera.main != null)
        {
            gameObject.transform.LookAt(Camera.main.transform.position);
            gameObject.transform.Rotate(0, 180, 0); //to face towards player
        }
    }
}
