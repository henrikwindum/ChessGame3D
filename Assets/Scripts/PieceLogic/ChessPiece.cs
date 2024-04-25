using UnityEngine;
using UnityEngine.EventSystems;

public class ChessPiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public PieceType Type {  get; private set; }
    public PlayerColor Color { get; private set; }

    public Transform parentAfterDrag;

    public void InitializeChessPiece(PieceType type, PlayerColor color)
    {
        Type = type;
        Color = color;
    }

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

public enum PieceType
{
    Pawn, Rook, Knight, Bishop, Queen, King
}

