using System;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.ComponentModel;
using DevExpress.ExpressApp.EF.Updating;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EF.DesignTime;

namespace PreCacheDemo.Module.BusinessObjects {
	public class PreCacheDemoContextInitializer : DbContextTypesInfoInitializerBase {
		protected override DbContext CreateDbContext() {
			return new PreCacheDemoDbContext("App=EntityFramework");
		}
	}
	[TypesInfoInitializer(typeof(PreCacheDemoContextInitializer))]
	public class PreCacheDemoDbContext : DbContext {
		public PreCacheDemoDbContext(String connectionString)
			: base(connectionString) {
		}
		public PreCacheDemoDbContext(DbConnection connection)
			: base(connection, false) {
		}
		public PreCacheDemoDbContext()
			: base("name=ConnectionString") {
		}
		public DbSet<ModuleInfo> ModulesInfo { get; set; }
	}
}