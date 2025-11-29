using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileScriptableObject", menuName = "Scriptable Objects/ProjectileScriptableObject")]
public class ProjectileScriptableObject : ScriptableObject
{
    public float speed; // 총알의 속도
    public float lifeTime; // 총알 생존 시간
    public float damage; // 대미지;
    public GameObject poolingPrefab; // 복사할 프리팹 오브젝트
    public LayerMask layerMask; // 레이어 마스크 (관통 레이어 정의)
    
    public AudioSource source;
    public AudioClip fireSound;
}