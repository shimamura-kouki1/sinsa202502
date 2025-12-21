using UnityEngine;

public class Gan : MonoBehaviour
{
    [SerializeField] private Transform _shotPos;
    [SerializeField] private Camera _playerCamera;
    //[SerializeField] private GameObject _stratPos;
    [SerializeField]private float _shotSpeed;
    [SerializeField] private float _shotTime;
    [SerializeField] private GunManager _gunManager;


    [Header("Gan Setting")]
    [SerializeField]private float _renge = 100f;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shot();
    }
    public void Shot()
    {
      
    }
}
