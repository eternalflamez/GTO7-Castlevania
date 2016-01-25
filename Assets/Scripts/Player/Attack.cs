using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
    private PlatformerCharacter2D player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlatformerCharacter2D>();
	}
	
    public void DoAttack()
    {
        player.HitEnemies();
    }
}
