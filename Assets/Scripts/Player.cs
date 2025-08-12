using UnityEngine;

public class Player : MonoBehaviour
{
    public ReactProperty<float> Healf = new();
    [SerializeField] private float _healthMax = 100;

    public void Init(Transform spawnPoint)
    {
        Healf.Value = _healthMax;
        transform.position = spawnPoint.position;
    }

    public void GetHeal(float heal)
    {

    }
}
