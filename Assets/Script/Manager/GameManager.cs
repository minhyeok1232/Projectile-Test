using UnityEngine;
using System;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    [Header("Controller")]
    [SerializeField] private ProjectileManager projectileManager;

    [SerializeField] private GunManager gunManager;
    
    void Awake()
    {
        EnsureSingleInstance();
    }
    
    void Start()
    {
        if (projectileManager == null)
        {
            projectileManager = ProjectileManager.Instance;
        }

        projectileManager.SetInit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (projectileManager != null)
                projectileManager.Fire();
            if (gunManager != null)
                gunManager.GetMuzzleOn();
        }
    }

    public GunManager GetGunManager()
    {
        return gunManager;
    }
}
