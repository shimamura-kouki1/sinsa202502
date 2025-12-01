using UnityEngine;

public class Gan : MonoBehaviour
{
    [SerializeField] private Transform _shotPos;
    [SerializeField] private Camera _playerCamera;
    //[SerializeField] private GameObject _stratPos;
    [SerializeField]private float _shotSpeed;
    [SerializeField] private float _shotTime;

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
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        RaycastHit hit;
        
        if(Physics.Raycast(ray,out hit, _renge))
        {

        }
    }
}
