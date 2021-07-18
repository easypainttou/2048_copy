using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GamePlay : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private Vector3 downPosition;
    private Vector3 upPosition;
    private int[,] gridMap = new int[4, 4];
    private Transform[] gridTransform;

    private List<int> emptyGrid = new List<int>();


    void Awake()
    {
        gridTransform = GetComponentsInChildren<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        downPosition = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        upPosition = Input.mousePosition;

        MoveType moveType = GetMoveType();


        //todo

        CreateRandomTile();


    }

    public MoveType GetMoveType()
    {
        if (Mathf.Abs(upPosition.x - downPosition.x) < Mathf.Abs(upPosition.y - downPosition.y))
        {
            if (upPosition.y > downPosition.y)
            {
                return MoveType.Up;
            }
            else
            {
                return MoveType.Down;
            }
        }
        else
        {
            if (upPosition.x > downPosition.x)
            {
                return MoveType.Right;
            }
            else
            {
                return MoveType.Left;
            }
        }
    }

    public void CreateRandomTile()
    {
        emptyGrid.Clear();
        int emptyCount = 0;
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                if (gridMap[i, j] == 0)
                {
                    emptyGrid.Add(i * 4 + j);
                    ++emptyCount;
                }
            }
        }
        int selectPos = Random.Range(0, emptyCount);
        int num = 2;
        if (Random.Range(0, 10) < 1)
        {
            num = 4;
        }

        CreateTile(gridTransform[selectPos], num);

    }

    public void CreateTile(Transform grid, int num)
    {
        Debug.Log(grid);
        Debug.Log(Resources.Load<GameObject>("Prefabs/Tile"));

        GameObject tileGo = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Tile"), grid);
        tileGo.GetComponentInChildren<Text>().text = num.ToString();
    }
}
