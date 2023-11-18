using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Snake : MonoBehaviour
{
    public enum State
    {
        Alive,
        Lose,
        Win
    }

    public State state;

    private CountDown countdown;

    private Vector2 _direction = Vector2.right;
    private Vector2Int gridMoveDirection;
    private Vector2Int gridPosition;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    private List<Transform> _segments;
    public Transform segmentPrefab;
    public BoxCollider2D gridArea;

    public int level = 1;
    public Text levelText;

    public float speed = 0.2f;
    public Text speedText;

    public int Goal = 10;
    public Text GoalText;

    private int score;
    public Text scoreText;

    public GameObject WinNotice;
    public GameObject WinNoticeText;
    public GameObject LoseNoticeText;

    private void Start()
    {

        gridPosition = new Vector2Int(0, 0);
        gridMoveTimerMax = speed;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(1, 0);

        _segments = new List<Transform>();
        _segments.Add(transform);

        state = State.Alive;

        levelText.text = level.ToString();  

        GoalText.text = Goal.ToString();

        speedText.text = level.ToString();

        score = 0;
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Alive:
                HandleInput();
                HandleGridMovement();
                break;
            case State.Lose:
                SetLoseNotice(true);
                break;
            case State.Win:
                break;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (gridMoveDirection.y != -1)
            {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gridMoveDirection.y != 1)
            {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gridMoveDirection.x != 1)
            {
                gridMoveDirection.x = -1;
                gridMoveDirection.y = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gridMoveDirection.x != -1)
            {
                gridMoveDirection.x = 1;
                gridMoveDirection.y = 0;
            }
        }
    }

    private void HandleGridMovement()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            for (int i = _segments.Count - 1; i > 0; i--)
            {
                _segments[i].transform.position = _segments[i - 1].transform.position;
            }


            DauChamDuoi();

            gridPosition += gridMoveDirection;
            gridMoveTimer -= gridMoveTimerMax;

            if (Mathf.Abs(gridPosition.x*2) > gridArea.size.x || 
                Mathf.Abs(gridPosition.y * 2) > gridArea.size.y)
            {
                Debug.Log(gridArea.size);
                if (gridMoveDirection.y == 0)
                {
                    if (gridMoveDirection.x > 0)
                    {
                        gridPosition.x = -gridPosition.x + 1;
                    }
                    else
                    {
                        gridPosition.x = -gridPosition.x - 1;
                    }
                }
                else if (gridMoveDirection.x == 0)
                {
                    if (gridMoveDirection.y > 0)
                    {
                        gridPosition.y = -gridPosition.y + 1;
                    }
                    else
                    {
                        gridPosition.y = -gridPosition.y - 1;
                    }
                }
            }

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
        }
    }  

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add( segment );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Point")
        {
            Grow();
            score += 1;
            scoreText.text = score.ToString();

            if(score == Goal)
            {
                SetWinNotice(true);
                state = State.Win;
            }    
        }
    }

    private void DauChamDuoi()
    {
        for (int i = _segments.Count - 1; i > 2; i--)
        {
            if(this.transform.position.x == _segments[i].position.x &&
                this.transform.position.y == _segments[i].position.y)
            {
                Debug.Log("DauChamDuoi");
                state = State.Lose;
            }
        }
    }

    public void SetWinNotice(bool isOpen)
    {
        WinNoticeText.SetActive(true);
        LoseNoticeText.SetActive(false);
        WinNotice.SetActive(isOpen);
    }

    public void SetLoseNotice(bool isOpen)
    {
        LoseNoticeText.SetActive(true);
        WinNoticeText.SetActive(false);
        WinNotice.SetActive(isOpen);
    }   
}
