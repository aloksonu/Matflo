using System.Collections;
using System.Collections.Generic;
using Matflo.Home.Scripts.Home;
using Matflo.Putaway.Scripts.DragAndDropCanvasCamera;
using UnityEngine;
using Utilities;

namespace Matflo.Putaway.Scripts
{
    public class PutawayGameManager : MonoSingleton<PutawayGameManager>
    {
        [SerializeField] GameObject[] dragObjects;
        [SerializeField] GameObject[] dropObjects;
        [SerializeField] private Animator animator;
        [SerializeField] PutawayPrerequisites putawayPrerequisites;
        [SerializeField] private CanvasGroup _cgPutawayTutorial;
        private static readonly int AnimIdle = Animator.StringToHash("Idle");
        private static readonly int AnimPutaway = Animator.StringToHash("Putaway");
        private List<Vector3> listPos = new List<Vector3>();
        private int dragCounter;

        internal int _wrongAttempt;
        void Start()
        {
            //GenericAudioManager.Instance.PlaySound(AudioName.CETinterface);
            dragCounter = 0;
            _wrongAttempt = 0;

            for (int i = 0; i < dragObjects.Length; i++)
            {
                listPos.Add(dragObjects[i].transform.position);
            }
            listPos.Shuffle();
            for (int i = 0; i < dragObjects.Length; i++)
            {
                dragObjects[i].transform.position = listPos[i];
            }

            StartPutawayPrerequisites();
            ShowDropObjects();

        }
        internal void ResetGame()
        {
            foreach (GameObject g in dragObjects)
            {
                g.GetComponent<DragAndDrop>().ResetPosition();
                g.GetComponent<DragAndDrop>().ResetIsDrop();
            }
            dragCounter = 0;
           _wrongAttempt = 0;
        }

        internal void UpdateWrongAttempt()
        {
            _wrongAttempt++;
        }

        private void StartPutawayPrerequisites()
        {
            animator.transform.gameObject.SetActive(false);
            putawayPrerequisites.BringIn(ShowAnimCharacter);
        }

        private void ShowAnimCharacter()
        {
            animator.transform.gameObject.SetActive(true);
        }


        internal void UpdateDragedCounter()
        {
            dragCounter++;
            if (dragCounter < 7)
                ShowDropObjects();
            Debug.Log("dragObjects.Length= " + dragObjects.Length);
            Debug.Log("dragCounter= " + dragCounter);
            if (dragCounter >= dragObjects.Length)
            {
                StartCoroutine(OnClickPutawayButtonE());
            }
            _cgPutawayTutorial.UpdateState(false, 0.1f);
            //if (dragCounter >= 7)
            //{
            //    StartCoroutine(OnClickPutawayButtonE());
            //}
        }
        internal int GetDragedCounter()
        {
            return dragCounter;
        }

        //private void OnClickPutawayButton()
        //{
        //    StartCoroutine(OnClickPutawayButtonE());
        //}

        internal void ShowDropObjects()
        {
            foreach(GameObject g in dropObjects)
            {
                g.SetActive(false);
            }
            dropObjects[dragCounter].SetActive(true);
        }

        private IEnumerator OnClickPutawayButtonE()
        {
            yield return new WaitForSeconds(0.2f);
            animator.SetTrigger(AnimPutaway);
            yield return new WaitForSeconds(animator.GetAnimatorClipLength(AnimPutaway) + 1f);
            LevelComplete.Instance.BringIn(0f);
        }
    }
}
