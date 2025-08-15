using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text _txtDamagePrefab;
    public Transform _txtDamageParent;
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

        StartCoroutine(SpawnTxt(health.ToString()));
    }

    private IEnumerator SpawnTxt(string text)
    {
        if (_txtDamagePrefab == null || _txtDamageParent == null)
            StopCoroutine("SpawnTxt");

        var txtObj = Instantiate(_txtDamagePrefab, _txtDamageParent);
        txtObj.text = text;
        var newPos = new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f));
        txtObj.transform.position = newPos;

        yield return new WaitForSeconds(1f);
        Destroy(txtObj.gameObject);
    }
}
