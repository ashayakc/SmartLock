using SmartLock.Model;

namespace SmartLock.CQRS.QueryResult
{
    public class DoorsByUserIdQueryResult : IQueryResult
    {
        public DoorsByUserIdQueryResult(IEnumerable<Door> doors)
        {
            Doors = doors;
        }

        public IEnumerable<Door> Doors { get; set; }
    }
}
