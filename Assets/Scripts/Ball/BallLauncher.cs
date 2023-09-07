using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallLauncher : MonoBehaviour
{
    public int NumberOfBalls { get { return maxBalls; } }

    [SerializeField] private Ball ballPrefab;
    [SerializeField] private BlockSpawner blockSpawner;

    private Vector3 startDragPosition;
    private Vector3 endDragPosition;
    private LaunchPreview launchPreview;
    private List<Ball> balls = new List<Ball>();
    private int ballsReady;
    private int maxBalls;

    private void Awake()
    {
        launchPreview = GetComponent<LaunchPreview>();
        CreateBall();
    }

    public void PullDownBalls()
    {
        foreach (Ball ball in balls)
        {
            ball.PullDown();
        }
    }

    public void ReturnBall()
    {
        ballsReady++;
        if (ballsReady == balls.Count)
        {
            blockSpawner.SpawnRowOfBlocks();
            CreateBall();
        }
    }

    private void CreateBall()
    {
        var ball = Instantiate(ballPrefab);
        ball.gameObject.SetActive(false);
        balls.Add(ball);
        ballsReady++;
    }

    private void Update()
    {
        if (ballsReady != balls.Count || GameManager.instance.IsGameOver) // don't let the player launch until all balls are back and game isn't over.
            return;

        maxBalls = ballsReady;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.back * -10;

        if (Input.GetMouseButtonDown(0))
        {
            StartDrag(worldPosition);
        }
        else if (Input.GetMouseButton(0))
        {
            ContinueDrag(worldPosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
    }

    private void EndDrag()
    {
        launchPreview.Reset();
        Vector3 direction = endDragPosition - startDragPosition;
        direction.Normalize();

        if(direction.y < 0)
            StartCoroutine(LaunchBalls(direction));
    }

    private IEnumerator LaunchBalls(Vector3 direction)
    {
        foreach (var ball in balls)
        {
            if (!ball) continue;

            ball.Reset();
            ball.transform.position = transform.position;
            ball.gameObject.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce(-direction);

            ballsReady -= 1;

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void ContinueDrag(Vector3 worldPosition)
    {
        endDragPosition = worldPosition;

        Vector3 direction = endDragPosition - startDragPosition;
        launchPreview.SetEndPoint(transform.position - direction);
        if(direction.y > 0f)
        {
            // Negative
            launchPreview.SetColor(Color.red);
        }else
        {
            launchPreview.SetColor(GetColorBasedOnLevel(SceneManager.GetActiveScene().name));
        }
    }

    private void StartDrag(Vector3 worldPosition)
    {
        startDragPosition = worldPosition;
        launchPreview.SetStartPoint(transform.position);
    }

    private Color GetColorBasedOnLevel(string levelName)
    {
        Color color = new();
        if(levelName == Strings.yellowLevel)
        {
            color = Color.yellow;
        }
        else if(levelName == Strings.magentaLevel)
        {
            color.r = 0.32f;
            color.g = 0.37f;
            color.b = 0.96f;
            color.a = 1f;
        }
        else if( levelName == Strings.cyanLevel)
        {
            color.r = 0.08f;
            color.g = 1f;
            color.b = 0;
            color.a = 1f;
        }

        return color;
    }
}
