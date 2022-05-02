using ContosoPizza.Models;
using ContosoPizza.Data;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;

public class PizzaService
{
    private readonly PizzaContext context;

    public PizzaService(PizzaContext pizzaContext)
    {
        context = pizzaContext;
    }

    public IEnumerable<Pizza> GetAll()
    {
        return context.Pizzas
                      .AsNoTracking()
                      .ToList();

    }

    public Pizza? GetById(int id)
    {
        return context.Pizzas
                      .Include(p => p.Toppings)
                      .Include(p => p.Sauce)
                      .AsNoTracking()
                      .SingleOrDefault(p => p.Id == id);
    }

    public Pizza? Create(Pizza newPizza)
    {
        context.Pizzas.Add(newPizza);
        context.SaveChanges();

        return newPizza;
    }

    public void AddTopping(int PizzaId, int ToppingId)
    {
        var pizzaToUpdate = context.Pizzas.Find(PizzaId);
        var toppingToAdd = context.Toppings.Find(ToppingId);

        if (pizzaToUpdate is null || toppingToAdd is null)
        {
            throw new NullReferenceException("Pizza ou molho não existem!");
        }

        if (pizzaToUpdate.Toppings is null)
        {
            pizzaToUpdate.Toppings = new List<Topping>();
        }

        pizzaToUpdate.Toppings.Add(toppingToAdd);

        context.Pizzas.Update(pizzaToUpdate);
        context.SaveChanges();


    }

    public void UpdateSauce(int PizzaId, int SauceId)
    {
        var pizzaToUpdate = context.Pizzas.Find(PizzaId);
        var sauceToUpdate = context.Sauces.Find(SauceId);

        if (pizzaToUpdate is null || sauceToUpdate is null)
        {
            throw new NullReferenceException("Pizza ou molho não existe!");
        }

        pizzaToUpdate.Sauce = sauceToUpdate;
        context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var pizzaToDelete = context.Pizzas.Find(id);

        if (pizzaToDelete is not null)
        {
            context.Pizzas.Remove(pizzaToDelete);
            context.SaveChanges();
        }

    }
}