using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;

    //Lifetime
    public float maxLifetime;
    public LayerMask whatIsEnemy;
    public int Damage;

    //Count collisions
    public int maxCollisions;
    int collisions;

    public bool useGravity;

    [Range(0f,1f)]
    public float bouncyness;

    PhysicMaterial physics_mat;

    [Header("Particle")]
    [SerializeField] private GameObject particleBullet;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Setup();
    }


    void Setup()
    {
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bouncyness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;

        GetComponent<SphereCollider>().material = physics_mat;

        rb.useGravity = useGravity;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject,maxLifetime);
    }

    void DestroyBullet()
    {
        particleBullet.SetActive(true);
        Destroy(gameObject,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        collisions++;
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy e = other.gameObject.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(Damage);
                Invoke("DestroyBullet", 0f);
            }
        }

        else if (other.gameObject.CompareTag("Ground"))
        {
            Invoke("DestroyBullet", 0f);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if(player != null)
            {
                player.TakeDamage(Damage);
                Invoke("DestroyBullet", 0f);
            }
        }
    }
}
