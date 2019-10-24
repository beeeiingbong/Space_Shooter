using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyExplosionPrefab;
    [SerializeField]
    private AudioClip _clip;

    //variable for your speed
    private float speed = 5.0f;

    private UIManager _uiManager;
    


    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // move down
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        //when off the screen on the bottom
        if(transform.position.y < -7)
        {
            float randomX = Random.Range(-7f,7f);
            transform.position = new Vector3(randomX, 7 ,0 );
        }
        //respawn back on top with a new x position between the bounds of the screen    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "laser")
        {
            Destroy(other.gameObject);
            Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
            _uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            Player player =other.GetComponent<Player>();

            if(player!=null)
            {
                player.Damage();
            }
            Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
        }
    } 
}
