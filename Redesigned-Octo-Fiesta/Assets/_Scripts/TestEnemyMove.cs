﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemyMove : MonoBehaviour
{
    public float lookRadius = 10f;

    private Transform target;
    public Transform home;
    NavMeshAgent agent;

    public int health = 3;
    public AudioManager audioManager;
    RaycastHit hit;

    Vector3 rayDirection;

    public GameObject deathBubbles;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if(health <= 0)
        {
            Debug.Log("I died");
            audioManager.updateAudio("boom");
            GameObject bubbles = Instantiate(deathBubbles, transform.position, Quaternion.identity);
            Destroy(bubbles, 2);
            gameObject.SetActive(false);
        }

        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            rayDirection = (target.transform.position + new Vector3(0,1,0)) - (transform.position + new Vector3 (0,1,0));

            bool raycastdown = Physics.Raycast((transform.position + new Vector3(0, 1, 0)), rayDirection, out hit);
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), new Vector3(target.transform.position.x, transform.position.y + 1.5f, target.transform.position.z));
            if (raycastdown && hit.transform.name.Equals("OctopusPlayer Variant"))
            {
                agent.SetDestination(target.position);
                audioManager.updateAudio("attack");

                if (distance <= agent.stoppingDistance)
                {
                    FaceTarget();
                }
            }
        } else
        {
            agent.SetDestination(home.position);
            FaceTarget();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}