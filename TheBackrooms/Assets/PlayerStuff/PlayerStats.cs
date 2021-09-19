using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int hp = 4;

    public AudioSource grape;
    public AudioSource sandwich;

    public AudioSource breathing1;
    public AudioSource breathing2;
    public AudioSource breathing3;

    public AudioSource randomSpooky1;
    public AudioSource randomSpooky2;
    public AudioSource randomSpooky3;

    public Sprite face1;
    public Sprite face2;
    public Sprite face3;
    public Sprite dead;

    public Sprite current;

    private GameObject overlay;

    public void TakeDamage() {
        hp -= 1;
        if (hp == 3) {
            current = face1;
            breathing1.Play();
            breathing2.Pause();
            breathing3.Pause();
            //breathing4.Pause();
        } else if (hp == 2) {
            current = face2;
            breathing1.Pause();
            breathing2.Play();
            breathing3.Pause();
            //breathing4.Pause();
        } else if (hp == 1) {
            current = face3;
            breathing1.Pause();
            breathing2.Pause();
            breathing3.Play();
            //breathing4.Pause();
        } else if (hp == 0) {
            current = dead;
        }

        overlay.GetComponent<SpriteRenderer>().sprite = current;

        if (hp == 0) {
            // TODO: end the timer
        }
    }

    public void Heal(string foodname) {
        if (hp == 0) return;
        
        hp += 1;

        if (hp == 3)
            current = face1;
        else if (hp == 2)
            current = face2;
        else if (hp == 1)
            current = face3;
        else if (hp == 0)
            current = dead;

        overlay.GetComponent<SpriteRenderer>().sprite = current;

        if (foodname == "grape") {
            // todo: play mm is grape
            grape.loop = false;
            grape.Play();
        } else {
            // todo: play default
            sandwich.loop = false;
            sandwich.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        grape = transform.Find("Grape").GetComponent<AudioSource>();
        sandwich = transform.Find("Sandwich").GetComponent<AudioSource>();

        breathing1 = transform.Find("Breathing1").GetComponent<AudioSource>();
        breathing2 = transform.Find("Breathing2").GetComponent<AudioSource>();
        breathing3 = transform.Find("Breathing3").GetComponent<AudioSource>();

        randomSpooky1 = transform.Find("Ambient").GetComponent<AudioSource>();
        randomSpooky2 = transform.Find("Ambient2").GetComponent<AudioSource>();
        randomSpooky3 = transform.Find("Ambient3").GetComponent<AudioSource>();

        breathing1.loop = false;
        breathing2.loop = false;
        breathing3.loop = false;

        overlay = new GameObject("overlay");
        overlay.AddComponent<SpriteRenderer>();
        overlay.transform.parent = transform.Find("Camera").transform;
        overlay.transform.localPosition = new Vector3(0, 0, 0.31f);
        overlay.transform.localScale = new Vector3(0.041f, 0.041f, 0.05f);
    }

    float currentTime = 0f;
    float limit = 5f;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > limit) {
            currentTime = currentTime - limit;
            limit = Random.Range(2, 80);

            switch(Random.Range(0, 3)) {
                case 0:
                    randomSpooky1.Play();
                    limit += 10;
                    break;
                case 1:
                    randomSpooky2.Play();
                    limit += 15;
                    break;
                case 2:
                    randomSpooky3.Play();
                    limit += 70;
                    break;
                default:
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            TakeDamage();
        }
    }
}
