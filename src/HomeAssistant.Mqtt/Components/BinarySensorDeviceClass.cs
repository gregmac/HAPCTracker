namespace HomeAssistant.Mqtt.Components
{
    /// <summary>
    /// See https://www.home-assistant.io/integrations/binary_sensor/#device-class
    /// </summary>
    public enum BinarySensorDeviceClass
    {
        none,
        battery,
        battery_charging,
        cold,
        connectivity,
        door,
        garage_door,
        gas,
        heat,
        light,
        @lock,
        moisture,
        motion,
        moving,
        occupancy,
        opening,
        plug,
        power,
        presence,
        problem,
        safety,
        smoke,
        sound,
        vibration,
        window,
    }
}
