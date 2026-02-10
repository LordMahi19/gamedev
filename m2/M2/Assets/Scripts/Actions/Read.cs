using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Read")]
public class Read : Action
{
    public override void RespondToInput(GameController controller, string noun)
    {
        // Read items in room.
        if (ReadItem(controller, controller.player.currentLocation.items, noun))
        {
            return;
        }

        // Read item in inventory
        if (ReadItem(controller, controller.player.inventory, noun))
        {
            return;
        }
        controller.currentText.text = "There is no " + noun + " here to read.";
    }

    private bool ReadItem(GameController controller, List<Item> items, string noun)
    {
        foreach (Item item in items)
        {
            if (item.itemName == noun)
            {
                if (controller.player.CanReadItem(controller, item))
                {
                    if (item.InteractionWith(controller, "read"))
                    {
                        return true;
                    }
                }
                controller.currentText.text = "The " + noun + " has nothing to read.";
                return true;
            }
        }
        return false;
    }
}
