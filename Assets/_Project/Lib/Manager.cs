using UnityEngine;

public abstract class Manager : MonoBehaviour
{
    protected GameManager game { get; private set; }

    protected void Log(string message)
    {
        Debug.Log($"[{gameObject.name}] {message}");
    }

    public void SetGameManager(GameManager game)
    {
        this.game = game;
    }
}
