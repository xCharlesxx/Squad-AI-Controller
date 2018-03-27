using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class AIController : MonoBehaviour
    {
        public NavMeshAgent navMeshAgent;
        private GameObject[] cover;
        private GameObject[] interactables; 
        private ThirdPersonCharacter character;
        private GameObject commander; 
        private Vector3 startPos;
        public bool jumpOnMe = false;
        public bool interaction = false;
        private bool chosenToInteract = false; 
        private Vector3 waitLocation;
        public Vector3 coverLocation;
        public Vector3 GetCoverLocation() { return coverLocation; }
        public void SetCoverLocation(Vector3 co) { coverLocation = co; }
        public bool GetChosenToInteract() { return chosenToInteract; }
        public void SetChosenToInteract(bool inter) { chosenToInteract = inter; } 

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            cover = GameObject.FindGameObjectsWithTag("Cover");
            commander = GameObject.FindGameObjectWithTag("Player"); 
            character = GetComponent<ThirdPersonCharacter>();
            startPos = new Vector3(-5, 0, 0);
            interactables = GameObject.FindGameObjectsWithTag("Interactable"); 
        }

        // Update is called once per frame
        void Update()
        {
            jumpOnMe = false; 
            //Check for interactable commands
            if (CheckForInteractOrder() == false)
                switch (commander.GetComponent<ThirdPersonUserControl>().getState())
                {
                    case ThirdPersonUserControl.State.FormUp:
                        FormUpState();
                        break;
                    case ThirdPersonUserControl.State.Advance:
                        AdvanceState();
                        break;
                    case ThirdPersonUserControl.State.None:
                        navMeshAgent.SetDestination(character.transform.position);
                        character.Move(Vector3.zero, true, false);
                        break;
                }
        }

        void FormUpState()
        {
            //Move towards player
            if (Vector3.Distance(commander.transform.position, character.transform.position) < 2)
            {
                character.Move(Vector3.zero, false, false);
                navMeshAgent.SetDestination(character.transform.position);
            }
            else
            {
                navMeshAgent.SetDestination(commander.transform.position);
                character.Move(navMeshAgent.desiredVelocity, false, false);
            }
        }
    
        void AdvanceState()
        {
            //Move to assigned cover
            if (Vector3.Distance(coverLocation, character.transform.position) < 1)
            {
                character.Move(Vector3.zero, true, false);
                navMeshAgent.SetDestination(character.transform.position);
                return;
            }
            navMeshAgent.SetDestination(coverLocation);
            character.Move(navMeshAgent.desiredVelocity, false, false);
        }

        void WaitState(float direction, bool jump)
        {
            //Make the AI stand when assisting jump
            if (interaction == true)
            {
                interaction = false;
                character.Move(Vector3.zero, !jump, false);
            }
            else
            character.Move(Vector3.zero, true, false);

            character.transform.position = waitLocation;
            character.transform.eulerAngles = new Vector3(0, direction, 0);
            jumpOnMe = jump; 
        }

        bool CheckForInteractOrder()
        {
            if (chosenToInteract == false)
                return false; 
            foreach (GameObject i in interactables)
            {
                if (i.GetComponent<CoverBehaviour>().GetTakeCover() == true)
                {
                    waitLocation = new Vector3(i.transform.position.x -0.5f, i.transform.position.y, i.transform.position.z); 
                    if (Vector3.Distance(i.transform.position, character.transform.position) < 1)
                    {
                        bool jump = true;
                        if (i.GetComponent<JumpBehaviour>() == null)
                            jump = false; 
                        WaitState(i.GetComponent<CoverBehaviour>().interactDirection, jump);
                        //If we are at switch
                        bool atSwitch; 
                        if (i.GetComponent<SwitchBehaviour>() != null)
                            atSwitch = true; 
                        else
                            atSwitch = false;
                        //Find platform
                        foreach (GameObject x in interactables)
                        {
                            if (x.GetComponent<MovingPlatBehaviour>() != null)
                                x.GetComponent<MovingPlatBehaviour>().activate = atSwitch;
                        }
                        return true; 
                    }
                    navMeshAgent.SetDestination(i.transform.position);
                    character.Move(navMeshAgent.desiredVelocity, false, false);
                    return true; 
                }
            }
            return false; 
        }
        //void PrioritiseCover(int x)
        //{
        //    for (int i = 0; i < cover.Length; i++)
        //    {
        //        if (i != x)
        //        {
        //            cover[i].GetComponent<CoverBehaviour>().takeCover = false; 
        //        }
        //    }

        //}
         
    }
}