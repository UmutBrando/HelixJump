using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    
    public GameObject[] childRings;
    private Transform player;
    private GameManager GameManager;
    private Ball ballScript;
    

    float radius = 100f;
    float force = 500f;
    
    
    private void Start()
    {
        ballScript = GameObject.Find("Ball").GetComponent<Ball>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    private void Update()
    {
        if (transform.position.y > player.position.y + 0.1f)
        {
            PassRings();   
        }
    }

    public void PassRings()
    {
        GameManager.noOfPassingRings++;
        ballScript.combo++;
        for (int i = 0; i < childRings.Length; i++)
        {
            childRings[i].GetComponent<Rigidbody>().isKinematic = false;
            childRings[i].GetComponent<Rigidbody>().useGravity = true;

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider newCollider in colliders)
            {
                Rigidbody rb = newCollider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                }
            }
            childRings[i].transform.parent = null;
            Destroy(childRings[i].gameObject, 1f);
            GameManager.UpdateScore();
        }
        this.enabled = false;
    }
}
