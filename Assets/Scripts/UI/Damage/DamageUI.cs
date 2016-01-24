using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour {
    [SerializeField]
    private Text text;

    public void SetText(int value, bool heal)
    {
        text.text = value.ToString();

        if(heal)
        {
            text.color = Color.green;
        }
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
