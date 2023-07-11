      CREATE TABLE [TaxPayers] (
          [Id] int NOT NULL IDENTITY,
          [Name] nvarchar(64) NULL,
          [Nip] nvarchar(10) NULL,
          [SatusVat] nvarchar(16) NULL,
          [Regon] nvarchar(9) NULL,
          [Pesel] nvarchar(11) NULL,
          [Krs] nvarchar(10) NULL,
          [ResidenceAddress] nvarchar(64) NULL,
          [WorkingAddress] nvarchar(64) NULL,
          [RegistrationLegalDate] datetime2 NULL,
          [RegistrationDenialBasis] nvarchar(64) NULL,
          [RegistrationDenialDate] datetime2 NULL,
          [RestorationBasis] nvarchar(64) NULL,
          [RestorationDate] datetime2 NULL,
          [RemovalBasis] nvarchar(64) NULL,
          [RemovalDate] datetime2 NULL,
          [HasVirtualAccounts] bit NOT NULL,
          [RequestId] nvarchar(16) NULL,
          [RequesteDateTime] datetime2 NULL,
          [QueryResult] nvarchar(64) NULL,
          [Created] datetime2 NULL,
          CONSTRAINT [PK_TaxPayers] PRIMARY KEY ([Id])
      );
      CREATE TABLE [AccountNumbers] (
          [Id] int NOT NULL IDENTITY,
          [Number] nvarchar(26) NULL,
          [TaxPayerId] int NOT NULL,
          CONSTRAINT [PK_AccountNumbers] PRIMARY KEY ([Id]),
          CONSTRAINT [FK_AccountNumbers_TaxPayers_TaxPayerId] FOREIGN KEY ([TaxPayerId]) REFERENCES [TaxPayers] ([Id]) ON DELETE CASCADE
      );
      CREATE TABLE [AuthorizedClerks] (
          [Id] int NOT NULL IDENTITY,
          [TaxPayerId] int NOT NULL,
          [FirstName] nvarchar(64) NULL,
          [LastName] nvarchar(64) NULL,
          [Nip] nvarchar(10) NULL,
          [Pesel] nvarchar(11) NULL,
          [CompanyName] nvarchar(64) NULL,
          CONSTRAINT [PK_AuthorizedClerks] PRIMARY KEY ([Id]),
          CONSTRAINT [FK_AuthorizedClerk_TaxPayerId] FOREIGN KEY ([TaxPayerId]) REFERENCES [TaxPayers] ([Id])
      );
      CREATE TABLE [Partners] (
          [Id] int NOT NULL IDENTITY,
          [TaxPayerId] int NOT NULL,
          [FirstName] nvarchar(64) NULL,
          [LastName] nvarchar(64) NULL,
          [Nip] nvarchar(10) NULL,
          [Pesel] nvarchar(11) NULL,
          [CompanyName] nvarchar(64) NULL,
          CONSTRAINT [PK_Partners] PRIMARY KEY ([Id]),
          CONSTRAINT [FK_Partner_TaxPayerId] FOREIGN KEY ([TaxPayerId]) REFERENCES [TaxPayers] ([Id])
      );
      CREATE TABLE [Representatives] (
          [Id] int NOT NULL IDENTITY,
          [TaxPayerId] int NOT NULL,
          [FirstName] nvarchar(64) NULL,
          [LastName] nvarchar(64) NULL,
          [Nip] nvarchar(10) NULL,
          [Pesel] nvarchar(11) NULL,
          [CompanyName] nvarchar(64) NULL,
          CONSTRAINT [PK_Representatives] PRIMARY KEY ([Id]),
          CONSTRAINT [FK_Representative_TaxPayerId] FOREIGN KEY ([TaxPayerId]) REFERENCES [TaxPayers] ([Id])
      );
      CREATE INDEX [IX_AccountNumbers_TaxPayerId] ON [AccountNumbers] ([TaxPayerId]);
      CREATE INDEX [IX_AuthorizedClerks_TaxPayerId] ON [AuthorizedClerks] ([TaxPayerId]);
      CREATE INDEX [IX_Partners_TaxPayerId] ON [Partners] ([TaxPayerId]);
      CREATE INDEX [IX_Representatives_TaxPayerId] ON [Representatives] ([TaxPayerId]);
      CREATE INDEX [IX_TaxPayers_Name] ON [TaxPayers] ([Name]);
      CREATE INDEX [IX_TaxPayers_Nip] ON [TaxPayers] ([Nip]);
      CREATE INDEX [IX_TaxPayers_Regon] ON [TaxPayers] ([Regon]);
