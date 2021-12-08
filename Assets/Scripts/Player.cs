using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private FireBall fireBall;

    private Coroutine attackRoutine;

    private RaycastHit hit;

    public Transform currentTarget;

    public Transform lastTarget;

    public GameObject[] spellPrefab;

    public Transform castPoint;

    public Renderer targetRenderer;

    public bool isAttacking = false;

    public bool finishedCasting = false;

    public float castTime = 2f; //just a hardcoded cast time for testing purposes for the first spell

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (currentTarget.tag == "Enemy" && !isAttacking)
            {
                attackRoutine = StartCoroutine(Attack());
            }
        }
    }

    public IEnumerator Attack()
    {
        isAttacking = true;
        //finishedCasting = false;

        yield return new WaitForSeconds(castTime); // Hardcoded cast time for testing purposes

        CastSpell();

        StopAttack();
    }

    public void StopAttack()
    {
        if (attackRoutine != null)
        {
            isAttacking = false;
            //finishedCasting = true;

            StopCoroutine(attackRoutine);
        }
    }

    public void CastSpell()
    {
        if (isAttacking)
            Instantiate(spellPrefab[0], castPoint.transform.position, Quaternion.identity);

        //finishedCasting = true;
    }

    private bool InLineOfSight() //not in action yet
    {
        return false;
    }
}
