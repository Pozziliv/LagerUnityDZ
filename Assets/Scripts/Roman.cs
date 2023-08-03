using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Roman : MonoBehaviour, IEventable
{
    private NavMeshAgent agent;
    [SerializeField] private Transform player;

    private bool isDestinating = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(isDestinating)
            agent.destination = player.position;
    }

    private void StopDestination()
    {
        agent.destination -= player.position;
        isDestinating = false;
    }

    public void OnEnable()
    {
        VideoCollecting.OnAllVideo += StopDestination;
    }

    public void OnDisable()
    {
        VideoCollecting.OnAllVideo -= StopDestination;
    }
}
