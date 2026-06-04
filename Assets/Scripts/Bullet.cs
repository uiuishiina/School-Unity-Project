using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    float timer = 0;
    Action<GameObject> callback_;
    bool stop = true;

    public void Initialize(Vector3 pos, int vec, float time, Action<GameObject> callback)
    {
        transform.position = pos;
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocityX = vec;
        timer = time;
        callback_ = callback;
        stop = false;
    }

    void End()
    {
        stop = true;
        timer = 0;
        callback_?.Invoke(this.gameObject);
    }
    private void Update()
    {
        if (stop) { return; }
        if (timer > 0) {
            timer -= Time.deltaTime;
        }
        else {
            End();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            End();
        }
        else if (collision.gameObject.CompareTag("Shield")) {
            End();
        }
    }
}
