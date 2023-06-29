using Audio.Matflo;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour,IDropHandler
{
    public int id;

    void Start()
    {

    }
    public void OnDrop(PointerEventData eventData)
    {
       if(eventData.pointerDrag != null)
        {
            if(eventData.pointerDrag.GetComponent<DragAndDrop>().id == id) {
                GenericAudioManager.Instance.PlaySound(AudioName.Correct);
                eventData.pointerDrag.GetComponent<DragAndDrop>().UpdateIsDrop();
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
               //Debug.Log("Correct");
            }
            else
            {
                GenericAudioManager.Instance.PlaySound(AudioName.Wrong);
                eventData.pointerDrag.GetComponent<DragAndDrop>().ResetPosition();
                eventData.pointerDrag.GetComponent<DragAndDrop>().UpdateHealth();
                //Debug.Log("Wrong");
            }
        }
    }

    //private void UpdateHealth()
    //{
    //    HealthManager.Instance.UpdateHealth(1);
    //    if (HealthManager.Instance.GetHealth() <= 0)
    //    {
    //        LevelFail.Instance.BringIn();
    //    }
    //}
}
