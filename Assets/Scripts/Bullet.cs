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
        Destroy(gameObject,0.01f);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    collisions++;
    //    if (collision.collider.CompareTag("Ground"))
    //    {
    //        Debug.Log("WOW");
    //    }
    //    Invoke("DestroyBullet", 0f);
    //}
}
