public interface IGameLoop
{
    ClientMono GetClientMono();
    PrintTable GetPrintTable();
    void SetStatusText(string text);
    void SetIsTurn(bool isTurn);
    bool GetIsTurn();
    MoveRequest GetMoves();
}