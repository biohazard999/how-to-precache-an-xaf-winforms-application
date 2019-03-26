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

namespace how_to_precache_an_xaf_winforms_application.Module.BusinessObjects {
	public class how_to_precache_an_xaf_winforms_applicationContextInitializer : DbContextTypesInfoInitializerBase {
		protected override DbContext CreateDbContext() {
			return new how_to_precache_an_xaf_winforms_applicationDbContext("App=EntityFramework");
		}
	}
	[TypesInfoInitializer(typeof(how_to_precache_an_xaf_winforms_applicationContextInitializer))]
	public class how_to_precache_an_xaf_winforms_applicationDbContext : DbContext {
		public how_to_precache_an_xaf_winforms_applicationDbContext(String connectionString)
			: base(connectionString) {
		}
		public how_to_precache_an_xaf_winforms_applicationDbContext(DbConnection connection)
			: base(connection, false) {
		}
		public how_to_precache_an_xaf_winforms_applicationDbContext()
			: base("name=ConnectionString") {
		}
		public DbSet<ModuleInfo> ModulesInfo { get; set; }
	}
}