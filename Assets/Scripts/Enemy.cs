using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    public GameObject player;
    [Header("Sight Values")]
    public float eyeHeight;
    public float sightDistance = 20f;
    public float fieldOfView = 85f;

    public StateMachine stateMachine;

    [SerializeField] private string currentState;

    [Header("Weapon Values")]
    public Transform gun;
    [Range(0.1f, 10f)] public float fireRate;

    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        IsCanSee();
        currentState = stateMachine.activeState.ToString();
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (health <= 0)
        {
            Destroy(gameObject);
            MoneyManager.instance.AddMoney(1000);
        }
    }

    public bool IsCanSee()
    {
        if(player != null)
        {
            if(Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);

                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    }

                }
            }
        }

        return false;
    }

    
}
