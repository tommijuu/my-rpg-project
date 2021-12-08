using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    private TargetingSystem _targetingSystem;

    private Rigidbody rb;

    public float animationSpeed = 2f;

    [SerializeField]
    private float speed;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _targetingSystem = GameObject.FindGameObjectWithTag("GameController").GetComponent<TargetingSystem>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!_targetingSystem.untargeted && _player.currentTarget != null)
        {
            Vector3 dir = _player.currentTarget.position - transform.position;

            rb.velocity = dir.normalized * speed;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //StartCoroutine(SpellHitAnimation());
        }
    }

    //Destroy the ball on impact
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    //private IEnumerator SpellHitAnimation()
    //{
    //    yield return new WaitForSeconds(animationSpeed);
    //    Destroy(gameObject);
    //}
}
