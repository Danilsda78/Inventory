using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Text _txtDamagePrefab;
    public Transform _txtDamageParent;

    public void Init(Transform spawnPoint)
    {
        transform.position = spawnPoint.position;
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(SpawnTxt(damage.ToString()));
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

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
