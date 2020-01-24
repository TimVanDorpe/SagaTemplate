
public static IEnumerable<PosWordStringPE> GetPosWordStrings()
        {
            return new PosWordStringPE[]
            {
                new PosWordStringPE()
                {
                    DateCreated = DateTime.Now,
                    DateLastUpdated = DateTime.Now,
                    HasbeenEnabled = true,
                    TenantUniqueId = Guid.Parse("17DBC831-4CF2-4BD1-B0B3-09E17D605291"),
                    State = PersistenceState.Added,
                    UniqueId= Guid.NewGuid(),
                    RowKey = BitConverter.GetBytes((long)1)
				,Text =
				,Language =
				,DefaultPosPolarity =
                },
                new PosWordStringPE()
                {
                    DateCreated = DateTime.Now,
                    DateLastUpdated = DateTime.Now,
                    HasbeenEnabled = true,
                    TenantUniqueId = Guid.NewGuid(),
                    State = PersistenceState.Added,
                    UniqueId= Guid.NewGuid(),
                    RowKey = BitConverter.GetBytes((long)1),
				   ,Text =
				   ,Language =
				   ,DefaultPosPolarity =
                },
                new PosWordStringPE()
                {
                    DateCreated = DateTime.Now,
                    DateLastUpdated = DateTime.Now,
                    HasbeenEnabled = true,
                    TenantUniqueId = Guid.Parse("17DBC831-4CF2-4BD1-B0B3-09E17D605291"),
                    State = PersistenceState.Added,
                    UniqueId= Guid.NewGuid(),
                    RowKey = BitConverter.GetBytes((long)1)
				    ,Text = 
				    ,Language = 
				    ,DefaultPosPolarity = 
                }
            };
        }

  public static async Task<PosWordStringRepository> GetPosWordStringRepository()
        {
            var factory = new AsyncInMemoryContextFactory<PosWordStringDbContext>(new DbContextOptionsBuilder<PosWordStringDbContext>());
            var context = await factory.CreateContextAsync();
            var repository = new PosWordStringRepository(factory);
            context.AddRange(Database.GetPosWordStrings());
            context.SaveChanges();
            return repository;
        }

		// ADAPT IOC INFRASTRUCTURE
		// ADAPT IOC TEST.SETUP
