using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChessBoard : MonoBehaviour
{ 
    private const int height = 8;
    private const int width = 8;

    public GameObject cellPrefab;

    [Header("Board Material")]
    public Material boardMaterialWhite;
    public Material boardMaterialBlack;

    [Header("Piece Material")]
    public Material pieceMaterialWhite;
    public Material pieceMaterialBlack;

    [Header("Piece Prefabs")]
    public GameObject[] piecePrefabs; // 0-5 for white, 6-11 for black (Pawn, Rook, Knight, Bishop, Queen, King)

    public ChessBoardCell[,] cells = new ChessBoardCell[height, width];

    private Dictionary<PlayerColor, HashSet<ChessPiece>> chessPiecesByColor = new Dictionary<PlayerColor, HashSet<ChessPiece>>();

    // Start is called before the first frame update
    void Start()
    {
        chessPiecesByColor[PlayerColor.White] = new HashSet<ChessPiece> ();
        chessPiecesByColor[PlayerColor.Black] = new HashSet<ChessPiece> ();
        CreateBoard();
    }

    private void CreateBoard()
    {
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                GameObject newCell = Instantiate(cellPrefab, new Vector3Int(x, 0, y), Quaternion.identity, transform);
                MeshRenderer cellRenderer = newCell.GetComponent<MeshRenderer>();
                bool isBlackCell = (x + y) % 2 == 0;
                cellRenderer.material = isBlackCell ? boardMaterialBlack : boardMaterialWhite;

                cells[x, y] = newCell.GetComponent<ChessBoardCell>();
            }
        }
        SetupPieces();
    }

    private void SetupPieces()
    {
        for (int x = 0; x < width; x++)
        {
            PlacePiece(PieceType.Pawn, PlayerColor.White, x, 1);
            PlacePiece(PieceType.Pawn, PlayerColor.Black, x, 6);
        }

        PieceType[] lineup = { 
            PieceType.Rook, PieceType.Knight, PieceType.Bishop, PieceType.Queen, 
            PieceType.King, PieceType.Bishop, PieceType.Knight, PieceType.Rook };
        int[] piecePositions = { 0, 7 };
        foreach( var y in piecePositions )
        {
            PlayerColor color = y == 0 ? PlayerColor.White : PlayerColor.Black;
            for ( int x = 0; x < width; x++) 
            {
                PlacePiece(lineup[x], color, x, y);
            }
        }
    }

    private void PlacePiece(PieceType type, PlayerColor color, int x, int y)
    {
        int index = (int)type + (color == PlayerColor.White ? 0 : 6); // Calculate prefab index based on type and color 
        GameObject pieceObject = Instantiate(piecePrefabs[index], new Vector3(x, 0.01f, y), Quaternion.identity, cells[x, y].transform);
        pieceObject.transform.localScale = new Vector3(1, 100, 1);
        ChessPiece piece = pieceObject.GetComponent<ChessPiece>();
        piece.InitializeChessPiece(type, color);
        chessPiecesByColor[color].Add(piece);
        cells[x, y].chessPiece = piece;
    }
}

public enum PlayerColor
{
    Black, White
}