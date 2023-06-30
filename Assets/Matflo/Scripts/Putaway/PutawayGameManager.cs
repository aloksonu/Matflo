using Audio.Matflo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PutawayGameManager : MonoSingleton<PutawayGameManager>
{
    [SerializeField] GameObject[] dragObjects;
    [SerializeField] private Animator animator;
    private static readonly int AnimIdle = Animator.StringToHash("Idle");
    private static readonly int AnimPutaway = Animator.StringToHash("Putaway");
    private List<Vector3> listPos = new List<Vector3>();
    private int dragCounter;
    void Start()
    {
        //GenericAudioManager.Instance.PlaySound(AudioName.CETinterface);
        dragCounter = 0;

        for (int i = 0; i < dragObjects.Length; i++)
        {
            listPos.Add(dragObjects[i].transform.position);
        }
        listPos.Shuffle();
        for (int i = 0; i < dragObjects.Length; i++)
        {
            dragObjects[i].transform.position = listPos[i];
        }

    }
    internal void ResetGame()
    {
        foreach (GameObject g in dragObjects)
        {
            g.GetComponent<DragAndDrop>().ResetPosition();
            g.GetComponent<DragAndDrop>().ResetIsDrop();
        }
        dragCounter = 0;
    }

    internal void UpdateDragedCounter()
    {
        dragCounter++;
        if (dragCounter >= dragObjects.Length)
        {
            //LevelComplete.Instance.BringIn(0f);
            StartCoroutine(OnClickPutawayButtonE());
        }
    }
    internal int GetDragedCounter()
    {
        return dragCounter;
    }

    //private void OnClickPutawayButton()
    //{
    //    StartCoroutine(OnClickPutawayButtonE());
    //}

    private IEnumerator OnClickPutawayButtonE()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetTrigger(AnimPutaway);
        yield return new WaitForSeconds(animator.GetAnimatorClipLength(AnimPutaway) + 0.2f);
        LevelComplete.Instance.BringIn(0f);
    }
}
