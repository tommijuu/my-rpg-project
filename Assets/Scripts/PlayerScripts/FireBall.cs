using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    //This script is attached to the FireBall prefab, and works when the prefab is instantiated in PlayerCombatController.cs

    [SerializeField]
    private PlayerCombatController _playerCombatController;
    private TargetingSystem _targetingSystem;

    private Rigidbody _rb;

    //public float animationSpeed = 2f;

    [SerializeField]
    private float _speed;

    private void Awake()
    {
        _playerCombatController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>();
        _targetingSystem = GameObject.FindGameObjectWithTag("GameController").GetComponent<TargetingSystem>();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!_targetingSystem.untargeted && _playerCombatController.currentTarget != null)
        {
            Vector3 dir = _playerCombatController.currentTarget.position - transform.position; //set ball's the direction

            _rb.velocity = dir.normalized * _speed; //set ball's velocity

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; //set ball's the angle

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
}
