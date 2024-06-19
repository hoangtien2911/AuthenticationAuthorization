using EquipmentsBO.Models;
using EquipmentsDAO.IBaseDAO;
namespace EquipmentsDAO;

public class RoomDAO : IBaseDAO<Room>
{
    private static RoomDAO instance;
    private static readonly object instanceLock = new object();

    public static RoomDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new RoomDAO();
                }
                return instance;
            }
        }
    }
}
