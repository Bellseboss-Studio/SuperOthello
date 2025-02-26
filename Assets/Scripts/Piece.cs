using UnityEngine;

public class Piece : MonoBehaviour
{
    public int x;
    public int y;
    private PrintTable boardManager; // Referencia para enviar el movimiento

    public void Init(int x, int y, PrintTable boardManager)
    {
        this.x = x;
        this.y = y;
        this.boardManager = boardManager;
    }

    private void OnMouseDown()
    {
        Debug.Log($"Click en la pieza ({x}, {y})");
        boardManager.OnTileClicked(x, y);
    }
}