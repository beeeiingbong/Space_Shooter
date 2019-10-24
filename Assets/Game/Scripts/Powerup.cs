using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID;//0=triple shot, 1=Speed Boost, 2=Shields
    [SerializeField]private AudioClip _clip;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: "+ other.name);

        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            if (player != null)
            {
                //enable TripleShot
                if (powerupID == 0)
                {
                    player.TripleShotPowerupOn();

                }
                
                //enable speed boost here
                else if(powerupID == 1)
                {
                    //enable spped boost here
                    player.SpeedUpPowerOn();
                }

                //enable shields 

                else if(powerupID == 2)
                {
                    player.EnableShields();
                }
            }
            //destroy ourself
            Destroy(this.gameObject);
        }

    }
}
