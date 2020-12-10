using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    private class WeaponUpgrade {
        public Weapon weapon;
        public int cost;
        public int initialAmmo;
        public int ammoBatchSize;
        public int ammoCost;

        public WeaponUpgrade(Weapon upgrade, int upgradeCost, int initAmmo, int ammoSize, int ammoPrice) {
            weapon = upgrade;
            cost = upgradeCost;
            initialAmmo = initAmmo;
            ammoCost = ammoPrice;
            ammoBatchSize = ammoSize;
        }
    }

    private class UtilityUpgrade {
        public string name;
        public int cost;
        public List<string> prerequisites;

        public UtilityUpgrade(string upgradeName, int price, List<string> prereqs = null) {
            if (prereqs == null)
                prereqs = new List<string>();

            name = upgradeName;
            cost = price;
            prerequisites = prereqs;
        }
    }

    List<WeaponUpgrade> availableWeaponUpgrades;
    List<UtilityUpgrade> availableUtilityUpgrades;

    void Start()
    {

    }

    void Awake() {
        availableWeaponUpgrades = new List<WeaponUpgrade>() {
            new WeaponUpgrade(new StandardWeapon(null), 50, -1, -1, -1),
            new WeaponUpgrade(new ShotgunWeapon(null), 100, 5, 3, 50),
            new WeaponUpgrade(new HomingMissileWeapon(null), 200, 3, 1, 50)
        };

        availableUtilityUpgrades = new List<UtilityUpgrade>() {
            new UtilityUpgrade("Speed Increase I", 150),
            new UtilityUpgrade("Speed Increase II", 150, new List<string>{"Speed Increase I"})
        };


        PopulateWeaponUpgradeMenu();
        PopulateUtilityUpgradeMenu();
    }

    void Update()
    {
        
    }

    public void PopulateWeaponUpgradeMenu() {
        GameObject weaponUpgradeList = GameObject.Find("WeaponUpgradeListContent");
        foreach(var upgrade in availableWeaponUpgrades) {
            var item = Instantiate(Resources.Load("Prefabs/WeaponUpgradeListItem"), weaponUpgradeList.transform) as GameObject;
            var itemText = item.transform.GetChild(0);
            var itemBuyButton = item.transform.GetChild(1);

            itemText.GetComponent<Text>().text = upgrade.weapon.name;
            if (playerHasWeapon(upgrade.weapon.name)) {
                string playerWeaponAmmo = Mathf.Max(getPlayerWeaponAmmo(upgrade.weapon.name), -1).ToString();
                if (playerWeaponAmmo == "-1")
                    playerWeaponAmmo = "∞";
                
                itemText.GetComponent<Text>().text = upgrade.weapon.name + " Ammo (" + playerWeaponAmmo + ")";

                if (upgrade.ammoCost == -1) {
                    itemBuyButton.transform.GetChild(0).GetComponent<Text>().text = "Infinite Ammo";
                } else {
                    itemBuyButton.transform.GetChild(0).GetComponent<Text>().text = "Buy " + upgrade.ammoBatchSize.ToString() + ": " + upgrade.ammoCost.ToString() + " pts";
                }
            } else {
                itemBuyButton.transform.GetChild(0).GetComponent<Text>().text = "Buy: " + upgrade.cost.ToString() + " pts";
            }

            var button =itemBuyButton.GetComponent<Button>();
            itemBuyButton.GetComponent<Button>().onClick.AddListener(() => BuyWeaponUpgrade(itemText, itemBuyButton, upgrade.weapon.name));
        }
    }

    public void PopulateUtilityUpgradeMenu() {
        GameObject utilityUpgradeList = GameObject.Find("UtilityUpgradeListContent");

        foreach (Transform child in utilityUpgradeList.transform)
            Destroy(child.gameObject);

        foreach(var upgrade in availableUtilityUpgrades) {
            if (!PersistentData.playerUpgrades.Contains(upgrade.name)) {
                // verify prerequisites are owned
                bool allPrereqsOwned = true;
                foreach(string prereq in upgrade.prerequisites) {
                    if (!PersistentData.playerUpgrades.Contains(prereq)) {
                        allPrereqsOwned = false;
                        break;
                    }
                }

                if (allPrereqsOwned) {
                    var item = Instantiate(Resources.Load("Prefabs/WeaponUpgradeListItem"), utilityUpgradeList.transform) as GameObject;
                    var itemText = item.transform.GetChild(0);
                    var itemBuyButton = item.transform.GetChild(1);

                    itemText.GetComponent<Text>().text = upgrade.name;
                    itemBuyButton.transform.GetChild(0).GetComponent<Text>().text = "Buy: " + upgrade.cost.ToString() + " pts";

                    var button =itemBuyButton.GetComponent<Button>();
                    itemBuyButton.GetComponent<Button>().onClick.AddListener(() => BuyUtilityUpgrade(itemText, itemBuyButton, upgrade.name));
                }
            }
        }
    }

    private bool playerHasWeapon(string weaponName) {
        return PersistentData.playerWeapons.Find(weapon => weapon.name == weaponName) != null;
    }

    private int getPlayerWeaponAmmo(string weaponName) {
        if (!playerHasWeapon(weaponName))
            return 0;

        return PersistentData.playerWeapons.Find(weapon => weapon.name == weaponName).ammoRemaining;
    }

    private void markListItemOwned(Transform clickedItemText, Transform buttonTransform, WeaponUpgrade upgrade) {
        string playerWeaponAmmo = getPlayerWeaponAmmo(upgrade.weapon.name).ToString();
        clickedItemText.GetComponent<Text>().text = upgrade.weapon.name + " Ammo (" + playerWeaponAmmo + ")";

        buttonTransform.GetChild(0).GetComponent<Text>().text = "Buy " + upgrade.ammoBatchSize.ToString() + ": " + upgrade.ammoCost.ToString() + " pts";
    }

    private void updateListItemAmmoOwned(Transform clickedItemText, WeaponUpgrade upgrade) {
        string playerWeaponAmmo = getPlayerWeaponAmmo(upgrade.weapon.name).ToString();
        clickedItemText.GetComponent<Text>().text = upgrade.weapon.name + " Ammo (" + playerWeaponAmmo + ")";
    }

    public void BuyWeaponUpgrade(Transform clickedItemText, Transform clickedButtonTransform, string weaponName) {
        WeaponUpgrade upgrade = availableWeaponUpgrades.Find(item => item.weapon.name == weaponName);
        if (upgrade == null)
            return;

        if (playerHasWeapon(weaponName)) {
            // we are trying to buy ammo

            // exit if weapon has infinite ammo
            if (upgrade.ammoCost == -1)
                return;

            if (PersistentData.numPoints >= upgrade.ammoCost) {
                var playerWeapon = PersistentData.playerWeapons.Find(weapon => weapon.name == upgrade.weapon.name);
                playerWeapon.ammoRemaining += upgrade.ammoBatchSize;
                updateListItemAmmoOwned(clickedItemText, upgrade);

                PersistentData.numPoints -= upgrade.ammoCost;
            }
        } else {
            // we are buying a new weapon

            if (PersistentData.numPoints >= upgrade.cost) {
                if (playerHasWeapon(weaponName))
                    return;

                Weapon newWeapon = upgrade.weapon;
                newWeapon.ammoRemaining = upgrade.initialAmmo;

                PersistentData.playerWeapons.Add(newWeapon);
                markListItemOwned(clickedItemText, clickedButtonTransform, upgrade);

                PersistentData.numPoints -= upgrade.cost;
            }
        }
    }

    void BuyUtilityUpgrade(Transform clickedItemText, Transform clickedButtonTransform, string upgradeName) {
        UtilityUpgrade upgrade = availableUtilityUpgrades.Find(item => item.name == upgradeName);
        if (upgrade == null)
            return;

        if (PersistentData.numPoints < upgrade.cost)
            return;

        PersistentData.playerUpgrades.Add(upgrade.name);
        PersistentData.numPoints -= upgrade.cost;

        PopulateUtilityUpgradeMenu();
    }
}
