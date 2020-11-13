using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    private class WeaponUpgrade {
        public Weapon weapon;
        public int cost;

        public WeaponUpgrade(Weapon upgrade, int upgradeCost) {
            weapon = upgrade;
            cost = upgradeCost;
        }
    }

    List<WeaponUpgrade> availableWeaponUpgrades;

    void Start()
    {

    }

    void Awake() {
        availableWeaponUpgrades = new List<WeaponUpgrade>() {
            new WeaponUpgrade(new StandardWeapon(null), 50),
            new WeaponUpgrade(new ShotgunWeapon(null), 100),
            new WeaponUpgrade(new HomingMissileWeapon(null), 200)
        };

        PopulateWeaponUpgradeMenu();
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
                itemBuyButton.transform.GetChild(0).GetComponent<Text>().text = "Owned!";
            } else {
                itemBuyButton.transform.GetChild(0).GetComponent<Text>().text = "Buy: " + upgrade.cost.ToString() + " pts";
            }

            var button =itemBuyButton.GetComponent<Button>();
            itemBuyButton.GetComponent<Button>().onClick.AddListener(() => BuyWeaponUpgrade(itemBuyButton, upgrade.weapon.name));
        }
    }

    private bool playerHasWeapon(string weaponName) {
        return PersistentData.playerWeapons.Find(weapon => weapon.name == weaponName) != null;
    }

    private void markListItemOwned(Transform buttonTransform) {
        buttonTransform.GetChild(0).GetComponent<Text>().text = "Owned!";
    }

    public void BuyWeaponUpgrade(Transform clickedButtonTransform, string weaponName) {
        WeaponUpgrade upgrade = availableWeaponUpgrades.Find(item => item.weapon.name == weaponName);
        if (upgrade == null)
            return;

        if (PersistentData.numPoints >= upgrade.cost) {
            if (playerHasWeapon(weaponName))
                return;

            PersistentData.playerWeapons.Add(upgrade.weapon);
            markListItemOwned(clickedButtonTransform);

            PersistentData.numPoints -= upgrade.cost;
        }
    }
}
