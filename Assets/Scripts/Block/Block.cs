using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Color minColor;
    [SerializeField] private Color maxColor;

    private int hitsRemaining = 5;
    private static float warningY = -2.0f, gameOverY = -3.3f;

    private SpriteRenderer spriteRenderer;
    private TextMeshPro text;

    private bool m_Counted = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TextMeshPro>();
        UpdateVisualState();
    }
    private void Update()
    {
        if (transform.position.y < warningY)
        {
            if(!m_Counted)
            {
                BallReturn.BlocksUnderWarningLevel++;
                m_Counted = true;
            }
        }
        if(transform.position.y <= gameOverY)
        {
            GameManager.instance.Gameover();
        }
    }

    private void OnDestroy()
    {
        if (m_Counted)
        {
            BallReturn.BlocksUnderWarningLevel--;
            m_Counted = false;
        }
    }

    private void UpdateVisualState()
    {
        text.SetText(hitsRemaining.ToString());
        spriteRenderer.color = Color.Lerp(minColor, maxColor, Mathf.Clamp01((float)hitsRemaining / 10f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ScoreManager.Score++;
        AudioManager.instance.Play(Strings.bounceAudio);
        hitsRemaining--;

        if (hitsRemaining > 0)
            UpdateVisualState();
        else
            Destroy(gameObject);
    }

    internal void SetHits(int hits)
    {
        hitsRemaining = hits;
        UpdateVisualState();
    }
}