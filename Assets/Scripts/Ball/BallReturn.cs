using UnityEngine;

public class BallReturn : MonoBehaviour
{
    [SerializeField] private GameObject m_WarningObject;
    
    private BallLauncher ballLauncher;
    public static int BlocksUnderWarningLevel = 0;

    private void Awake()
    {
        ballLauncher = FindObjectOfType<BallLauncher>();
        m_WarningObject = Instantiate(m_WarningObject) as GameObject;
    }

    private void Update()
    {
        if (BlocksUnderWarningLevel > 0)
        {
            m_WarningObject.SetActive(true);
        }
        else
        {
            m_WarningObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ballLauncher.ReturnBall();
        collision.collider.gameObject.SetActive(false);
    }
}