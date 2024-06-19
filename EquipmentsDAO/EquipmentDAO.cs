using EquipmentsBO.Models;
using EquipmentsDAO.IBaseDAO;

namespace EquipmentsDAO;

public class EquipmentDAO : IBaseDAO<Equipment>
{
    private static EquipmentDAO instance;
    private static readonly object instanceLock = new object();

    public static EquipmentDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new EquipmentDAO();
                }
                return instance;
            }
        }
    }
}
