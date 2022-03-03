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
    public Animator outOfRangeAnimator, noTargetAnimator;
    public TargetingSystem targetingSystem;

    public EnemyStats enemyStats;

    public bool isAttacking = false;

    public bool finishedCasting = false;

    public bool spellReachedEnemy = false;

    public float castTime = 2.0f; //just a hardcoded cast time for testing purposes as I have just one spell at the moment
    public float currentCastTime = 0.0f;

    public float fireBallDmg = 20f; //set to 20 just to test things out currently

    public bool behindEnemy;
    public bool canSpellAttack;

    public float spellCastDistance = 40f;
    public float attackingAngle = 60f;

    public bool canAutoAttack;

    public float autoAttackDistance = 5f;
    public float autoAttackCooldown = 3f;
    public float autoAttackCurTime;

    public float autoAttackDmg = 10f;

    public LayerMask raycastLayers;
    public bool inLineOfSight;


    private void Start()
    {
        targetingSystem = GameObject.FindGameObjectWithTag("GameController").GetComponent<TargetingSystem>();
        targetingSystem.playerCombatController = this;
        spellCastTimerText = GameObject.Find("CastTimerText").GetComponent<Text>();
        outOfRangeAnimator = GameObject.Find("OutOfRangeText").GetComponent<Animator>();
        noTargetAnimator = GameObject.Find("NoTargetText").GetComponent<Animator>();
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
            //Attacking angle and distance stuff
            Vector3 toTarget = (currentTarget.transform.position - transform.position).normalized;
            if (Vector3.Dot(toTarget, currentTarget.transform.forward) < 0)
            {
                behindEnemy = false;
            }
            else
            {
                behindEnemy = true;
                //Some crit logic here if rogue for example
            }

            float distance = Vector3.Distance(this.transform.position, currentTarget.transform.position);
            Vector3 targetDir = currentTarget.transform.position - transform.position;
            Vector3 forward = transform.forward;
            float angle = Vector3.Angle(targetDir, forward);

            if (angle > attackingAngle)
            {
                canSpellAttack = false;
            }
            else
            {

                if (distance <= autoAttackDistance)
                {
                    canAutoAttack = true;
                }
                else
                {
                    canAutoAttack = false;
                }

                if (distance <= spellCastDistance)
                {
                    canSpellAttack = true;
                }
                else
                {
                    canSpellAttack = false;
                }

            }

            //Line of sight
            RaycastHit hit;
            if (Physics.Linecast(currentTarget.transform.position, transform.position, out hit, raycastLayers))
            {
                Debug.Log("LOS");
                inLineOfSight = false;
            }
            else
            {
                inLineOfSight = true;
            }

            //Auto attack
            if (currentTarget.CompareTag("HostileNPC") && !isAttacking && canAutoAttack && inLineOfSight)
            {
                if (autoAttackCurTime < autoAttackCooldown)
                {
                    autoAttackCurTime += Time.deltaTime;
                }
                else
                {
                    if (targetingSystem.rightClickedOrAttacking)
                    {
                        AutoAttack();
                        autoAttackCurTime = 0;
                    }
                }
            }


        }

        //When button 1 pressed and able to attack, attack
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            targetingSystem.rightClickedOrAttacking = true;
            if (currentTarget != null)
            {
                if (currentTarget.CompareTag("HostileNPC") && !isAttacking && canSpellAttack && inLineOfSight)
                {
                    _attackRoutine = StartCoroutine(SpellAttack());
                }

                if (!canSpellAttack)
                {
                    outOfRangeAnimator.SetTrigger("ShowErrorText");
                }
            }
            else
            {
                noTargetAnimator.SetTrigger("ShowErrorText");
            }
        }


        if (isAttacking)
        {
            //Showing the current cast time in seconds and milliseconds in relation to the required quest time

            if ((currentTarget == null) && (lastTarget == null)) //if target has died mid spell cast, stop casting
            {
                StopAttack();
            }
            else //there is a target
            {
                currentCastTime += Time.deltaTime;
                int seconds = (int)(currentCastTime % 60);
                int milliseconds = (int)(currentCastTime * 100f) % 100;

                if (seconds >= 10) //format to show two numbers for seconds
                    spellCastTimerText.text = seconds.ToString("D2") + "." + milliseconds.ToString("D2") + "/" + castTime.ToString();
                else //don't show the 0 in front of the one number seconds
                    spellCastTimerText.text = seconds.ToString("D1") + "." + milliseconds.ToString("D2") + "/" + castTime.ToString();
            }
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

    public void AutoAttack()
    {
        enemyStats.ReceiveDamage(autoAttackDmg);
    }

    public IEnumerator SpellAttack() //Currently implemented to just cast a fireball
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
        if (isAttacking && inLineOfSight)
        {
            Instantiate(spellPrefab[0], castPoint.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Target not in line of sight.");
        }

        finishedCasting = true;
    }

    private bool InLineOfSight() //not in action yet
    {
        return false;
    }
}
