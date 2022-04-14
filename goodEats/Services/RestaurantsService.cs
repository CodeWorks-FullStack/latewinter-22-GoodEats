using System;
using System.Collections.Generic;
using goodEats.Models;
using goodEats.Repositories;

namespace goodEats.Services
{
  public class RestaurantsService
  {
    private readonly RestaurantsRepository _repo;

    public RestaurantsService(RestaurantsRepository repo)
    {
      _repo = repo;
    }

    internal List<Restaurant> GetAll()
    {
      return _repo.GetAll();
    }

    internal Restaurant GetById(int id)
    {
      Restaurant found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("no restaurant by that id");
      }
      return found;
    }

    internal Restaurant Create(Restaurant data)
    {
      return _repo.Create(data);
    }

    internal Restaurant Update(Restaurant updateData)
    {
      Restaurant original = GetById(updateData.Id);
      if (updateData.CreatorId != original.CreatorId)
      {
        throw new Exception("Cannot Edit, Not your restaurant");
      }
      //NOTE double check everything you set in your repo is checked here
      original.Name = updateData.Name ?? original.Name;
      original.Address = updateData.Address ?? original.Address;
      original.Category = updateData.Category ?? original.Category;
      original.Picture = updateData.Picture ?? original.Picture;
      //NOTE pass to repo what you set = to on the left here
      return _repo.Update(original);
    }

    internal string Remove(int id, string userId)
    {
      Restaurant original = GetById(id);
      if (userId != original.CreatorId)
      {
        throw new Exception("Cannot Remove, Not your restaurant");
      }
      return _repo.Remove(id);
    }
  }
}