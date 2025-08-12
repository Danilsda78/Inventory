using UnityEngine;

public class Player : MonoBehaviour
{
    public ReactProperty<float> Health = new();
    [SerializeField] private float _healthMax = 100;
    [SerializeField] private float _health;

    public void Init(Transform spawnPoint)
    {
        Health.Value = 50;
        transform.position = spawnPoint.position;
        Health.EChanged += (float helf) => { _health = helf; };
    }

    public void GetHeal(float health)
    {
        var newHealth = Mathf.Clamp(Health.Value + health, 0, _healthMax);
        Health.Value = newHealth;
    }
}
