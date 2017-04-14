using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject bunker;
    [SerializeField]
    private GameObject bunkerBtn;
    [SerializeField]
    private GameObject bunkerUPBtn;
    [SerializeField]
    private GameObject teslaTower;
    [SerializeField]
    private GameObject teslaTowerBtn;
    [SerializeField]
    private GameObject teslaTowerUPBtn;
    [SerializeField]
    private GameObject artilleryTowerBtn;
    [SerializeField]
    private GameObject selection;

    private GameObject objectSelected;
    private Button buttonComp;

    // Można by tu użyć funkcje, arrays i pętle do uproszczenia kodu.

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (hit.collider.tag == "BuildZone" || hit.collider.tag == "UpgradableTower")
                {
                    objectSelected = hit.collider.gameObject;

                    if (GameObject.FindWithTag("Selection"))
                        Destroy(GameObject.FindWithTag("Selection"));
                    GameObject newSelection = Instantiate(selection);
                    newSelection.transform.position = objectSelected.transform.position;

                    if (hit.collider.tag == "BuildZone")
                    {
                        if (!bunkerBtn.GetComponent<Button>().IsInteractable())
                            bunkerBtn.GetComponent<Button>().interactable = true;
                        if (bunkerUPBtn.GetComponent<Button>().IsInteractable())
                            bunkerUPBtn.GetComponent<Button>().interactable = false;
                        if (!teslaTowerBtn.GetComponent<Button>().IsInteractable())
                            teslaTowerBtn.GetComponent<Button>().interactable = true;
                        if (teslaTowerUPBtn.GetComponent<Button>().IsInteractable())
                            teslaTowerUPBtn.GetComponent<Button>().interactable = false;
                        if (!artilleryTowerBtn.GetComponent<Button>().IsInteractable())
                            artilleryTowerBtn.GetComponent<Button>().interactable = true;
                    }
                    else if (hit.collider.name == "bunkerTower(Clone)")
                    {
                        if (bunkerBtn.GetComponent<Button>().IsInteractable())
                            bunkerBtn.GetComponent<Button>().interactable = false;
                        if (!bunkerUPBtn.GetComponent<Button>().IsInteractable())
                            bunkerUPBtn.GetComponent<Button>().interactable = true;
                        if (teslaTowerBtn.GetComponent<Button>().IsInteractable())
                            teslaTowerBtn.GetComponent<Button>().interactable = false;
                        if (teslaTowerUPBtn.GetComponent<Button>().IsInteractable())
                            teslaTowerUPBtn.GetComponent<Button>().interactable = false;
                        if (artilleryTowerBtn.GetComponent<Button>().IsInteractable())
                            artilleryTowerBtn.GetComponent<Button>().interactable = false;
                    }
                    else if (hit.collider.name == "teslaTower(Clone)")
                    {
                        if (bunkerBtn.GetComponent<Button>().IsInteractable())
                            bunkerBtn.GetComponent<Button>().interactable = false;
                        if (bunkerUPBtn.GetComponent<Button>().IsInteractable())
                            bunkerUPBtn.GetComponent<Button>().interactable = false;
                        if (teslaTowerBtn.GetComponent<Button>().IsInteractable())
                            teslaTowerBtn.GetComponent<Button>().interactable = false;
                        if (!teslaTowerUPBtn.GetComponent<Button>().IsInteractable())
                            teslaTowerUPBtn.GetComponent<Button>().interactable = true;
                        if (artilleryTowerBtn.GetComponent<Button>().IsInteractable())
                            artilleryTowerBtn.GetComponent<Button>().interactable = false;
                    }
                }
                else
                    resetSelection();
                // hit.collider.tag = "BuildZoneFull";
            }
        }
               
    }

    public void PickTower()
    {
        GameObject buttonPressed = EventSystem.current.currentSelectedGameObject;
        GameObject towerPicked = buttonPressed.GetComponent<TowerBtn>().TowerType;

        if (buttonPressed == bunkerBtn)
            instantiateTower(towerPicked);
        else if (buttonPressed == bunkerUPBtn)
            upgradeTower(towerPicked);
        else if (buttonPressed == teslaTowerBtn)
            instantiateTower(towerPicked);
        else if (buttonPressed == teslaTowerUPBtn)
            upgradeTower(towerPicked);
        else if (buttonPressed == artilleryTowerBtn)
            instantiateTower(towerPicked);

        resetSelection();
    }

    private void instantiateTower(GameObject TowerPicked)
    {
        GameObject newTower = Instantiate(TowerPicked);
        newTower.transform.position = objectSelected.transform.position;
    }

    private void upgradeTower(GameObject TowerPicked)
    {
        GameObject newTower = Instantiate(TowerPicked);
        newTower.transform.position = objectSelected.transform.position;
        Destroy(objectSelected);
    }

    private void resetSelection()
    {
        if (bunkerBtn.GetComponent<Button>().IsInteractable())
            bunkerBtn.GetComponent<Button>().interactable = false;
        if (bunkerUPBtn.GetComponent<Button>().IsInteractable())
            bunkerUPBtn.GetComponent<Button>().interactable = false;
        if (teslaTowerBtn.GetComponent<Button>().IsInteractable())
            teslaTowerBtn.GetComponent<Button>().interactable = false;
        if (teslaTowerUPBtn.GetComponent<Button>().IsInteractable())
            teslaTowerUPBtn.GetComponent<Button>().interactable = false;
        if (artilleryTowerBtn.GetComponent<Button>().IsInteractable())
            artilleryTowerBtn.GetComponent<Button>().interactable = false;
        if (GameObject.FindWithTag("Selection"))
            Destroy(GameObject.FindWithTag("Selection"));
    }
}
