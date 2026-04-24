using UnityEngine;

public class FloorObject : MonoBehaviour
{
    private int posX_;
    void Start()
    {
        
    }

    public void SetPosX(int x){ posX_ = x; }
    public int GetPosX() { return posX_; }
}
