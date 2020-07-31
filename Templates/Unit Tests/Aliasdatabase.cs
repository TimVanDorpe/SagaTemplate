
public static IEnumerable<AliasPE> GetAliass()
        {
            return new AliasPE[]
            {
                new AliasPE()
                {
                    DateCreated = DateTime.Now,
                    DateLastUpdated = DateTime.Now,
                    HasbeenEnabled = true,
                    TenantUniqueId = Guid.Parse("17DBC831-4CF2-4BD1-B0B3-09E17D605291"),
                    State = PersistenceState.Added,
                    UniqueId= Guid.NewGuid(),
                    RowKey = BitConverter.GetBytes((long)1)
				,Name =
				,FrameworkUniqueId =
				,CategoryUniqueId =
                },
                new AliasPE()
                {
                    DateCreated = DateTime.Now,
                    DateLastUpdated = DateTime.Now,
                    HasbeenEnabled = true,
                    TenantUniqueId = Guid.NewGuid(),
                    State = PersistenceState.Added,
                    UniqueId= Guid.NewGuid(),
                    RowKey = BitConverter.GetBytes((long)1),
				   ,Name =
				   ,FrameworkUniqueId =
				   ,CategoryUniqueId =
                },
                new AliasPE()
                {
                    DateCreated = DateTime.Now,
                    DateLastUpdated = DateTime.Now,
                    HasbeenEnabled = true,
                    TenantUniqueId = Guid.Parse("17DBC831-4CF2-4BD1-B0B3-09E17D605291"),
                    State = PersistenceState.Added,
                    UniqueId= Guid.NewGuid(),
                    RowKey = BitConverter.GetBytes((long)1)
				    ,Name = 
				    ,FrameworkUniqueId = 
				    ,CategoryUniqueId = 
                }
            };
        }

  public static async Task<AliasRepository> GetAliasRepository()
        {
            var factory = new AsyncInMemoryContextFactory<AliasDbContext>(new DbContextOptionsBuilder<AliasDbContext>());
            var context = await factory.CreateContextAsync();
            var repository = new AliasRepository(factory);
            context.AddRange(Database.GetAliass());
            context.SaveChanges();
            return repository;
        }

		// ADAPT IOC INFRASTRUCTURE
		// ADAPT IOC TEST.SETUP
