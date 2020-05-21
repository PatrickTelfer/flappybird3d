using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    float moveSpeed;
    public BallSpawner observer;
    public int index;
    bool deleted = false;
    ParticleSystem ps;
    public void HoverEnter()
    {
        GameManager.IncreaseScore();

        DeletePellet(true);
        

    }

    private void Start()
    {
        ps = FindObjectOfType<ParticleSystem>();
    }

    private void DeletePellet(bool playerPopped)
    {
        if (deleted == false)
        {
            Debug.Log("pellet deleted");
            observer.notify(index);
            deleted = true;
            StartCoroutine(ShrinkPelletOverTime(0.25f, playerPopped));
        }
        
    }


    IEnumerator ShrinkPelletOverTime(float time, bool playerPopped)
    {
        if (time == 0)
        {
            yield return null;
        }

        Vector3 size = transform.localScale;
        Vector3 nothing = new Vector3(0, 0, 0);

        float currentTime = 0.0f;

        do
        {
            transform.localScale = Vector3.Lerp(size, nothing, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        if (playerPopped)
        {
            ps.transform.position = transform.position;
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.startColor = UnityEngine.Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.85f, 1f);
            ps.Emit(emitParams, 30);
            ps.Play();
            GameManager.PlaySoundEffect();
        }
       


        Destroy(this.gameObject);
        yield return null;
    }

    void Update()
    {
        MovePellet(moveSpeed);
    }

    public void SetupPellet(int index, float distanceBetweenPellets, float moveSpeed)
    {
        float z = UnityEngine.Random.Range(-1, 3);
        float y = UnityEngine.Random.Range(1, 18);
        this.moveSpeed = moveSpeed;
        float xOffset = UnityEngine.Random.Range(5f, 10f);

        if (UnityEngine.Random.Range(-1f, 1f) < 0)
        {
            xOffset *= -1;
        }

        transform.localPosition = new Vector3(index * distanceBetweenPellets + xOffset, y, z);
    }

    public void MovePellet(float amount)
    {
        if (transform.localPosition.x < -50f)
        {
            DeletePellet(false);
        }

        transform.position += Vector3.left * amount * Time.deltaTime;
    }
}
