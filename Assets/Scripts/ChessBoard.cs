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

    [Header("White Pieces")]
    public GameObject whitePawnPrefab;
    public GameObject whiteRookPrefab;
    public GameObject whiteKnightPrefab;
    public GameObject whiteBishopPrefab;
    public GameObject whiteQueenPrefab;
    public GameObject whiteKingPrefab;

    [Header("Black Pieces")]
    public GameObject blackPawnPrefab;
    public GameObject blackRookPrefab;
    public GameObject blackKnightPrefab;
    public GameObject blackBishopPrefab;
    public GameObject blackQueenPrefab;
    public GameObject blackKingPrefab;

    public ChessBoardCell[,] cells = new ChessBoardCell[height, width];

    // Start is called before the first frame update
    void Start()
    {
        CreateBoard();
    }

    private void CreateBoard()
    {
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                GameObject newCell = Instantiate(cellPrefab, new Vector3Int(x, 0, y), Quaternion.identity, transform);

                bool isBlackCell = (x + y) % 2 == 0;

                MeshRenderer cellRenderer = newCell.GetComponent<MeshRenderer>();

                cellRenderer.material = isBlackCell ? boardMaterialBlack : boardMaterialWhite;

                cells[x, y] = new ChessBoardCell
                {
                    x = x,  
                    y = y,
                    cellPrefab = newCell
                };

                InstantiatePiece(x, y);
            }
        }
    }

    private void InstantiatePiece(int x, int y)
    {
        if (y == 1 || y == 6)
        {
            GameObject piecePrefab = y == 1 ? whitePawnPrefab : blackPawnPrefab;
            InstantiateAndPlacePiece(piecePrefab, x, y);
        }
        else if (y == 0 || y == 7)
        {
            GameObject piecePrefab = null;
            if (x == 0 || x == 7)
            {
                piecePrefab = y == 0 ? whiteRookPrefab : blackRookPrefab;
            }
            else if (x == 1 || x == 6)
            {
                piecePrefab = y == 0 ? whiteKnightPrefab : blackKnightPrefab;
            }
            else if (x == 2 || x == 5)
            {
                piecePrefab = y == 0 ? whiteBishopPrefab : blackBishopPrefab;
            }
            else if (x == 3)
            {
                piecePrefab = y == 0 ? whiteQueenPrefab : blackQueenPrefab;
            }
            else if (x == 4)
            {
                piecePrefab = y == 0 ? whiteKingPrefab : blackKingPrefab;
            }

            InstantiateAndPlacePiece(piecePrefab, x, y);
        }
    }

    private void InstantiateAndPlacePiece(GameObject prefab, int x, int y)
    {
        if (prefab != null)
        {
            GameObject newPiece = Instantiate(prefab, new Vector3((float)x, 0.01f, (float)y), Quaternion.identity, cells[x, y].cellPrefab.transform);
            newPiece.transform.localScale = new Vector3(1, 100, 1);
            newPiece.transform.SetParent(cells[x, y].cellPrefab.transform, false);
            cells[x, y].chessPiece = newPiece.GetComponent<ChessPiece>();
        }
    }

    public class ChessBoardCell
    {
        public int x;
        public int y;
        public GameObject cellPrefab;
        public ChessPiece chessPiece;
    }
}