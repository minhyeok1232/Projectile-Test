using System;
using UnityEngine;
using UnityEngine.Pool; // Unity 지원하는 Pool Namespace 등록

public class Bullet : MonoBehaviour
{
    private ProjectileScriptableObject data;
    private IObjectPool<Bullet> pool;
    private Rigidbody rb;
    
    private float currentLifeTime;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable() // Pool 에서 총알을 꺼내 올 때
    {
        currentLifeTime = 0f;
        
        // 물리 초기화
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero; 
        }

        Fly();
    }
    
    
    
    // Projectile 이 생성 될 시,
    #region Initialize 
    public void SetManagedPool(IObjectPool<Bullet> _pool)
    {
        pool = _pool;
    }

    public void SetData(ProjectileScriptableObject _data)
    {
        data = _data;
    }
    #endregion

    void Fly()
    {
        rb.AddForce(transform.right * data.speed, ForceMode.Impulse);
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("other : " + other.gameObject.layer);
        Debug.Log("bullet : " + data.layerMask.value);
        
        // 충돌 레이어 확인
        if ((data.layerMask.value & (1 << other.gameObject.layer)) > 0)
        {
            Debug.Log($"{other.name} hit! Damage: {data.damage}");
            
            ReleaseToPool();
        }
    }

    void Update()
    {
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime >= data.lifeTime)
        {
            ReleaseToPool();
        }
    }

    void ReleaseToPool()
    {
        // 연결된 풀로 자신을 반환
        if (pool != null)
        {
            pool.Release(this);
        }
        else
        {
            Destroy(gameObject); // 풀이 없으면 파괴 (안전장치)
        }
    }
}