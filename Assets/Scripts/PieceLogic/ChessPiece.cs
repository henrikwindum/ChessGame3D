using UnityEngine;
using UnityEngine.EventSystems;

public class ChessPiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public PieceType Type {  get; private set; }
    public PlayerColor Color { get; private set; }

    public Transform parentAfterDrag;
    
    const float fixedY = 0.01f;

    float cameraDistance;

    public void InitializeChessPiece(PieceType type, PlayerColor color)
    {
        Type = type;
        Color = color;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);

        cameraDistance = Camera.main.transform.position.y - fixedY;

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance);
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);

        transform.position = new Vector3(worldPoint.x, fixedY, worldPoint.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        transform.localPosition = Vector3.zero;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}

public enum PieceType
{
    Pawn, Rook, Knight, Bishop, Queen, King
}

