using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _ExplosionPrefab;
    [SerializeField]
    private GameObject _ShieldObject;
    private GameManager _gameManager;
    [SerializeField]
    private GameObject[] _engines;
    private int _hitCount = 0;
    [SerializeField]
    private float _fireRate = 0.25f;

    private float _canFire = 0.0f;

    public bool canTripleShot = true;
    public bool speedBoostOn = true;
    public bool shieldIsOn = false;
    public int playerHealth = 3;
    private UIManager _uiManager;

    [SerializeField]
    private float _speed = 5.0f;
    private Spawn_Manager _spawnManager;
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _clip;
   
	// Use this for initialization
	void Start () {
        _audioSource = GetComponent<AudioSource>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<Spawn_Manager>();
       // _spawnManager.StartSpawnRoutine();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager != null)
        {
            _uiManager.UpdateLives(playerHealth);
        }
        transform.position = new Vector3(0, 0, 0);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        if(_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutine();
        }
        _hitCount = 0;
	}
	
	// Update is called once per frame
	void Update () {

        Movement();

        if ((Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
        
    }
    
	private void Shoot()
    {
        if (Time.time > _canFire)
        {
            _audioSource.Play();
            if (canTripleShot)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);   
            }else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
        }
        _canFire = Time.time + _fireRate;
    }
    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (speedBoostOn)
        {
            transform.Translate(Vector3.right * _speed * 1.50f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 1.50f * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }
        if (transform.position.x < -9.4f)
        {
            transform.position = new Vector3(9.2f, transform.position.y, 0);
        }
        else if (transform.position.x > 9.4f)
        {
            transform.position = new Vector3(-9.3f, transform.position.y, 0);
        }
        else if (transform.position.y > 4.2f)
        {
            transform.position = new Vector3(transform.position.x, 4.2f, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }
    }
    public void SpeedPowerupOn()
    {
        AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
        speedBoostOn = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    public  IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        speedBoostOn = false;
    }
    public void TripleShotPowerupOn()
    {
        AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }
    public void ShieldPowerupOn()
    {
        AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
        shieldIsOn = true;
        _ShieldObject.SetActive(true);
    }
    public void takeDamage()
    {
        _hitCount++;
        if (shieldIsOn == true)
        {
            shieldIsOn = false;
            _ShieldObject.SetActive(false);
            return;
        }
        else
        {
            
            playerHealth -= 1;
            _uiManager.UpdateLives(playerHealth);
        }
        if (playerHealth < 1)
        {
            Instantiate(_ExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            _uiManager.ShowTitleScreen();
            _gameManager.gameOver = true;
            
            
        }
        if (_hitCount == 1)
        { 
            _engines[Random.Range(0,1)].SetActive(true);
        } else if (_hitCount == 2 && _engines[0])
        {
            _engines[1].SetActive(true);
        } else
        {
            _engines[0].SetActive(true);
        }
    }
}
