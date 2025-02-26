using System;
using System.Text;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class ClientHttpFacade
{
    private string _baseUrl;

    public ClientHttpFacade(string baseUrl)
    {
        _baseUrl = baseUrl.TrimEnd('/');
    }

    public async UniTask<CreateGameResponse> CreateGame()
    {
        using UnityWebRequest request = UnityWebRequest.PostWwwForm($"{_baseUrl}/game", "");
        await request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error al crear partida: {request.error}");
            throw new Exception("Error al crear partida");
        }
        else
        {
            string json = request.downloadHandler.text;
            Debug.Log($"CreateGameResponse: {json}");
            
            return JsonHelper.Deserialize<CreateGameResponse>(json);
        }
    }

    public async UniTask<GetGameResponse> GetGameState(int gameId)
    {
        using UnityWebRequest request = UnityWebRequest.Get($"{_baseUrl}/game/{gameId}");
        await request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error al obtener estado de la partida: {request.error}");
            throw new Exception("Error al obtener estado de la partida");
        }
        else
        {
            string json = request.downloadHandler.text;
            Debug.Log($"GetGameResponse: {json}");
            return JsonHelper.Deserialize<GetGameResponse>(json);
        }
    }

    public async UniTask<ValidMoveResponse> GetValidMoves(int gameId)
    {
        using UnityWebRequest request = UnityWebRequest.Get($"{_baseUrl}/game/{gameId}/valid-moves");
        await request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error al obtener movimientos válidos: {request.error}");
            throw new Exception("Error al obtener movimientos válidos");
        }
        else
        {
            string json = request.downloadHandler.text;
            return JsonHelper.Deserialize<ValidMoveResponse>(json);
        }
    }

    public async UniTask<StatusGameResponse> GetGameStatus(int gameId)
    {
        using UnityWebRequest request = UnityWebRequest.Get($"{_baseUrl}/game/{gameId}/status");
        await request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error al obtener estado de la partida: {request.error}");
            throw new Exception("Error al obtener estado de la partida");
        }
        else
        {
            string json = request.downloadHandler.text;
            return JsonHelper.Deserialize<StatusGameResponse>(json);
        }
    }

    public async UniTask<MoveResponse> MakeMove(int gameId, MoveRequest move)
    {
        string json = JsonUtility.ToJson(move);
        byte[] body = Encoding.UTF8.GetBytes(json);

        using UnityWebRequest request = new UnityWebRequest($"{_baseUrl}/game/{gameId}/move", "POST");
        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        await request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error al realizar movimiento: {request.error}");
            throw new Exception("Error al realizar movimiento");
        }
        else
        {
            string responseJson = request.downloadHandler.text;
            return JsonHelper.Deserialize<MoveResponse>(responseJson);
        }
    }
}

public static class JsonHelper
{
    public static T Deserialize<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}