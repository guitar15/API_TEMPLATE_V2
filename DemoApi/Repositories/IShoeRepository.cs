using DemoApi.Models;

namespace DemoApi.Repositories;

public interface IShoeRepository
{
    List<Shoe> GetShoes(string Ip, string queue);

    bool DeleteShoe(int id);
}
