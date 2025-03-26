using System;
using Newtonsoft.Json;

public class BaseResponse
{
    public string status;
}

[Serializable]
public class CreateGameResponse
{
    public string status;
    public int id;
    public int[][] board;
    public string currentPlayer;
    public string updatedAt;
    public string createdAt;
}

[Serializable]
public class GetGameResponse
{
    public int id { get; set; }
    public int[][] board { get; set; }
    public string currentPlayer { get; set; }
    public string status { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
}

[Serializable]
public class ValidMoveResponse
{
    public Move[] validMoves;
    public string player;
}

[Serializable]
public class Move
{
    public int x;
    public int y;
}

[Serializable]
public class StatusGameResponse
{
    public string status;
    public string curren_turn;
}

[Serializable]
public class MoveRequest
{
    public int x;
    public int y;

    public MoveRequest(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

[Serializable]
public class MoveResponse
{
    public int id;
    public int[][] board;
    public string currentPlayer;
    public string status;
    public string createdAt;
    public string updatedAt;
}