using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerScript : MonoBehaviour
{
    public int fieldWidth, fieldHeight;
    public GameObject cellPref;
    public Transform cellParent;
    public GameObject towerPref;
    public GameObject endBildingPref;
    public Sprite[] tileSprites = new Sprite[2];
    public List<GameObject> wayPoints = new List<GameObject>();
    //GameObject[,] allCells = new GameObject[11, 18]; // сделать CellScript
    CellScript[,] allCells = new CellScript[11, 18]; // сделать CellScript

    float spriteSizeX, spriteSizeY;
    int currentWayX, currentWayY;
    GameObject startCell;

    string[] path1 =
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
    string[] path =
        { "000000000000000000",
          "000000000000000000",
          "000000000000000000",
          "000000000000000000",
          "111111111111111111",
          "N00000000000000000",
          "000000000000000000",
          "000000000000000000",
          "000000000000000000",
          "000000000000000000",
          "000000000000000000" };

    void Start()
    {
        spriteSizeX = cellPref.GetComponent<SpriteRenderer>().bounds.size.x;
        spriteSizeY = cellPref.GetComponent<SpriteRenderer>().bounds.size.y;
        CreateLevel(); 
    }

    void CreateCell(bool isRoad, Sprite spr, int x, int y, Vector3 wV)
    {
        var tmpCell = Instantiate(cellPref);

        tmpCell.transform.SetParent(cellParent, false);
        tmpCell.GetComponent<SpriteRenderer>().sprite = spr;

        //var spriteSizeX = tmpCell.GetComponent<SpriteRenderer>().bounds.size.x;
        //var spriteSizeY = tmpCell.GetComponent<SpriteRenderer>().bounds.size.y;

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

        allCells[y, x] = tmpCell.GetComponent<CellScript>();
    }

    public void CreateLevel()
    {
        wayPoints.Clear();
        startCell = null;

        var worldVec = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));

        for (var i = 0; i < fieldHeight; i++)
            for (var j = 0; j < fieldWidth; j++)
            {
                var isNum = int.TryParse(path[i].ToCharArray()[j].ToString(), out var pos);
                Sprite spr;
                if(!isNum)
                {
                    DeployTower(path[i].ToCharArray()[j].ToString());
                    spr = tileSprites[0];
                }
                else
                    spr = tileSprites[pos];
                var isRoad = spr == tileSprites[1];

                CreateCell(isRoad, spr, j, i, worldVec);
            }
        LoadWayPoints();
    }

    private void DeployTower(string type)
    {
        /*var tmpCell = Instantiate(towerPref);
        GameControllerScript.All
        tmpCell.transform.SetParent(cellParent, false);
        tmpCell.GetComponent<SpriteRenderer>().sprite = spr;

        var spriteSizeX = tmpCell.GetComponent<SpriteRenderer>().bounds.size.x;
        var spriteSizeY = tmpCell.GetComponent<SpriteRenderer>().bounds.size.y;

        tmpCell.transform.position = new Vector3(wV.x + spriteSizeX * x, wV.y + spriteSizeY * -y);*/
    }

    void LoadWayPoints()
    {
        GameObject currentWay;
        wayPoints.Add(startCell);

        while (true)
        {
            currentWay = null;

            if (currentWayX > 0
                && allCells[currentWayY, currentWayX - 1].isRoad
                && !wayPoints.Exists(x => x == allCells[currentWayY, currentWayX - 1].gameObject))
            {
                currentWay = allCells[currentWayY, currentWayX - 1].gameObject;
                currentWayX--;
            }
            else if (currentWayX < fieldWidth - 1
                && allCells[currentWayY, currentWayX + 1].isRoad
                && !wayPoints.Exists(x => x == allCells[currentWayY, currentWayX + 1].gameObject))
            {
                currentWay = allCells[currentWayY, currentWayX + 1].gameObject;
                currentWayX++;
            }
            else if (currentWayY > 0
                && allCells[currentWayY - 1, currentWayX].isRoad
                && !wayPoints.Exists(x => x == allCells[currentWayY - 1, currentWayX].gameObject))
            {
                currentWay = allCells[currentWayY - 1, currentWayX].gameObject;
                currentWayY--;
            }
            else if (currentWayY < fieldHeight - 1
                && allCells[currentWayY + 1, currentWayX].isRoad
                && !wayPoints.Exists(x => x == allCells[currentWayY + 1, currentWayX].gameObject))
            {
                currentWay = allCells[currentWayY + 1, currentWayX].gameObject;
                currentWayY++;
            }
            else
                break;
            wayPoints.Add(currentWay);
        }
        DeployEndBilding(wayPoints[wayPoints.Count - 1]);
    }

    private void DeployEndBilding(GameObject endPoint)
    {
        var tmpBilding = Instantiate(endBildingPref);
        //tmpBilding.GetComponent<SpriteRenderer>().sprite = endSpr;
        tmpBilding.transform.position = new Vector3(endPoint.transform.position.x + spriteSizeX/2, endPoint.transform.position.y - spriteSizeY/2, -1);
    }
}
