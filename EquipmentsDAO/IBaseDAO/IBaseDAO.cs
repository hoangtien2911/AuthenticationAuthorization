using EquipmentsBO.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EquipmentsDAO.IBaseDAO;

public class IBaseDAO<T> where T : class
{
    private readonly Equipments2024DBContext _context;
    private readonly DbSet<T> _dbSet;

    public IBaseDAO()
    {
        _context = new Equipments2024DBContext();
        _dbSet = _context.Set<T>();
    }

    public IQueryable<T> Get()
    {
        return _dbSet.AsQueryable();
    }

    public List<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T? GetById(int id)
    {
        return _dbSet.Find(id);
    }
    public bool Create(T entity)
    {
        try
        {
            _context.ChangeTracker.Clear();
            var tracker = _dbSet.Add(entity);
            _context.SaveChanges();
            tracker.State = EntityState.Detached;
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
    public T? Update(T entity)
    {
        try
        {
            _context.ChangeTracker.Clear();
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
            tracker.State = EntityState.Detached;
            return entity;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public bool Remove(T entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChanges();
        return true;
    }
}