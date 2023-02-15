IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127012254_m1')
BEGIN
    CREATE TABLE [accountstatuses] (
        [Id] varchar(36) NOT NULL,
        [Code] varchar(10) NOT NULL,
        [Text] nvarchar(50) NULL,
        CONSTRAINT [PK_accountstatuses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127012254_m1')
BEGIN
    CREATE TABLE [transactiontypes] (
        [Id] varchar(36) NOT NULL,
        [Code] varchar(10) NOT NULL,
        [Text] nvarchar(50) NULL,
        CONSTRAINT [PK_transactiontypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127012254_m1')
BEGIN
    CREATE TABLE [accounts] (
        [Id] varchar(36) NOT NULL,
        [Number] varchar(10) NOT NULL,
        [Holder] nvarchar(50) NOT NULL,
        [Balance] decimal(18,0) NOT NULL,
        [AccountStatusId] varchar(36) NOT NULL,
        CONSTRAINT [PK_accounts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_accounts_accountstatuses_AccountStatusId] FOREIGN KEY ([AccountStatusId]) REFERENCES [accountstatuses] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127012254_m1')
BEGIN
    CREATE TABLE [transactions] (
        [Id] varchar(36) NOT NULL,
        [Amount] decimal(18,0) NOT NULL,
        [Note] nvarchar(100) NULL,
        [Date] datetime2 NULL,
        [AccountId] varchar(36) NOT NULL,
        [TransactionTypeId] varchar(36) NOT NULL,
        CONSTRAINT [PK_transactions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_transactions_accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [accounts] ([Id]),
        CONSTRAINT [FK_transactions_transactiontypes_TransactionTypeId] FOREIGN KEY ([TransactionTypeId]) REFERENCES [transactiontypes] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127012254_m1')
BEGIN
    CREATE INDEX [IX_accounts_AccountStatusId] ON [accounts] ([AccountStatusId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127012254_m1')
BEGIN
    CREATE UNIQUE INDEX [IX_accounts_Number] ON [accounts] ([Number]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127012254_m1')
BEGIN
    CREATE UNIQUE INDEX [IX_accountstatuses_Code] ON [accountstatuses] ([Code]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127012254_m1')
BEGIN
    CREATE INDEX [IX_transactions_AccountId] ON [transactions] ([AccountId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127012254_m1')
BEGIN
    CREATE INDEX [IX_transactions_TransactionTypeId] ON [transactions] ([TransactionTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127012254_m1')
BEGIN
    CREATE UNIQUE INDEX [IX_transactiontypes_Code] ON [transactiontypes] ([Code]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127012254_m1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230127012254_m1', N'7.0.2');
END;
GO

COMMIT;
GO

