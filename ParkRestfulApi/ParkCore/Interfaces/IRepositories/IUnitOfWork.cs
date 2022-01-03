using System;

namespace ParkCore.Interfaces.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        INationalParkRepository NationalParkRepository { get; }
        ITrailRepository TrailRepository { get; }
    }
}
