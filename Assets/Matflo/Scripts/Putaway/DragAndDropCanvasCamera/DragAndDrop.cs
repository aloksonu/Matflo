using Audio.Matflo;
using Ui.ScoreSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

public class DragAndDrop : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    private RectTransform rt;
    private CanvasGroup cg;
    public Canvas canvas;
    public int id;
    private Vector2 initialPos;
    private bool isDrop;
    
    void Start()
    {
        isDrop = false;
        rt = GetComponent<RectTransform>();
        cg = GetComponent<CanvasGroup>();
        //initialPos = transform.position;
        this.Invoke(() => SetInitialPosition(), 0.2f);
    }

    private void SetInitialPosition()
    {
        initialPos = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        cg.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        //rt.anchoredPosition += eventData.delta;
        rt.anchoredPosition += eventData.delta/canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        cg.blocksRaycasts = true;
        if(isDrop == false)
        transform.position = initialPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("On Pointer Down");
    }

    public void ResetPosition()
    {
        transform.position = initialPos;
      }
    public void UpdateHealth()
    {
        //HealthManager.Instance.UpdateHealth(1);
        //if (HealthManager.Instance.GetHealth() <= 0)
        //{
        //    LevelFail.Instance.BringIn();
        //}
    }
    public void UpdateIsDrop() {
        isDrop = true;
        UpdateScore();
        if(id==1)
            PutawayNarrator.Instance.BringInNarrator(PutawayNarrator.Instance.NOIScreen, AudioName.OIScreen, PutawayNarrator.Instance.spriteOIScreen, () => PutawayGameManager.Instance.UpdateDragedCounter());
        else if (id == 2)
            PutawayNarrator.Instance.BringInNarrator(PutawayNarrator.Instance.NPickingLocation, AudioName.PickingLocation, PutawayNarrator.Instance.spritePickingLOcation, () => PutawayGameManager.Instance.UpdateDragedCounter());
        else if (id == 3)
            PutawayNarrator.Instance.BringInNarrator(PutawayNarrator.Instance.NScanTM, AudioName.ScanTM, PutawayNarrator.Instance.spriteScanTM, () => PutawayGameManager.Instance.UpdateDragedCounter());
        else if (id == 4)
            PutawayNarrator.Instance.BringInNarrator(PutawayNarrator.Instance.NScanSKU, AudioName.ScanSKU, PutawayNarrator.Instance.spriteScanSKU, () => PutawayGameManager.Instance.UpdateDragedCounter());
    }

    public void UpdateScore()
    {
        ScoreManager.Instance.UpdateScore(10, 10);
    }

    public void ResetIsDrop()
    {
        isDrop = false;
    }
}
