CREATE TABLE [dbo].[users] (
    [Id]       INT        IDENTITY (1, 1) NOT NULL,
    [username] NCHAR (10) NULL,
    [password] NCHAR (10) NULL,
    [email]    NCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[bizCards] (
    [Id]        INT        IDENTITY (1, 1) NOT NULL,
    [id_owner]  INT        NOT NULL,
    [Nume]      NCHAR (10) NULL,
    [Prenume]   NCHAR (10) NULL,
    [website]   NCHAR (10) NULL,
    [phone1]    NCHAR (10) NULL,
    [phone2]    NCHAR (10) NULL,
    [fax]       NCHAR (10) NULL,
    [bizName]   NCHAR (10) NULL,
    [adresa]    NCHAR (10) NULL,
    [regCom]    NCHAR (10) NULL,
    [codCIF]    NCHAR (10) NULL,
    [Functie]   NCHAR (10) NULL,
    [email]     NCHAR (10) NULL,
    [imagePath] NCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([id_owner]) REFERENCES [dbo].[users] ([Id])
);




CREATE TABLE [dbo].[AuthKey] (
    [Id]         INT        IDENTITY (1, 1) NOT NULL,
    [authstring] NCHAR (10) NULL,
    [expiration] DATETIME   NULL,
    [id_owner]   INT        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([id_owner]) REFERENCES [dbo].[users] ([Id])
);

