using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class EnemyBehaviour : MonoBehaviour
    {
        enum State
        {
            Idle, 
            Chase,
            Patrol
        }
        // Use this for initialization
        ThirdPersonCharacter character;
        public NavMeshAgent navMeshAgent;

        GameObject ethanBody;
        GameObject ethanGlasses;
        GameObject chaseIntruder;
        GameObject[] wayPoints; 
        public Material Body;
        public Material Glasses;
        Renderer rend;
        List<GameObject> intruders = new List<GameObject>();
        
        State state;
        Vector3 startPos;
        Quaternion startRot;
        int currentWayPoint = 0; 

        void Start()
        {
            intruders.Add(GameObject.FindGameObjectWithTag("Player"));
            foreach (GameObject i in GameObject.FindGameObjectsWithTag("TeamMate"))
                intruders.Add(i);

            wayPoints = GameObject.FindGameObjectsWithTag("PatrolPoint"); 
            ethanBody = gameObject.transform.Find("EthanBody").gameObject;
            ethanGlasses = gameObject.transform.Find("EthanGlasses").gameObject;
            rend = ethanBody.GetComponent<Renderer>();
            rend.material = Body;
            rend = ethanGlasses.GetComponent<Renderer>();
            rend.material = Glasses;
            startPos = gameObject.transform.position;
            startRot = gameObject.transform.rotation; 
            navMeshAgent = GetComponent<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
        }

        // Update is called once per frame
        void Update()
        {
            if (state != State.Chase)
            foreach (GameObject g in intruders)
                if (Vector3.Distance(g.transform.position, gameObject.transform.position) < 4)
                {
                    chaseIntruder = g;
                    state = State.Chase;
                    break;
                }
            switch (state)
            {
                case State.Idle:
                    Idle(); 
                    break;
                case State.Chase:
                    Chase(); 
                    break;
                case State.Patrol:
                    Patrol(); 
                    break; 
            }
        }

        void Idle()
        {
            if (Vector3.Distance(gameObject.transform.position, startPos) < 1)
            {
                gameObject.transform.position = startPos;
                gameObject.transform.rotation = startRot;
                character.Move(Vector3.zero, false, false);
                navMeshAgent.SetDestination(startPos);
            }
            else
            {
                character.Move(navMeshAgent.desiredVelocity, false, false);
                navMeshAgent.SetDestination(startPos);
            }
        }
        void Chase()
        {
            if (Vector3.Distance(gameObject.transform.position, chaseIntruder.transform.position) < 8)
            {
                if (Vector3.Distance(gameObject.transform.position, chaseIntruder.transform.position) < 1)
                {
                    if (chaseIntruder == GameObject.FindGameObjectWithTag("Player"))
                        Application.LoadLevel(Application.loadedLevel); 
                    for (int i = 0; i < intruders.Count; i++)
                        if (intruders[i] == chaseIntruder)
                        {
                            chaseIntruder.SetActive(false);
                            intruders.RemoveAt(i);
                            chaseIntruder = null;
                            state = State.Patrol; 
                            break; 
                        }
                             
                }
                else
                {
                    character.Move(navMeshAgent.desiredVelocity, false, false);
                    navMeshAgent.SetDestination(chaseIntruder.transform.position);
                }

            }
            else
                state = State.Idle; 
        }
        void Patrol()
        {
            if (currentWayPoint > wayPoints.Length)
            {
                state = State.Idle;
                currentWayPoint = 0; 
                return; 
            }
            character.Move(navMeshAgent.desiredVelocity, false, false);
            navMeshAgent.SetDestination(wayPoints[currentWayPoint].transform.position);

            if (Vector3.Distance(gameObject.transform.position, wayPoints[currentWayPoint].transform.position) < 1)
                currentWayPoint++; 
        }
    }
}