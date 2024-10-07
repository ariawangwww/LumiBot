using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform originalParent;

    public string itemType; // ��Ʒ����

    void Start()
    {
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        transform.SetParent(transform.root); // ����Ʒ�Ӹ��������Ƴ����Ա������϶�
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
        // ���û�з�����ȷ�Ŀ��У�����ԭλ
        transform.position = startPosition;
        transform.SetParent(originalParent);
    }
}
