using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerScript : MonoBehaviour
{
    public int fieldWidth, fieldHeight;
    public GameObject cellPref;
    public Transform cellParent;
    public Sprite[] tileSprites = new Sprite[2];
    public List<GameObject> wayPoints = new List<GameObject>();
    GameObject[,] allCells = new GameObject[11, 18];

    int currentWayX, currentWayY;
    GameObject startCell;

    string[] path =
        { "000000000000000000",
          "111000000000000000",
          "001000000000000000",
          "001000001111100000",
          "001000001000100000",
          "001000001000111111",
          "001111001000000000",
          "000001111000000000",
          "000000000000000000",
          "000000000000000000",
          "000000000000000000" };

    void Start()
    {
        CreateLevel();
        LoadWayPoints();
    }

    void CreateCell(bool isRoad, Sprite spr, int x, int y, Vector3 wV)
    {
        var tmpCell = Instantiate(cellPref);

        tmpCell.transform.SetParent(cellParent, false);
        tmpCell.GetComponent<SpriteRenderer>().sprite = spr;

        var spriteSizeX = tmpCell.GetComponent<SpriteRenderer>().bounds.size.x;
        var spriteSizeY = tmpCell.GetComponent<SpriteRenderer>().bounds.size.y;

        tmpCell.transform.position = new Vector3(wV.x + spriteSizeX * x, wV.y + spriteSizeY * -y);

        if (isRoad)
        {
            tmpCell.GetComponent<CellScript>().isRoad = true;
            if (startCell == null)
            {
                startCell = tmpCell;
                currentWayX = x;
                currentWayY = y;
            }
        }

        allCells[y, x] = tmpCell;
    }

    void CreateLevel()
    {
        var worldVec = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));

        for (var i = 0; i < fieldHeight; i++)
            for (var j = 0; j < fieldWidth; j++)
            {
                var spr = tileSprites[int.Parse(path[i].ToCharArray()[j].ToString())];
                var isRoad = spr == tileSprites[1];

                CreateCell(isRoad, spr, j, i, worldVec);
            }
    }

    void LoadWayPoints()
    {
        GameObject currentWay;
        wayPoints.Add(startCell);

        while (true)
        {
            currentWay = null;

            if (currentWayX > 0
                && allCells[currentWayY, currentWayX - 1].GetComponent<CellScript>().isRoad
                && !wayPoints.Exists(x => x == allCells[currentWayY, currentWayX - 1]))
            {
                currentWay = allCells[currentWayY, currentWayX - 1];
                currentWayX--;
            }
            else if (currentWayX < fieldWidth - 1
                && allCells[currentWayY, currentWayX + 1].GetComponent<CellScript>().isRoad
                && !wayPoints.Exists(x => x == allCells[currentWayY, currentWayX + 1]))
            {
                currentWay = allCells[currentWayY, currentWayX + 1];
                currentWayX++;
            }
            else if (currentWayY > 0
                && allCells[currentWayY - 1, currentWayX].GetComponent<CellScript>().isRoad
                && !wayPoints.Exists(x => x == allCells[currentWayY - 1, currentWayX]))
            {
                currentWay = allCells[currentWayY - 1, currentWayX];
                currentWayY--;
            }
            else if (currentWayY < fieldHeight - 1
                && allCells[currentWayY + 1, currentWayX].GetComponent<CellScript>().isRoad
                && !wayPoints.Exists(x => x == allCells[currentWayY + 1, currentWayX]))
            {
                currentWay = allCells[currentWayY + 1, currentWayX];
                currentWayY++;
            }
            else
                break;
            wayPoints.Add(currentWay);
        }
    }
}
