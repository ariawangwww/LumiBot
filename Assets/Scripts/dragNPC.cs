using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform originalParent;

    public string itemType; // 物品类型

    void Start()
    {
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        transform.SetParent(transform.root); // 将物品从父对象中移除，以便自由拖动
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject dropZone = eventData.pointerEnter;
        if (dropZone != null && dropZone.GetComponent<DropZone>() != null)
        {
            DropZone zone = dropZone.GetComponent<DropZone>();
            if (zone.zoneType == itemType)
            {
                transform.SetParent(zone.transform);
                transform.position = zone.transform.position;
                return;
            }
        }
        // 如果没有放入正确的框中，返回原位
        transform.position = startPosition;
        transform.SetParent(originalParent);
    }
}
