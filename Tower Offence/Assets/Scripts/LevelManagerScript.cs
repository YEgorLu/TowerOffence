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
    CellScript[,] allCells = new CellScript[11, 18];

    float spriteSizeX, spriteSizeY;
    int currentWayX, currentWayY;
    GameObject startCell;
    string[] path;

    void Start()
    {
        spriteSizeX = cellPref.GetComponent<SpriteRenderer>().bounds.size.x;
        spriteSizeY = cellPref.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    GameObject CreateCell(bool isRoad, Sprite spr, int x, int y, Vector3 wV)
    {
        var tmpCell = Instantiate(cellPref);

        tmpCell.transform.SetParent(cellParent, false);
        tmpCell.GetComponent<SpriteRenderer>().sprite = spr;

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
        return tmpCell;
    }

    public void CreateLevel(int mapNumber)
    {
        DeletePreviousLevel();
        LoadPath(mapNumber);

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
                    spr = tileSprites[0];
                }
                else
                    spr = tileSprites[pos];
                var isRoad = spr == tileSprites[1];

                var cell = CreateCell(isRoad, spr, j, i, worldVec);
                DeployTower(cell, path[i].ToCharArray()[j].ToString());
            }
        LoadWayPoints();
    }

    private void LoadPath(int mapNumber)
    {
        var tmpText = Resources.Load<TextAsset>(@"Maps\Map" + mapNumber);
        var tmpStr = tmpText.text.Replace(System.Environment.NewLine, string.Empty);
        path = tmpStr.Split(',');
    }

    public void DeletePreviousLevel()
    {
        for (var i = 0; i < cellParent.childCount; i++)
            Destroy(cellParent.GetChild(i).gameObject);
    }

    private void DeployTower(GameObject cell, string towerTypeSymbol)
    {
        if(!SymbToType.TryParse(towerTypeSymbol, out SymbToType towerType) || (int)towerType <= 1)
            return;
        
        var tmpTower = Instantiate(towerPref);
        tmpTower.transform.SetParent(cell.transform);
        tmpTower.GetComponent<TowerScript>().selfType = (TowerType)towerType;
        tmpTower.transform.position = new Vector3(cell.transform.position.x + spriteSizeX/2, cell.transform.position.y - spriteSizeY/2);
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
        tmpBilding.transform.SetParent(endPoint.transform);
        tmpBilding.transform.position = new Vector3(endPoint.transform.position.x + spriteSizeX/2, endPoint.transform.position.y - spriteSizeY/2, -1);
    }

    enum SymbToType
    {
        N = 2,
        S,
        A
    }
}
