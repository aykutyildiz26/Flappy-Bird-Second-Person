using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyThingControl : MonoBehaviour
{
    public static float power = 3.5f;
    public static float pipeScore = 0;
    public float pipeSpeed = 1f;
    public static Rigidbody rb;
    public List<GameObject> Pipes = new List<GameObject>();
    public SoundManager SoundManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        pipeScore = 0;
        SoundManagerScript = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.gameOver)
        {
            if (!PlayerManager.startGamePanel)
            {
                for (int i = 0; i < Pipes.Count; i++)
                {
                    Pipes[i].transform.Translate(-(transform.right) * Time.deltaTime * (pipeSpeed + (Time.deltaTime / 10)));
                    if (Pipes[i].transform.position.x + 8 < (this.gameObject.transform.position.x))
                    {
                        //Debug.Log("You shall not pass!");
                        Pipes[i].transform.position = new Vector3(20, Random.Range(24, 30), Pipes[i].transform.position.z);

                    }
                }
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        //Debug.Log("Triggered");
        SoundManagerScript.audioSource.PlayOneShot(SoundManagerScript.hitPipe, 0.35f);
        PlayerManager.gameOver = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Point Earned! " + pipeScore);
        pipeScore++;
        SoundManagerScript.audioSource.PlayOneShot(SoundManagerScript.pointEarned, 0.50f);
    }
}
