using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    [SerializeField]
    
    private float _speed = 2.0f;
    [SerializeField]
    private GameObject _EnemyExplosionPrefab;
    private UIManager _uiManager;
    [SerializeField]
    private AudioClip _clip;
  

    private void _Explode()
    {
        
        Instantiate(_EnemyExplosionPrefab, transform.position, Quaternion.identity);
        
        
    }
    private void Start()
    {
        
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            { 
                _Explode();
                player.takeDamage();
                AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
                Destroy(this.gameObject);
            }
            
        } else if (other.tag == "Laser")
        { if (other.transform.parent != null)
            {
                
                Destroy(other.transform.parent.gameObject);

            }
            Destroy(other.gameObject);
            _Explode();
            _uiManager.updateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
            
            
        }
        
    }

    // Update is called once per frame
    void Update () {
        
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <  -7.2f)
        {
            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 8, 0);
        }

        
    }
}
