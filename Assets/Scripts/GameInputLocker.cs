using UnityEngine;

public static class GameInputLocker
{
    public static bool InputLocked = false;

    public static bool GetKey(KeyCode kc) {
        if (InputLocked) {
            return false;
        }

        return Input.GetKey(kc);
    }

    public static void SetInputLock(bool locked) {
        InputLocked = locked;
    }
}
