/* 
Nguyễn Minh Tuấn
202419010
*/

// điều khiển điều hòa
public class AirConditionerRemote : TeachingDevice
{
    public string AssignedRoom { get; }
    public bool IsResponsive { get; private set; }

    public AirConditionerRemote(string id, string name, string assignedRoom, bool isResponsive = true)
        : base(id, name)
    {
        AssignedRoom = assignedRoom;
        IsResponsive = isResponsive;
    }

    // cập nhật trạng thái phản hồi và cảnh báo nếu mất phản hồi
    public void UpdateResponseState(bool isResponsive)
    {
        if (IsResponsive && !isResponsive)
        {
            IsResponsive = false;
            RaiseAlert($"Điều khiển điều hòa tại phòng {AssignedRoom} không phản hồi.", AlertSeverity.Critical);
        }
        else
        {
            IsResponsive = isResponsive;
        }
    }
}
