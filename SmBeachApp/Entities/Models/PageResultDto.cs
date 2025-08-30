namespace SmBeachApp.Entities.Models;

public class PageResultDto<T>
{
    public int CollectionSize { get; set; }
    public T Result { get; set; }
}