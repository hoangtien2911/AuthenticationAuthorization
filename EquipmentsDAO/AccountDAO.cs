using EquipmentsBO.Models;
using EquipmentsDAO.IBaseDAO;
namespace EquipmentsDAO;

public class AccountDAO : IBaseDAO<Account>
{
    private static AccountDAO instance;
    private static readonly object instanceLock = new object();

    public static AccountDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }
    }
}