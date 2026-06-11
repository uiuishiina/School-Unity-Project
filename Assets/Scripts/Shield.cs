using UnityEngine;
using UnityEngine.EventSystems;


public interface ShiledInterface : IEventSystemHandler
{
    void OnRecieve(bool multi);
}

public class Shield : MonoBehaviour, ShiledInterface
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] int score;
    private bool mul = false;
    public void OnRecieve(bool multi)
    {
        mul = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (mul) {
                gameManager.ScoreUpdate(score * 2);
                mul = false;
            }
            else {
                gameManager.ScoreUpdate(score);
            }
        }
    }
}
