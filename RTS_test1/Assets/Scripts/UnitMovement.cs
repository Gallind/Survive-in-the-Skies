using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    Camera cam;
    NavMeshAgent agent;
    public LayerMask ground;
    public bool isCommandToMove;
    DirectionIndicator directionIndicator;

    private void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        directionIndicator = GetComponent<DirectionIndicator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                isCommandToMove = true;
                agent.SetDestination(hit.point);
                directionIndicator.DrawLine(hit);
            }
        }

        if (!agent.hasPath || agent.remainingDistance <= agent.stoppingDistance)
        {
            isCommandToMove = false;
        }
        
    }
}
