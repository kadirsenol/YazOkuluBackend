using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using YazOkulu.Data.Base;
using YazOkulu.Data.Context;
using YazOkulu.Data.Models;
using Microsoft.AspNetCore.Http;
using YazOkulu.Data.Interfaces;

namespace YazOkulu.Data.Halpers
{
    public class UnitOfWork(YazOkuluDbContext db, IHttpContextAccessor contextAccessor) : UnitOfWorkBase(db, contextAccessor), IUnitOfWork
    {
        #region Private Repos
        private IRepository<Course> _CourseRepository;
        private IRepository<User> _UserRepository;
        private IRepository<GsmOtp> _GsmOtpRepository;
        private IRepository<Parameter> _ParameterRepository;
        private IRepository<Application> _ApplicationRepository;


        #endregion Private Repos

        #region Public Repos
        public IRepository<Course> CourseRepository => _CourseRepository ??= new Repository<Course>(db, contextAccessor, IsAudit);
        public IRepository<User> UserRepository => _UserRepository ??= new Repository<User>(db, contextAccessor, IsAudit);
        public IRepository<GsmOtp> GsmOtpRepository => _GsmOtpRepository ??= new Repository<GsmOtp>(db, contextAccessor, IsAudit);
        public IRepository<Parameter> ParameterRepository => _ParameterRepository ??= new Repository<Parameter>(db, contextAccessor, IsAudit);
        public IRepository<Application> ApplicationRepository => _ApplicationRepository ??= new Repository<Application>(db, contextAccessor, IsAudit);
        #endregion Public Repos
    }
}
