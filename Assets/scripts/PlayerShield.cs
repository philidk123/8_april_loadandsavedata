using UnityEngine;
using System.Collections;

public class PlayerShield : MonoBehaviour
{
    public GameObject shieldPrefab;

    private GameObject activeShield;
    private bool shieldActive = false;

    public void ActivateShield()
    {
        if (!shieldActive)
        {
            StartCoroutine(ShieldCoroutine());
        }
    }

    IEnumerator ShieldCoroutine()
    {
        shieldActive = true;

        
        activeShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);

        
        activeShield.transform.SetParent(transform);

        
        yield return new WaitForSeconds(5f);

        
        Destroy(activeShield);

        shieldActive = false;
    }
}
