using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class AIManager : MonoBehaviour
    {

		public GameObject Arrow; 
        //public NavMeshAgent navMeshAgent;
        private GameObject[] teamMembers;
        private GameObject[] cover;
        private GameObject[] interactables;
        private GameObject commander;

        // Use this for initialization
        void Start()
        {
            //  navMeshAgent = GetComponent<NavMeshAgent>();
            cover = GameObject.FindGameObjectsWithTag("Cover");
            commander = GameObject.FindGameObjectWithTag("Player");
            teamMembers = GameObject.FindGameObjectsWithTag("TeamMate");
            interactables = GameObject.FindGameObjectsWithTag("Interactable");
			float displacement = 0; 
			foreach (GameObject g in teamMembers) 
			{
				GameObject x = Instantiate (Arrow, new Vector3 ((Arrow.GetComponent<RectTransform>().rect.width/2) + displacement, (Arrow.GetComponent<RectTransform>().rect.width/2), 0), Quaternion.identity, GameObject.Find ("Canvas").transform);
				displacement += x.GetComponent<RectTransform> ().rect.width; 
				x.GetComponent<ArrowPointer> ().target = g; 
			}
        }

        // Update is called once per frame
        void Update()
        {
            //Set all cover to unoccupied
            foreach (GameObject i in cover)
            {
                i.GetComponent<CoverBehaviour>().SetOccupied(false);
            }
            //Loop through every team member and set cover to occupied after assigning a squad member to it
            for (int x = 0; x < teamMembers.Length; x++)
            {
                foreach (GameObject i in cover)
                {
                    if (i.GetComponent<CoverBehaviour>().GetTakeCover() == true &&
                        i.GetComponent<CoverBehaviour>().GetOccupied() == false)
                    {
                        teamMembers[x].GetComponent<AIController>().SetCoverLocation(i.transform.position);
                        i.GetComponent<CoverBehaviour>().SetOccupied(true);
                        break;
                    }
                }
            }

            CheckInteractables(); 
        }

        void CheckInteractables()
        {
            foreach (GameObject i in interactables)
            {
                if (i.GetComponent<CoverBehaviour>().GetTakeCover() == true)
                {
                    FindClosestTeamMemberTo(i).GetComponent<AIController>().SetChosenToInteract(true); 
                }
            }
        }

        GameObject FindClosestTeamMemberTo(GameObject interactable)
        {
            //Find closest team mate to interactable
            int closestTeamMate = 0;
			for (int i = 0; i < teamMembers.Length; i++)
            {
                // if the distance between this team member and the interactable is smaller than the distance of the closest team mate 
				if (Vector3.Distance(teamMembers[i].transform.position, interactable.transform.position) < 
                    Vector3.Distance(teamMembers[closestTeamMate].transform.position, interactable.transform.position))
                    //Change this to closest team mate 
                    closestTeamMate = i;
            }
            Debug.Log("Closest team member is member " + closestTeamMate.ToString());
            return teamMembers[closestTeamMate];
        }
    }
}