using Windows.Gaming.Input;

Gamepad gamepad = null;
bool connected = true;

while (gamepad == null)
{
    Console.WriteLine("BRANCHEZ MANETTE VITE VITE VITE VITE");

    for (int i = 0; i < RawGameController.RawGameControllers.Count; i++)
    {
        RawGameController rawGameController = RawGameController.RawGameControllers[i];
        gamepad = Gamepad.FromGameController(rawGameController);
        if (gamepad != null) 
        {
            break;
        }
    }
}

while(connected)
{
    GamepadReading reading = gamepad.GetCurrentReading();

    if (GamepadButtons.A == (reading.Buttons & GamepadButtons.A))
    {
        Console.WriteLine("A pressed !");
    } else {
        Console.WriteLine("NOTHINGU");
    }
}

Console.WriteLine("Hello World !");