using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBehaviour : MonoBehaviour
{
    public static AlienBehaviour Instance;

    public GameObject myAlien;
    [Range(0,10)]
    public float moveSpeed;
    public bool isFirstJumpScared = false; 
    public Transform reachPoint;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (isFirstJumpScared)
        {
            Vector3 dir = reachPoint.position - myAlien.transform.position;
            myAlien.transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);

            if (Vector3.Distance(myAlien.transform.position, reachPoint.position) <= 0.2)
            {
                moveSpeed = 0f;
                myAlien.SetActive(false);
            }
        }
    }
}
