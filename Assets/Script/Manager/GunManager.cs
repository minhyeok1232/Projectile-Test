using UnityEngine;

public class GunManager : Singleton<GunManager>
{
    [Header("Transform")] 
    [SerializeField] private Transform muzzlePoint; 
    
    // TODO 
    // 게임 시작 시, Gun을 생성하고 장착(착용) 및 총 전환 관리 로직은 여기서 하나, 
    // 나중에 할 예정.
    
    public ParticleSystem particle;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // 총의 위치를 반환한다.
    public Transform GetMuzzlePoint()
    {
        return muzzlePoint;
    }

    public void GetMuzzleOn()
    {
        particle.Stop(); // 이미 실행중이면, 멈추고 다시 실행.
        particle.Play();
    }
}
