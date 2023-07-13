using Matflo.Common.Audio;
using Matflo.Common.Health;
using Matflo.Common.Score;
using Matflo.Home.Scripts.Home;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace Matflo.Putaway.Scripts.DragAndDropCanvasCamera
{
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
            HealthManager.Instance.UpdateHealth(1);
            if (HealthManager.Instance.GetHealth() <= 0)
            {
                LevelFail.Instance.BringIn();
            }
        }
        public void UpdateIsDrop()
        {
            isDrop = true;
            UpdateScore();
            if (LevelPanel.Instance.levelName == LevelName.Receiving)
            {
                if (id == 1)
                    ReceivingNarrator.Instance.BringInNarrator(ReceivingNarrator.Instance.NOIScreen, AudioName.OIScreenReceiving, ReceivingNarrator.Instance.spriteOIScreen, () => PutawayGameManager.Instance.UpdateDragedCounter());
                else if (id == 2)
                    ReceivingNarrator.Instance.BringInNarrator(ReceivingNarrator.Instance.NSelectDelivery, AudioName.SelectDeliveryReceiving, ReceivingNarrator.Instance.spriteSelectDelivery, () => PutawayGameManager.Instance.UpdateDragedCounter());
                else if (id == 3)
                    ReceivingNarrator.Instance.BringInNarrator(ReceivingNarrator.Instance.NScanSKU, AudioName.ScanSKUReceiving, ReceivingNarrator.Instance.spriteScanSKU, () => PutawayGameManager.Instance.UpdateDragedCounter());
                else if (id == 4)
                    ReceivingNarrator.Instance.BringInNarrator(ReceivingNarrator.Instance.NScanTM, AudioName.ScanTMReceiving, ReceivingNarrator.Instance.spriteScanTM, () => PutawayGameManager.Instance.UpdateDragedCounter());
                else if (id == 5)
                    ReceivingNarrator.Instance.BringInNarrator(ReceivingNarrator.Instance.NEnterQuantity, AudioName.EnterQuantityReceiving, ReceivingNarrator.Instance.spriteEnterQuantity, () => PutawayGameManager.Instance.UpdateDragedCounter());
                else if (id == 6)
                    ReceivingNarrator.Instance.BringInNarrator(ReceivingNarrator.Instance.NPutTM, AudioName.PutTMReceiving, ReceivingNarrator.Instance.spritePutTM, () => PutawayGameManager.Instance.UpdateDragedCounter());
                else if (id == 7)
                    ReceivingNarrator.Instance.BringInNarrator(ReceivingNarrator.Instance.NClosingDelivery, AudioName.ClosingDeliveryReceiving, ReceivingNarrator.Instance.spriteClosingDelivery, () => PutawayGameManager.Instance.UpdateDragedCounter());
            }
            if (LevelPanel.Instance.levelName == LevelName.Putaway)
            {
                if (id == 1)
                    PutawayNarrator.Instance.BringInNarrator(PutawayNarrator.Instance.NOIScreen, AudioName.OIScreen, PutawayNarrator.Instance.spriteOIScreen, () => PutawayGameManager.Instance.UpdateDragedCounter());
                else if (id == 2)
                    PutawayNarrator.Instance.BringInNarrator(PutawayNarrator.Instance.NPickingLocation, AudioName.PickingLocation, PutawayNarrator.Instance.spritePickingLocation, () => PutawayGameManager.Instance.UpdateDragedCounter());
                else if (id == 3)
                    PutawayNarrator.Instance.BringInNarrator(PutawayNarrator.Instance.NScanTM, AudioName.ScanTM, PutawayNarrator.Instance.spriteScanTM, () => PutawayGameManager.Instance.UpdateDragedCounter());
                else if (id == 4)
                    PutawayNarrator.Instance.BringInNarrator(PutawayNarrator.Instance.NScanSKU, AudioName.ScanSKU, PutawayNarrator.Instance.spriteScanSKU, () => PutawayGameManager.Instance.UpdateDragedCounter());
                else if (id == 5)
                    PutawayNarrator.Instance.BringInNarrator(PutawayNarrator.Instance.NPutawayLocation, AudioName.PutawayLocation, PutawayNarrator.Instance.spritePutawayLocation, () => PutawayGameManager.Instance.UpdateDragedCounter());
                else if (id == 6)
                    PutawayNarrator.Instance.BringInNarrator(PutawayNarrator.Instance.NScanLocation, AudioName.ScanLocation, PutawayNarrator.Instance.spriteScanLocation, () => PutawayGameManager.Instance.UpdateDragedCounter());
                else if (id == 7)
                    PutawayNarrator.Instance.BringInNarrator(PutawayNarrator.Instance.NPutTM, AudioName.PutTM, PutawayNarrator.Instance.spritePutTM, () => PutawayGameManager.Instance.UpdateDragedCounter());
            }
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
}
