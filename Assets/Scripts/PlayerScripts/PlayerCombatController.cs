using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatController : MonoBehaviour
{
    //private FireBall _fireBall;

    private Coroutine _attackRoutine;

    private RaycastHit _hit;

    public Transform currentTarget;

    public Transform lastTarget;

    public GameObject[] spellPrefab;

    public Transform castPoint;

    public Renderer targetRenderer;

    public Text spellCastTimerText;
    public TargetingSystem targetingSystem;

    public EnemyStats enemyStats;

    public bool isAttacking = false;

    public bool finishedCasting = false;

    public bool spellReachedEnemy = false;

    public float castTime = 2.0f; //just a hardcoded cast time for testing purposes as I have just one spell at the moment
    public float currentCastTime = 0.0f;

    public float fireBallDmg = 20f; //set to 20 just to test things out currently

    private void Start()
    {
        targetingSystem = GameObject.FindGameObjectWithTag("GameController").GetComponent<TargetingSystem>();
        targetingSystem.playerCombatController = this;
        spellCastTimerText = GameObject.Find("CastTimerText").GetComponent<Text>();
        currentTarget = null;
        lastTarget = null;
        targetRenderer = null;
        isAttacking = false;
        finishedCasting = false;
    }

    void Update()
    {
        if (currentTarget != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (currentTarget.CompareTag("HostileNPC") && !isAttacking)
                {
                    _attackRoutine = StartCoroutine(Attack());
                }
            }
        }


        if (isAttacking)
        {
            //Showing the current cast time in seconds and milliseconds in relation to the required quest time

            currentCastTime += Time.deltaTime;
            int seconds = (int)(currentCastTime % 60);
            int milliseconds = (int)(currentCastTime * 100f) % 100;

            if (seconds >= 10) //format to show two numbers for seconds
                spellCastTimerText.text = seconds.ToString("D2") + "." + milliseconds.ToString("D2") + "/" + castTime.ToString();
            else //don't show the 0 in front of the one number seconds
                spellCastTimerText.text = seconds.ToString("D1") + "." + milliseconds.ToString("D2") + "/" + castTime.ToString();
        }

        if (spellReachedEnemy)
        {
            spellReachedEnemy = false;
            if (currentTarget != null || lastTarget != null)
            {
                enemyStats.ReceiveDamage(fireBallDmg);
            }
        }
    }

    public IEnumerator Attack() //Currently implemented to just cast a fireball
    {
        isAttacking = true;
        finishedCasting = false;

        yield return new WaitForSeconds(castTime); // Hardcoded cast time for testing purposes

        CastSpell();

        StopAttack();
    }

    public void StopAttack()
    {
        if (_attackRoutine != null)
        {
            isAttacking = false;
            currentCastTime = 0;
            spellCastTimerText.text = "";

            StopCoroutine(_attackRoutine);
        }
    }

    public void CastSpell() //Instantiates a fireball prefab and FireBall.cs takes over, launching it towards the target
    {
        if (isAttacking)
            Instantiate(spellPrefab[0], castPoint.transform.position, Quaternion.identity);

        finishedCasting = true;
    }

    private bool InLineOfSight() //not in action yet
    {
        return false;
    }
}
