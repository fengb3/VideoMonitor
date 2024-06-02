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

CREATE TABLE [UserRecords] (
    [UrId] bigint NOT NULL IDENTITY,
    [Uid] bigint NOT NULL,
    [TimeStamp] bigint NOT NULL,
    [FollowerNum] int NOT NULL,
    [FollowingNum] int NOT NULL,
    [ArchiveNum] int NOT NULL,
    [LikeNum] int NOT NULL,
    CONSTRAINT [PK_UserRecords] PRIMARY KEY ([UrId])
);
GO

CREATE TABLE [Users] (
    [Uid] bigint NOT NULL,
    [MostRecentUserRecordId] bigint NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [FaceUrl] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Uid])
);
GO

CREATE TABLE [VideoRecords] (
    [VrId] bigint NOT NULL IDENTITY,
    [BvId] nvarchar(max) NOT NULL,
    [TimeStamp] bigint NOT NULL,
    [Likes] int NOT NULL,
    [Dislikes] int NOT NULL,
    [Coins] int NOT NULL,
    [Favorites] int NOT NULL,
    [Shares] int NOT NULL,
    [Danmaku] int NOT NULL,
    [Comments] int NOT NULL,
    [Views] int NOT NULL,
    CONSTRAINT [PK_VideoRecords] PRIMARY KEY ([VrId])
);
GO

CREATE TABLE [Videos] (
    [BvId] nvarchar(450) NOT NULL,
    [TypeId] int NOT NULL,
    [AuthorUid] bigint NOT NULL,
    [MostRecentVideoRecordId] bigint NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [UploadTimeStamp] bigint NOT NULL,
    [CoverUrl] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Videos] PRIMARY KEY ([BvId])
);
GO

CREATE TABLE [VideoTypes] (
    [TypeId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_VideoTypes] PRIMARY KEY ([TypeId])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240528125855_Initial', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'MostRecentUserRecordId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Users] ALTER COLUMN [MostRecentUserRecordId] bigint NULL;
GO

CREATE UNIQUE INDEX [IX_Users_MostRecentUserRecordId] ON [Users] ([MostRecentUserRecordId]) WHERE [MostRecentUserRecordId] IS NOT NULL;
GO

CREATE INDEX [IX_UserRecords_Uid] ON [UserRecords] ([Uid]);
GO

ALTER TABLE [UserRecords] ADD CONSTRAINT [FK_UserRecords_Users_Uid] FOREIGN KEY ([Uid]) REFERENCES [Users] ([Uid]) ON DELETE CASCADE;
GO

ALTER TABLE [Users] ADD CONSTRAINT [FK_Users_UserRecords_MostRecentUserRecordId] FOREIGN KEY ([MostRecentUserRecordId]) REFERENCES [UserRecords] ([UrId]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240529135234_add-fk-User-Userrecord', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Videos]') AND [c].[name] = N'TypeId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Videos] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Videos] ALTER COLUMN [TypeId] int NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Videos]') AND [c].[name] = N'MostRecentVideoRecordId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Videos] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Videos] ALTER COLUMN [MostRecentVideoRecordId] bigint NULL;
GO

ALTER TABLE [Videos] ADD [UserUid] bigint NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[VideoRecords]') AND [c].[name] = N'BvId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [VideoRecords] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [VideoRecords] ALTER COLUMN [BvId] nvarchar(450) NOT NULL;
GO

ALTER TABLE [VideoRecords] ADD [ViewingNum] int NOT NULL DEFAULT 0;
GO

CREATE INDEX [IX_Videos_AuthorUid] ON [Videos] ([AuthorUid]);
GO

CREATE UNIQUE INDEX [IX_Videos_MostRecentVideoRecordId] ON [Videos] ([MostRecentVideoRecordId]) WHERE [MostRecentVideoRecordId] IS NOT NULL;
GO

CREATE INDEX [IX_Videos_TypeId] ON [Videos] ([TypeId]);
GO

CREATE INDEX [IX_Videos_UserUid] ON [Videos] ([UserUid]);
GO

CREATE INDEX [IX_VideoRecords_BvId] ON [VideoRecords] ([BvId]);
GO

ALTER TABLE [VideoRecords] ADD CONSTRAINT [FK_VideoRecords_Videos_BvId] FOREIGN KEY ([BvId]) REFERENCES [Videos] ([BvId]) ON DELETE CASCADE;
GO

ALTER TABLE [Videos] ADD CONSTRAINT [FK_Videos_Users_AuthorUid] FOREIGN KEY ([AuthorUid]) REFERENCES [Users] ([Uid]) ON DELETE NO ACTION;
GO

ALTER TABLE [Videos] ADD CONSTRAINT [FK_Videos_Users_UserUid] FOREIGN KEY ([UserUid]) REFERENCES [Users] ([Uid]);
GO

ALTER TABLE [Videos] ADD CONSTRAINT [FK_Videos_VideoRecords_MostRecentVideoRecordId] FOREIGN KEY ([MostRecentVideoRecordId]) REFERENCES [VideoRecords] ([VrId]) ON DELETE NO ACTION;
GO

ALTER TABLE [Videos] ADD CONSTRAINT [FK_Videos_VideoTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [VideoTypes] ([TypeId]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240530154331_add-releationship', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Configs] (
    [Key] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NOT NULL,
    [LastTimeUpdate] bigint NOT NULL,
    CONSTRAINT [PK_Configs] PRIMARY KEY ([Key])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Key', N'LastTimeUpdate', N'Value') AND [object_id] = OBJECT_ID(N'[Configs]'))
    SET IDENTITY_INSERT [Configs] ON;
INSERT INTO [Configs] ([Key], [LastTimeUpdate], [Value])
VALUES (N'buvid3', CAST(0 AS bigint), N'AFE917A3-F157-AB06-AD81-F5E97E8B5D9A60062infoc'),
(N'buvid4', CAST(0 AS bigint), N'DAC8A899-9006-0900-5D29-5D64C5BB0F8824741-023013104-NLgo%2F2RgzmTINRwjdsii8w%3D%3D'),
(N'SESSDATA', CAST(0 AS bigint), N'');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Key', N'LastTimeUpdate', N'Value') AND [object_id] = OBJECT_ID(N'[Configs]'))
    SET IDENTITY_INSERT [Configs] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240601075944_add-configs', N'8.0.6');
GO

COMMIT;
GO

