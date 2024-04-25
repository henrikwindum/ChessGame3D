using UnityEngine.EventSystems;
using UnityEngine;

public class ChessBoardCell : MonoBehaviour, IDropHandler
{
    public ChessPiece chessPiece;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            ChessPiece draggedChessPiece = eventData.pointerDrag.GetComponent<ChessPiece>();
            if (draggedChessPiece != null && chessPiece == null)
            {
                draggedChessPiece.parentAfterDrag = transform;
                chessPiece = draggedChessPiece;
            }
        }
    }
}