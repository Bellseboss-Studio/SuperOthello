using UnityEngine;

public class PrintTable : MonoBehaviour
{
    public GameObject emptyPrefab;
    public GameObject blackPrefab;
    public GameObject whitePrefab;
    public Vector2 boardCenter = Vector2.zero; // Centro del tablero en Unity
    public float cellSize = 1.0f; // Tamaño de cada celda

    private GameObject[,] boardObjects = new GameObject[8, 8]; // Para almacenar los objetos creados
    private GameLoop _gameLoop;

    public void Config(GameLoop gameLoop)
    {
        _gameLoop = gameLoop;
    }

    public void GenerateBoard(int[][] board)
    {
        Debug.Log($"Generando tablero...");
        if (board == null || board.Length == 0)
        {
            Debug.LogError("El tablero es nulo o vacío.");
            return;
        }

        // Limpiar el tablero si ya hay fichas
        ClearBoard();

        int rows = board.Length;
        int cols = board[0].Length;

        // Calcular la esquina superior izquierda del tablero en Unity
        Vector2 startPos = boardCenter - new Vector2(cols / 2f, rows / 2f) * cellSize;

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                Vector2 worldPosition =
                    startPos + new Vector2(x * cellSize,
                        -y * cellSize); // Invertimos Y porque en Unity (0,0) está abajo

                GameObject prefabToInstantiate = GetPrefabForValue(board[x][y]);

                if (prefabToInstantiate != null)
                {
                    var gameobjectPiece = Instantiate(prefabToInstantiate, worldPosition, Quaternion.identity);
                    gameobjectPiece.GetComponent<Piece>().Init(x, y, this);
                    boardObjects[y, x] = gameobjectPiece;
                }
            }
        }
    }

    private GameObject GetPrefabForValue(int value)
    {
        switch (value)
        {
            case 1: return blackPrefab;
            case 2: return whitePrefab;
            default: return emptyPrefab;
        }
    }

    private void ClearBoard()
    {
        for (int y = 0; y < boardObjects.GetLength(0); y++)
        {
            for (int x = 0; x < boardObjects.GetLength(1); x++)
            {
                if (boardObjects[y, x] != null)
                {
                    Destroy(boardObjects[y, x]);
                }
            }
        }
    }

    public void OnTileClicked(int x, int y)
    {
        _gameLoop.OnTileClicked(x, y);
    }
}