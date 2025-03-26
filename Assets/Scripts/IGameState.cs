using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IGameState
{
    UniTask Enter();
    UniTask Doing();
    UniTask Exit();

    StateOfGame NextState();
}