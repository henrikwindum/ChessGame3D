using UnityEngine;
using UnityEngine.EventSystems;

public class ChessPiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Color color;

    public Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
    }
}