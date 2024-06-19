using EquipmentsBO.Models;
using EquipmentsDAO;
using EquipmentsRepo.IRepository;

namespace EquipmentsRepo;

public class AccountRepo : IAccountRepo
{
    private readonly AccountDAO accountDAO = AccountDAO.Instance;
    public AccountRepo()
    {
    }
    public bool Create(Account entity)
    {
        return accountDAO.Create(entity);
    }

    public Account? GetByUsernamePassword(string username, string password)
    {
        return accountDAO.Get().Where(a => a.UserName == username && a.Password == password).FirstOrDefault();
    }

    public List<Account> GetAll()
    {
        return accountDAO.GetAll();
    }

    public Account? GetById(int id)
    {
        return accountDAO.GetById(id);
    }

    public bool Remove(Account entity)
    {
        return accountDAO.Remove(entity);
    }

    public Account? Update(Account entity)
    {
        return accountDAO.Update(entity);
    }
}
