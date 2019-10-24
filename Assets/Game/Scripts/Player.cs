using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private float speed = 5.0f;
    [SerializeField]private float fireRate = 0.25f;
    [SerializeField]private GameObject laserPrefab;
    [SerializeField]private GameObject tripleShotPrefab;
    [SerializeField]private GameObject playerExplosion;
    [SerializeField]private GameObject ShieldGameObject;
    [SerializeField]private GameObject[] engines;

    private float canFire = 0.0f;

    //variable to know if you collected the TripleShot
    public bool canTripleShot= false;
    //variable to know if you collected the speed power up
    public bool isSpeedUp= false;
    //variable to know if you collected the shield
    public bool shieldsActive=false;
    //variable for lives
    public int lives =3;
    //declaring UI manager
    private UIManager _uiManager;
    //declaring GameManger object
    private GameManager _gameManager;
    //declaring variable for spawn manager
    private Spawn_Manager _spawnManager;
    //declaring audio source 
    private AudioSource _audioSource;
    //hit counts track
    private int hitCount = 0;

    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

         if (_uiManager != null)
         {
             _uiManager.UpdateLives(lives);
         }   

         _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
         _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();

         if(_spawnManager != null)
         {
             _spawnManager.StartSpawnRoutines();
         }

         _audioSource =GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();        

        }
    }

    private void Shoot(){
        if (Time.time > canFire)
            {
                _audioSource.Play();
                if (canTripleShot == true)
                {  
                    Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);

                }
                else{
                    Instantiate(laserPrefab, transform.position + new Vector3( 0, 0.99f, 0), Quaternion.identity);
                }
                
                canFire = Time.time + fireRate;
            }
    
    }


    private void Movement(){

          float horizontalInput = Input.GetAxis("Horizontal");
          float verticalInput = Input.GetAxis("Vertical");
          
          //if speed boost enabled
          //move 1.5x speed
          if (isSpeedUp == true)
          {
              
          transform.Translate(Vector3.right * Time.deltaTime * speed*1.5f * horizontalInput);
          transform.Translate(Vector3.up * Time.deltaTime * speed*1.5f * verticalInput);
          }
          //else
          //move normal speed
          else{
          transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
          transform.Translate(Vector3.up * Time.deltaTime * speed * verticalInput);
          }



          if (transform.position.y > 0)
          {
              transform.position = new Vector3(transform.position.x, 0 ,0);
          }
          else if(transform.position.y < -4.1f)
          {
              transform.position = new Vector3(transform.position.x, -4.1f, 0);
          }
          else if (transform.position.x > 10.2f)
          {
              transform.position = new Vector3(-10.2f, transform.position.y, 0);
          }
          else if (transform.position.x < -10.2f)
          {
              transform.position = new Vector3(10.2f, transform.position.y, 0);
          }
    }

    public void Damage()
    {    

         //if player has shields
        if(shieldsActive == true)
        {
            shieldsActive = false;
            ShieldGameObject.SetActive(false); 
            return;
        }

        
        hitCount++;

        if (hitCount ==1)
        {
            //turn on left engine
            engines[0].SetActive(true);
        }
        else if (hitCount==2)
        {
            //turn on right engine
            engines[1].SetActive(true);
        }

        //Subtract 1 life from the player
        lives --;
        _uiManager.UpdateLives(lives);
        //if life is less than 1 (meaning no live)
        //destroy the player
        if (lives <1)
        {
            Instantiate(playerExplosion, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }
    }
    
    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    // method to enable to speedUp
    public void SpeedUpPowerOn()
    {
        isSpeedUp=true;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    public void EnableShields()
    {
        shieldsActive =true;
        ShieldGameObject.SetActive(true);
    }

        //coroutine method(ienumerator) to powerDown the speed Boost
    public IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedUp=false;
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }
}
