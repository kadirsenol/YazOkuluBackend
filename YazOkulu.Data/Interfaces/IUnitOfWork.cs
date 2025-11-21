using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using YazOkulu.Data.Models;

namespace YazOkulu.Data.Interfaces
{
    public interface IUnitOfWork : IUnitOfWorkBase
    {
        IRepository<Course> CourseRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<GsmOtp> GsmOtpRepository { get; }
        IRepository<Parameter> ParameterRepository { get; }
        IRepository<Application> ApplicationRepository { get; }
    }
}
