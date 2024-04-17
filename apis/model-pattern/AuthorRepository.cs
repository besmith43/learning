namespace model_pattern.Models;

public class AuthorRepository : IAuthorRepository
{
    void IAuthorRepository.Create(Author entity)
    {
        throw new NotImplementedException();
    }
    void IAuthorRepository.Delete(Author entity)
    {
        throw new NotImplementedException();
    }
    Author IAuthorRepository.GetById(int id)
    {
        throw new NotImplementedException();
    }
    void IAuthorRepository.Update(Author entity)
    {
        throw new NotImplementedException();
    }
}