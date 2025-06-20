using UnityEngine;

public class health : MonoBehaviour
{
    [SerializeField]
    private int healthLevel = 3;
    private int currentHp;
    public Transform healthBar;
    public Texture2D heart;

    /*public int Hp
    {
        get => currentHp;
        private set { var isDamage = value < currentHp;
            currentHp = Mathf.Clamp(value, 0, healthLevel);
        };*/
    // Update is called once per frame
    void Awake()
    {
        currentHp = healthLevel;
    }
}
