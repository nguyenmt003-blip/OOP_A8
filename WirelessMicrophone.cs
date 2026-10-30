/* 
Nguyễn Minh Tuấn
202419010
*/

// micro không dây
public class WirelessMicrophone : TeachingDevice
{
    public int BatteryLevel { get; private set; }
    public int SignalQuality { get; private set; }

    public WirelessMicrophone(string id, string name, int initialBattery = 100, int initialSignalQuality = 100)
        : base(id, name)
    {
        // kiểm tra giá trị pin và chất lượng tín hiệu ban đầu
        if (initialBattery < 0 || initialBattery > 100)
        {
            throw new InvalidDeviceReadingException(id, initialBattery);
        }

        if (initialSignalQuality < 0 || initialSignalQuality > 100)
        {
            throw new InvalidDeviceReadingException(id, initialSignalQuality);
        }

        BatteryLevel = initialBattery;
        SignalQuality = initialSignalQuality;
    }

    // cập nhật pin micro
    public void UpdateBatteryLevel(int newLevel)
    {
        if (newLevel < 0 || newLevel > 100)
        {
            throw new InvalidDeviceReadingException(Id, newLevel);
        }

        BatteryLevel = newLevel;

        if (newLevel == 0)
        {
            RaiseAlert("Mức pin micro bằng 0%.", AlertSeverity.Critical);
        }
        else if (newLevel < 30)
        {
            RaiseAlert($"Mức pin micro dưới 30% ({newLevel}%).", AlertSeverity.Warning);
        }
    }

    // cập nhật chất lượng tín hiệu micro
    public void UpdateSignalQuality(int newQuality)
    {
        if (newQuality < 0 || newQuality > 100)
        {
            throw new InvalidDeviceReadingException(Id, newQuality);
        }

        SignalQuality = newQuality;

        if (newQuality == 0)
        {
            RaiseAlert("Chất lượng tín hiệu micro bằng 0%.", AlertSeverity.Critical);
        }
        else if (newQuality < 40)
        {
            RaiseAlert($"Chất lượng tín hiệu micro dưới 40% ({newQuality}%).", AlertSeverity.Warning);
        }
    }
}
