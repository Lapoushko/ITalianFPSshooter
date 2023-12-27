using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public float moveTimer;
    private float losePlayerTimer;
    private float shotTimer;

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
        if (enemy.IsCanSee())
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.player.transform);

            if (shotTimer > enemy.fireRate)
            {
                Shoot();
            }

            if (moveTimer > Random.Range(1, 5))
            {
                //Движение босса
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 10));

                moveTimer = 0;
            }
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 8)
            {
                //Состояния поиска игрока
                stateMachine.ChangeState(new AttackState());
            }
        }
    }

    public void Shoot()
    {
        Transform gun = enemy.gun;

        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gun.position, enemy.transform.rotation);

        Vector3 shootDirection = (enemy.player.transform.position - gun.transform.position).normalized;

        bullet.GetComponent<Rigidbody>().velocity = 
            Quaternion.AngleAxis(Random.Range(-3f,3f),Vector3.up) * shootDirection * 50;
        shotTimer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
