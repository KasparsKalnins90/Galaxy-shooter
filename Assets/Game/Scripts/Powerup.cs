using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private int _powerupID; // 0 = tripleshot, 1= Speed boost, 2 = shields
    


    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                if(_powerupID == 0)
                {
                    player.TripleShotPowerupOn();
                } else if (_powerupID == 1)
                {
                    player.SpeedPowerupOn();   
                } else if (_powerupID ==2)
                {
                    player.ShieldPowerupOn();
                }
                
            }
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update ()
    {
        transform.Translate(Vector3.down *_speed * Time.deltaTime );
	if(transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }	
	}
}
