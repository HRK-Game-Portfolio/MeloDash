Prevent Packed Obstacles
~~~~~~~~~~~~~~~~~~~~~~~~

.. attention:: To make the game playable, the minimal inetrval between obstacles are equal to half of character’s jump distance (12/2). Any obstacles generated within that distance will be deleted from the list.

.. code-block:: C#

    void Update() {
        
        ...

        // ------- prevent obstacles from spawning too close to each other -----
        if (children.Length >= 2) {
            var lastChild       = children[children.Length - 1].gameObject;
            var lastSecondChild = children[children.Length - 2].gameObject;

            string lastChildName       = lastChild.name;
            string lastSecondChildName = lastSecondChild.name;

            float lastChildXPos       = children[children.Length - 1].transform.position.x;
            float lastSecondChildXPos = children[children.Length - 2].transform.position.x;

            //Debug.Log(lastSecondChildName);
            //Debug.Log(lastChildName);

            /*
             * if the last obstacle spawned is to close to the last second obstacle spawned,
             * destroy the last one to prevent obstacles from spawning too close to each other
             * which left impossible situation for the player to mitigate
             */
            if (lastChildName == lastSecondChildName && lastChildName == "DownObstacle") {
                if (lastChildXPos - lastSecondChildXPos < jumpReactionDistance) {
                    Destroy(lastChild);
                }
            } else {
                if (lastChildXPos - lastSecondChildXPos < jumpReactionDistance / 2) {
                    Destroy(lastChild);
                }
            }
        }
    }

