using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text spellCastTimerText;

    public bool isAttacking = false;

    public bool finishedCasting = false;

    public float castTime = 2.0f; //just a hardcoded cast time for testing purposes for the first spell
    public float currentCastTime = 0.0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (currentTarget.CompareTag("Enemy") && !isAttacking)
            {
                attackRoutine = StartCoroutine(Attack());
            }
        }

        if (isAttacking)
        {
            currentCastTime += Time.deltaTime;
            int seconds = (int)(currentCastTime % 60);
            int milliseconds = (int)(currentCastTime * 100f) % 100;

            if (seconds >= 10)
                spellCastTimerText.text = seconds.ToString("D2") + "." + milliseconds.ToString("D2") + "/" + castTime.ToString();
            else
                spellCastTimerText.text = seconds.ToString("D1") + "." + milliseconds.ToString("D2") + "/" + castTime.ToString();
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
            currentCastTime = 0;
            spellCastTimerText.text = "";

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
