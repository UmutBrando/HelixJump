using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject splitPrefab;
    
    
    public int combo;

    public float maxspeed;
    public float bounceForce = 400f;
    
    
    public bool ComboBuff;
    public bool fireEffectBool;
    public bool allowDestroyingAssets;
 
    
    public ParticleSystem firework;
    public ParticleSystem fireEffect;

    Rigidbody rb;
    
   

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxspeed);
        if (combo >= 3)
        {
            ComboBuff = true;
        }
        if (ComboBuff == true && !fireEffectBool) 
        {
            fireEffect.Play();
            fireEffectBool = true;
        }  
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (ComboBuff == true && GameManager.levelWin == false)
        {
            if (other.transform.parent!=null && other.transform.parent.TryGetComponent<Ring>(out Ring ring))
            {
                ring.PassRings();
                firework.Play();
                return;
            }
            fireEffectBool = false;
            fireEffect.Stop();
        }

        string materialName = other.transform.GetComponent<MeshRenderer>().material.name;

        if (materialName == "Red (Instance)" && !ComboBuff)// GAME OVER
        {
            GameManager.gameOver = true;
        }
        
        if (!GameManager.levelWin)
        {
            rb.velocity = new Vector3(rb.velocity.x, bounceForce * Time.deltaTime, rb.velocity.z);
            GameObject newsplit = Instantiate(splitPrefab, new Vector3(transform.position.x, other.transform.position.y + 0.19f, transform.position.z), transform.rotation);
            newsplit.transform.localScale = Vector3.one * Random.Range(0.8f, 1.2f);
            newsplit.transform.parent = other.transform;
            combo = 0;
            ComboBuff = false;

        }

        if (materialName == "LastRing (Instance)") // WIN
        {
            GameManager.levelWin = true;

        }


      


    }
}
