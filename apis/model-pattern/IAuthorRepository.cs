namespace model_pattern.Models;

public interface IAuthorRepository
{
    Author GetById(int id);
    void Create(Author entity);
    void Delete(Author entity);
    void Update(Author entity);
}