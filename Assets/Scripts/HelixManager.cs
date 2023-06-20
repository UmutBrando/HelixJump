using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixManager : MonoBehaviour
{
    public GameObject[] rings;

    public int noOfRings;
    public float ringDistance = 5f;
    float yPos;

    private void Start()
    {
        noOfRings = GameManager.currentLevelIndex + 5;
        for(int i=0; i<noOfRings; i++)
        {
            if (i == 0)
            {
                //spawn the first ring
                SpawnRings(0);
            }
            else
            {
                //spawn the middle ring
                SpawnRings(Random.Range(1, rings.Length - 1));
            }
        }
        SpawnRings(rings.Length - 1);
    }
    void SpawnRings(int index)
    {
        GameObject newRing = Instantiate(rings[index], new Vector3(transform.position.x, yPos, transform.position.z), Quaternion.identity);
        yPos -= ringDistance;
        newRing.transform.parent = transform;
    }

}
