using UnityEngine;
using UnityEngine.EventSystems;

public class ChessPiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public PieceType Type {  get; private set; }
    public PlayerColor Color { get; private set; }
    public GameObject GameObject { get; private set; }

    public Transform parentAfterDrag;

    public ChessPiece(PieceType type, PlayerColor color, GameObject gameObject)
    {
        Type = type;
        Color = color;
        GameObject = gameObject;
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

