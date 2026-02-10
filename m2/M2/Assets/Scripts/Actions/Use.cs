using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Use")]
public class Use : Action
{
    public override void RespondToInput(GameController controller, string noun)
    {
        // Use items in room.
        if (UseItem(controller, controller.player.currentLocation.items, noun))
        {
            return;
        }

        // Use item in inventory
        if (UseItem(controller, controller.player.inventory, noun))
        {
            return;
        }
        controller.currentText.text = "there is no " + noun + " here.";
    }

    private bool UseItem(GameController controller, List<Item> items, string noun)
    {
        foreach (Item item in items)
        {
            if (item.itemName == noun)
            {
                if (controller.player.CanUseItem(controller, item))
                {
                    if (item.InteractionWith(controller, "use"))
                    {
                        return true;
                    }
                }
                controller.currentText.text = "The " + noun + " does nothing.";
                return true;
            }
        }
        return false;
    }
}
