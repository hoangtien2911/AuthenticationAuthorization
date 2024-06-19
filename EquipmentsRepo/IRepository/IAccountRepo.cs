using EquipmentsBO.Models;
using System.Linq.Expressions;

namespace EquipmentsRepo.IRepository;

public interface IAccountRepo
{
    Account? GetByUsernamePassword(string username, string password);
    Account? GetById(int id);
    List<Account> GetAll();
    bool Create(Account entity);
    Account? Update(Account entity);
    bool Remove(Account entity);
}
