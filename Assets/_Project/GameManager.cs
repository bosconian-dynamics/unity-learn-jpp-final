using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public EventManager Events { get; private set; } = new EventManager();
    public Dictionary<Type, Manager> Managers { get; private set; } = new Dictionary<Type, Manager>();

    /**
     * Unity MonoBehavior Messages
     **/

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Debug.LogError("Attempted to instantiate a second GameManager.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Register all managers from child GameObjects.
        RegisterManagers(GetComponentsInChildren<Manager>());
    }

    /**
     * Public Methods
     **/

    public void RegisterManager(Manager manager)
    {
        Type managerType = manager.GetType();

        if (Managers.ContainsKey(managerType))
        {
            Debug.LogError($"Duplicate registration for Manager of type \"{managerType}\".");
            return;
        }

        Managers[managerType] = manager;
        manager.SetGameManager(this);
    }

    public void RegisterManagers(IEnumerable<Manager> managers)
    {
        foreach (Manager manager in managers)
            RegisterManager(manager);
    }
}
