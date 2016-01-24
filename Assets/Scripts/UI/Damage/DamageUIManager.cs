using UnityEngine;
using System.Collections;

public class DamageUIManager : Singleton<DamageUIManager> {

    [SerializeField]
    private GameObject damageTextPrefab;

    public void CreateDamageNumber(int damage, Vector3 location, bool toPlayer = false, bool heal = false)
    {
        DamageUI dui = Instantiate(damageTextPrefab).GetComponent<DamageUI>();
        dui.transform.position = location;
        dui.SetText(damage, heal);
        dui.transform.SetParent(this.transform);

        if(toPlayer)
        {
            if(!heal)
            {
                damage = -damage;
            }
            PlayerHealth.instance.ChangeHealth(damage);
        }
    }
}
