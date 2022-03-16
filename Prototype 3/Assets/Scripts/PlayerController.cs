using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody playerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool gameOver = false;
    private Animator playerAnim;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    void Start()
    {
	playerRb = GetComponent<Rigidbody>();
    playerAnim = GetComponent<Animator>();
	Physics.gravity *= gravityModifier;
    playerAudio = GetComponent<AudioSource>();
    }




    // Update is called once per frame
    public ParticleSystem dirtParticle;
    public bool isOnGround = true;
    void Update()
    {
	    if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver) 
	    {
	        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    
	}
    public ParticleSystem explosionParticle;
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        } else if(collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }

}
    