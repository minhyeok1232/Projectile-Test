using UnityEngine;
using UnityEngine.Pool;

public class ProjectileManager : Singleton<ProjectileManager>
{
    [Header("Settings")]
    [SerializeField] private ProjectileScriptableObject data; // 여기에 SO 파일 넣기
    private IObjectPool<Bullet> pool;
    
    private Transform point; // 총 스폰 후, 건의 muzzle에 넣을 예정.
    
    private bool isInitialized = false;

    void Start() // 추후 총이 생기게 되면, 총 스크립트의 OnEnable() 을 참조하는게 좋을 것 같음.
    {
        point = GameManager.Instance.GetGunManager().GetMuzzlePoint();
    }
    
    public void SetInit()
    {
        if (isInitialized) return;

        // Pooling Interface 빌려서 사용
        pool = new ObjectPool<Bullet>(
            CreateProjectile, // 생성
            (p) => {  // pool 에서 꺼냄
                p.gameObject.SetActive(true); 
                p.transform.position = point.position; 
                p.transform.rotation = point.rotation;
            },
            (p) => p.gameObject.SetActive(false), // pool 에 반납
            (p) => Destroy(p.gameObject), // pool 용량 초과 -> 파괴
            true,
            20,
            100
        );

        isInitialized = true;
        Debug.Log("ProjectileManager Initialized");
    }
    
    private Bullet CreateProjectile()
    {
        GameObject obj = Instantiate(data.poolingPrefab);
        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.SetManagedPool(pool); 
        bullet.SetData(data); 
        return bullet;
    }

    public void Fire()
    {
        if (!isInitialized) SetInit();
        pool.Get();
    }
}
