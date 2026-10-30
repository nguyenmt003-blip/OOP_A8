/* 
Nguyễn Minh Tuấn
202419010
*/

// điều khiển máy chiếu
public class ProjectorRemote : TeachingDevice
{
    public int BatteryLevel { get; private set; }

    public ProjectorRemote(string id, string name, int initialBattery = 100)
        : base(id, name)
    {
        // kiểm tra mức pin hợp lệ
        if (initialBattery < 0 || initialBattery > 100)
        {
            throw new InvalidDeviceReadingException(id, initialBattery);
        }

        BatteryLevel = initialBattery;
    }

    // cập nhật pin và phát cảnh báo nếu pin yếu hoặc hết
    public void UpdateBatteryLevel(int newLevel)
    {
        if (newLevel < 0 || newLevel > 100)
        {
            throw new InvalidDeviceReadingException(Id, newLevel);
        }

        BatteryLevel = newLevel;

        if (newLevel == 0)
        {
            RaiseAlert("Mức pin máy chiếu bằng 0%.", AlertSeverity.Critical);
        }
        else if (newLevel < 20)
        {
            RaiseAlert($"Mức pin máy chiếu giảm xuống dưới 20% ({newLevel}%).", AlertSeverity.Warning);
        }
    }
}
