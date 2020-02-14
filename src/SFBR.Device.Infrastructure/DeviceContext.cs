using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SFBR.Device.Domain.SeedWork;
using SFBR.Device.Infrastructure.EntityConfigurations;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SFBR.Device.Infrastructure
{
    public class DeviceContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "SFBR";
        #region 设备描述
        public DbSet<Domain.AggregatesModel.DeviceTypeAggregate.DeviceType> DeviceTypes { get; set; }
        public DbSet<Domain.AggregatesModel.DeviceTypeAggregate.DeviceTypeChannel> DeviceTypeChannels { get; set; }
        public DbSet<Domain.AggregatesModel.DeviceTypeAggregate.DeviceTypeController> DeviceTypeControllers { get; set; }
        public DbSet<Domain.AggregatesModel.DeviceTypeAggregate.DeviceTypeSensor> DeviceTypeSensors { get; set; }
        public DbSet<Domain.AggregatesModel.DeviceTypeAggregate.DeviceTypePart> DeviceTypeParts { get; set; }
        public DbSet<Domain.AggregatesModel.DeviceTypeAggregate.DeviceTypeAlarm> DeviceTypeAlarms { get; set; }
        public DbSet<Domain.AggregatesModel.DeviceTypeAggregate.DeviceTypeFunction> DeviceTypeFunctions { get; set; }
        //public DbSet<Domain.AggregatesModel.DeviceTypeAggregate.AlarmCode> AlarmCodes { get; set; }
        #endregion

        #region 设备实例
        public DbSet<Domain.AggregatesModel.DeviceAggregate.Device> Devices { get; set; }
        public DbSet<Domain.AggregatesModel.DeviceAggregate.Channel> Channels { get; set; }
        public DbSet<Domain.AggregatesModel.DeviceAggregate.Controller> Controllers { get; set; }
        public DbSet<Domain.AggregatesModel.DeviceAggregate.Sensor> Sensors { get; set; }
        public DbSet<Domain.AggregatesModel.DeviceAggregate.Part> Parts { get; set; }
        public DbSet<Domain.AggregatesModel.DeviceAggregate.DeviceAlarm> DeviceAlarms { get; set; }
        public DbSet<Domain.AggregatesModel.DeviceAggregate.DeviceFunction> DeviceFunctions { get; set; }
        public DbSet<Domain.AggregatesModel.DeviceAggregate.Location> Locations { get; set; }
        public DbSet<Domain.AggregatesModel.DeviceAggregate.DevicePower> DevicePowers { get; set; }
        public DbSet<Domain.AggregatesModel.DeviceAggregate.TimedTask> TimedTasks { get; set; }
        #endregion

        #region 设备负载
        //public DbSet<Domain.AggregatesModel.LoadAggregate.LoadCategory> LoadCategories { get; set; }
        //public DbSet<Domain.AggregatesModel.LoadAggregate.DeviceProp> LoadProps { get; set; }
        //public DbSet<Domain.AggregatesModel.LoadAggregate.Load> Loads { get; set; }
        //public DbSet<Domain.AggregatesModel.LoadAggregate.DeviceProp> LoadExtends { get; set; }
        #endregion

        #region 区域
        public DbSet<Domain.AggregatesModel.RegionAggregate.Region> Regions { get; set; }
        #endregion

        #region 品牌
        public DbSet<Domain.AggregatesModel.BrandAggregate.Brand> Brands { get; set; }
        #endregion

        #region 维保信息
        //public DbSet<Domain.AggregatesModel.OprationAggregate.Company> Companies { get; set; }
        //public DbSet<Domain.AggregatesModel.OprationAggregate.Operation> Operations { get; set; }
        #endregion

        #region 用户信息
        public DbSet<Domain.AggregatesModel.UserAggregate.User> Users { get; set; } 
        #endregion


        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

      
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        private DeviceContext(DbContextOptions<DeviceContext> options) : base(options)
        {

        }

        public DeviceContext(DbContextOptions<DeviceContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


            System.Diagnostics.Debug.WriteLine("DeviceContext::ctor ->" + this.GetHashCode());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.InitTable();
            base.OnModelCreating(modelBuilder);
            modelBuilder.InitBaseData();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies();
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);

            var result = await base.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }

    public class DeviceContextDesignFactory : Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory<DeviceContext>
    {
        public DeviceContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DeviceContext>()
                .UseSqlServer("Server=.;Initial Catalog=SFBR.Device.Api;Integrated Security=true",o=>o.MigrationsAssembly("SFBR.Device.Infrastructure"));

            return new DeviceContext(optionsBuilder.Options, new NoMediator());
        }

        class NoMediator : IMediator
        {
            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task Publish(object notification, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.FromResult(default(TResponse));
            }
        }
    }
}
