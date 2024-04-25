using UnityEngine.EventSystems;
using UnityEngine;

public class ChessBoardCell : MonoBehaviour, IDropHandler
{
    public ChessPiece chessPiece;
    public void OnDrop(PointerEventData eventData)
    {
        ChessPiece chessPiece = eventData.pointerDrag.GetComponent<ChessPiece>();
        chessPiece.parentAfterDrag = transform;
    }
}