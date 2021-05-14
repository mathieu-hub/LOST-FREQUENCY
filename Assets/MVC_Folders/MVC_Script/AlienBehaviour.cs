using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBehaviour : MonoBehaviour
{
    public static AlienBehaviour Instance;

    
    public GameObject alien01;
    [Range(0,10)]
    public float moveSpeed01;
    public bool isFirstJumpScared = false; 
    public Transform reachPoint;
    [Space(10)]
    public GameObject alien02;
    [Range(0, 10)]
    public float moveSpeed02;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private int waypointIndex = 0;
    public bool alienPoursuit = false;
    [Space(10)]
    public GameObject alien03;
    [SerializeField]
    private Transform target02;
    [SerializeField]
    private int waypointIndex02 = 0;
    public bool alienPoursuit02 = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        target = Waypoints.points[0];
        target02 = Waypoints02.points[0];
    }

    void Update()
    {
        if (isFirstJumpScared)
        {
            FirstJumpScared();
        }

        if (alienPoursuit)
        {
            AlienPoursuit();
        }

        if (alienPoursuit02)
        {
            AlienPoursuit02();
        }
    }

    void FirstJumpScared()
    {       
        Vector3 dir = reachPoint.position - alien01.transform.position;
        alien01.transform.Translate(dir.normalized * moveSpeed01 * Time.deltaTime, Space.World);

        if (Vector3.Distance(alien01.transform.position, reachPoint.position) <= 0.2)
        {
            moveSpeed01 = 0f;
            alien01.SetActive(false);
            GameManager.Instance.lightChandelier.enabled = false;
            isFirstJumpScared = false;
        }
    }

    void AlienPoursuit()
    {
        Vector3 dir = target.position - alien02.transform.position;
        alien02.transform.Translate(dir.normalized * moveSpeed02 * Time.deltaTime, Space.World);
        alien02.transform.LookAt(target);


        if (Vector3.Distance(alien02.transform.position, target.position) <= 0.2)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            alienPoursuit = false;
            Destroy(alien02);
            return;
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    void AlienPoursuit02()
    {
        Vector3 dir = target02.position - alien03.transform.position;
        alien03.transform.Translate(dir.normalized * moveSpeed02 * Time.deltaTime, Space.World);
        alien03.transform.LookAt(target02);


        if (Vector3.Distance(alien03.transform.position, target02.position) <= 0.2)
        {
            GetNextWaypoint02();
        }
    }

    private void GetNextWaypoint02()
    {
        if (waypointIndex02 >= Waypoints02.points.Length - 1)
        {
            alienPoursuit02 = false;
            Destroy(alien03);
            return;
        }

        waypointIndex02++;
        target02 = Waypoints02.points[waypointIndex02];
    }
}
