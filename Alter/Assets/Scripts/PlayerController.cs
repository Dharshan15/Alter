using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject focalPoint;
    private Rigidbody playerRb;
    public GameObject powerupIndicator;
    public float speed = 4.8f;
    public bool hasPowerup;
    private float powerUpStrength = 10.0f;
    public bool isGameOver = false;

    public AudioSource dashAudio;
    public AudioSource powerUpAudio;
    public AudioSource normalDashAudio;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Vertical");

        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerupIndicator.gameObject.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if (transform.position.y < -3)
        {
            isGameOver = true;
            //Time.timeScale = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup") )
        {
            powerUpAudio.Play();
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
            StartCoroutine(powerupCountdownRoutine());
        }
    }
    IEnumerator powerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        powerupIndicator.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup )
        {
            dashAudio.Play();
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
            Debug.Log("collided with :" + collision.gameObject.name + "with powerup set to " + hasPowerup);
        }
        if (collision.gameObject.CompareTag("Enemy"))
            normalDashAudio.Play();       
    }
}
