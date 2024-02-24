CREATE TABLE [dbo].[Park] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [NumberFloors] INT            NOT NULL,
    [ParkName]     NVARCHAR (255) NOT NULL,
    [Latitude]     FLOAT (53)     NOT NULL,
    [Longitude]    FLOAT (53)     NOT NULL,
    [OpeningTime]  DATETIME       NOT NULL,
    [ClosingTime]  DATETIME       NOT NULL,
    [IsActive]     BIT            NOT NULL,
    [NightFee]     FLOAT (53)     NOT NULL,
    [PriceTableId] INT            NOT NULL,
    [Location]     VARCHAR (250)  NULL,
    CONSTRAINT [PK_Park] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[ParkingSpot] (
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [Status] BIT NOT NULL,
    [FloorNumber] INT NOT NULL,
    [VehicleType] NVARCHAR (255) NOT NULL,
    [ParkId] INT NOT NULL,
    CONSTRAINT [PK_ParkingSpot] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParkId] FOREIGN KEY ([ParkId]) REFERENCES [dbo].[Park] ([Id])
);

CREATE TABLE [dbo].[PriceTable] (
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [InitialDate] DATETIME NOT NULL,
    CONSTRAINT [PK_PriceTable] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[LinePriceTable] (
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [PeriodId] INT NOT NULL,
	[PriceTableId] INT NOT NULL,
    CONSTRAINT [PK_LinePriceTable] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_PriceTableId] FOREIGN KEY ([PriceTableId]) REFERENCES [dbo].[PriceTable] ([Id])
);

CREATE TABLE [dbo].[Period] (
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [InitialTime] TIME NOT NULL,
    [FinalTime] TIME NOT NULL,
    CONSTRAINT [PK_Period] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Fraction] (
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [Order] INT NOT NULL,
    [Minutes] TIME NOT NULL,
    [VehicleType] NVARCHAR (255) NOT NULL,
	[PeriodId] INT NOT NULL,
    [Price]       FLOAT (53)     NULL,
    CONSTRAINT [PK_Fraction] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_PeriodId] FOREIGN KEY ([PeriodId]) REFERENCES [dbo].[Period]([Id])
);


-- Mock data for Park table
INSERT INTO [dbo].[Park] ([NumberFloors], [ParkName], [Latitude], [Longitude], [OpeningTime], [ClosingTime], [IsActive], [NightFee], [PriceTableId], [Location])
VALUES (5, 'City Park', 41.1496, -8.6109, '2023-11-14T08:00:00', '2023-11-14T18:00:00', 1, 5.0, 1, 'location'),
       (3, 'Central Park', 41.1492, -8.6115, '2023-11-14T09:00:00', '2023-11-14T17:00:00', 1, 3.5, 2, 'location');

-- Mock data for ParkingSpot table
INSERT INTO [dbo].[ParkingSpot] ([Status], [FloorNumber], [VehicleType], [ParkId])
VALUES 
    (1, 1, '2', 1),
    (0, 2, '0', 1),
    (1, 1, '1', 2),
    (0, 2, '0', 2),
    (1, 3, '1', 2);

-- Mock data for PriceTable table
INSERT INTO [dbo].[PriceTable] ([InitialDate])
VALUES ('2023-11-14T00:00:00'),
       ('2023-11-14T00:00:00');

-- Mock data for LinePriceTable table
INSERT INTO [dbo].[LinePriceTable] ([PeriodId], [PriceTableId])
VALUES (1, 1),
       (2, 1),
       (3, 1),
       (1, 2),
       (2, 2),
       (3, 2);

-- Mock data for Period table
INSERT INTO [dbo].[Period] ([InitialTime], [FinalTime])
VALUES ('08:00:00', '12:00:00'),
       ('12:00:00', '16:00:00'),
       ('16:00:00', '20:00:00'),
       ('09:00:00', '12:00:00'),
       ('12:00:00', '15:00:00'),
       ('15:00:00', '18:00:00');

-- Mock data for Fraction table
INSERT INTO [dbo].[Fraction] ([Order], [Minutes], [VehicleType], [PeriodId])
VALUES (1, '00:15:00', '0', 1),
       (2, '00:30:00', '1', 1),
       (3, '00:45:00', '2', 1),
       (4, '01:00:00', '3', 1),
       (1, '00:20:00', '0', 2),
       (2, '00:40:00', '1', 2),
       (3, '01:00:00', '2', 2),
       (4, '01:20:00', '3', 2),
       (1, '00:10:00', '0', 3),
       (2, '00:20:00', '1', 3),
       (3, '00:30:00', '2', 3),
       (4, '00:40:00', '3', 3),
       (1, '00:15:00', '0', 4),
       (2, '00:30:00', '1', 4),
       (3, '00:45:00', '2', 4),
       (4, '01:00:00', '3', 4),
       (1, '00:10:00', '0', 5),
       (2, '00:20:00', '1', 5),
       (3, '00:30:00', '2', 5),
       (4, '00:40:00', '3', 5),
       (1, '00:15:00', '0', 6),
       (2, '00:30:00', '1', 6),
       (3, '00:45:00', '2', 6);

-- More Table Creations

CREATE TABLE [dbo].[Customer] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Username]         NVARCHAR (100) NOT NULL,
    [Email]            NVARCHAR (100) NOT NULL,
    [Password]         NVARCHAR (100) NOT NULL,
    [Name]             NVARCHAR (100) NOT NULL,
    [Blocked]          BIT            DEFAULT ((0)) NOT NULL,
    [Invitecode]       NVARCHAR (100) NULL,
    [RegistrationDate] DATETIME       NULL,
    [ParkyWalletId]    INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Vehicle] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [LicensePlate] NVARCHAR (20)  NOT NULL,
    [Model]        NVARCHAR (255) NOT NULL,
    [Brand]        NVARCHAR (255) NOT NULL,
    [Type]         NVARCHAR (255) NOT NULL,
    [CustomerId]   INT            NOT NULL,
    CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([LicensePlate] ASC),
    CONSTRAINT [FK_Vehicle_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id])
);

CREATE TABLE [dbo].[ParkLog] (
    [Id]        INT      IDENTITY (1, 1) NOT NULL,
    [VehicleId] INT      NOT NULL,
    [ParkId]    INT      NOT NULL,
    [StartTime] DATETIME NULL,
    [EndTime]   DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParkLog_Vehicle] FOREIGN KEY ([VehicleId]) REFERENCES [dbo].[Vehicle] ([Id]),
    CONSTRAINT [FK_ParkLog_Park] FOREIGN KEY ([ParkId]) REFERENCES [dbo].[Park] ([Id])
);

CREATE TABLE [dbo].[Invoice] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [ParkLogId]   INT             NOT NULL,
    [TotalAmount] DECIMAL (10, 2) NOT NULL,
    [IsPayed]     BIT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Invoice_ParkLog] FOREIGN KEY ([ParkLogId]) REFERENCES [dbo].[ParkLog] ([Id])
);

CREATE TABLE [dbo].[ParkyCoinsConfiguration] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [ConfigName]  NVARCHAR (40)  NOT NULL,
    [Description] NVARCHAR (150) NOT NULL,
    [Amount]      NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_ParkyCoinsConfiguration] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[ParkyWallet] (
    [Id]     INT IDENTITY (1, 1) NOT NULL,
    [Amount] INT NOT NULL,
    CONSTRAINT [PK_ParkyWallet] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[ParkyWalletMovements] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Amount]        INT           NOT NULL,
    [Date]          DATETIME      NOT NULL,
    [MovementType]  VARCHAR (250) NOT NULL,
    [ParkyWalletId] INT           NOT NULL,
    CONSTRAINT [PK_ParkyWalletMovements] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParkyWalletId] FOREIGN KEY ([ParkyWalletId]) REFERENCES [dbo].[ParkyWallet] ([Id])
);

CREATE TABLE [dbo].[PaymentMethod] (
    [PaymentMethodId]    INT           IDENTITY (1, 1) NOT NULL,
    [CustomerId]         INT           NULL,
    [PaymentToken]       VARCHAR (255) NULL,
    [PaymentMethodType]  VARCHAR (50)  NULL,
    [CardLastFourDigits] INT           NULL,
    [FullName]           VARCHAR (255) NULL,
    [ExpirationDate]     DATE          NULL,
    PRIMARY KEY CLUSTERED ([PaymentMethodId] ASC),
    CONSTRAINT [FK_PaymentMethod_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id])
);

-- Mock Data

INSERT INTO [dbo].[ParkyWallet] ([Amount])
VALUES (1000),
       (500),
       (2000),
       (1500),
       (3000);

INSERT INTO [dbo].[Customer] ([Username], [Email], [Password], [Name], [Blocked], [Invitecode], [RegistrationDate], [ParkyWalletId])
VALUES ('User1', 'user1@example.com', 'password1', 'John Doe',  0, '', '2023-01-01T00:00:00',  1),
       ('User2', 'user2@example.com', 'password2', 'Jane Smith',  0, '', '2023-01-02T00:00:00',  2),
       ('User3', 'user3@example.com', 'password3', 'Alice Johnson',  0, '', '2023-01-03T00:00:00',  3),
       ('User4', 'user4@example.com', 'password4', 'Bob Brown',  0, '', '2023-01-04T00:00:00',  4),
       ('User5', 'user5@example.com', 'password5', 'Charlie Davis',  0, '', '2023-01-05T00:00:00',  5);


-- Mock data for Park table
INSERT INTO [dbo].[Park] ([NumberFloors], [ParkName], [Latitude], [Longitude], [OpeningTime], [ClosingTime], [IsActive], [NightFee], [PriceTableId], [Location])
VALUES 
    (4, 'Green Gardens Park', 41.1552, -8.6098, '2023-11-14T09:00:00', '2023-11-14T18:00:00', 1, 4.0, 1, 'location'),
    (6, 'Sunset Plaza Park', 41.1525, -8.6083, '2023-11-14T10:00:00', '2023-11-14T20:00:00', 1, 6.0, 2, 'location'),
    (3, 'Urban Oasis Park', 41.1487, -8.6135, '2023-11-14T08:30:00', '2023-11-14T17:30:00', 1, 3.0, 3, 'location'),
    (5, 'Lakeside Park', 41.1538, -8.6055, '2023-11-14T09:30:00', '2023-11-14T19:30:00', 1, 5.5, 4, 'location'),
    (7, 'Mountain View Park', 41.1510, -8.6110, '2023-11-14T10:30:00', '2023-11-14T21:00:00', 1, 7.0, 5, 'location'),
    (4, 'Cityscape Park', 41.1502, -8.6100, '2023-11-14T08:00:00', '2023-11-14T17:00:00', 1, 4.5, 6, 'location'),
    (6, 'Tranquil Gardens Park', 41.1490, -8.6122, '2023-11-14T09:00:00', '2023-11-14T19:00:00', 1, 6.5, 7, 'location'),
    (5, 'Meadowlands Park', 41.1545, -8.6075, '2023-11-14T10:00:00', '2023-11-14T18:30:00', 1, 5.0, 8, 'location');

-- Mock data for ParkingSpot table
INSERT INTO [dbo].[ParkingSpot] ([Status], [FloorNumber], [VehicleType], [ParkId])
VALUES 
    (1, 1, '2', 3),
    (0, 2, '0', 5),
    (1, 1, '1', 8),
    (0, 2, '0', 2),
    (1, 3, '1', 4),
    (0, 2, '0', 7),
    (1, 1, '2', 1),
    (1, 3, '1', 6),
    (0, 2, '0', 3),
    (1, 3, '1', 5),
    (0, 2, '0', 4),
    (1, 3, '1', 2),
    (0, 2, '0', 1),
    (1, 1, '2', 7),
    (0, 2, '0', 6),
    (1, 3, '1', 3),
    (0, 2, '0', 8);

-- Mock data for PriceTable table
INSERT INTO [dbo].[PriceTable] ([InitialDate])
VALUES ('2023-11-14T00:00:00'),
       ('2023-11-14T00:00:00'),
       ('2023-11-14T00:00:00'),
       ('2023-11-14T00:00:00'),
       ('2023-11-14T00:00:00'),
       ('2023-11-14T00:00:00'),
       ('2023-11-14T00:00:00'),
       ('2023-11-14T00:00:00');

-- Mock data for LinePriceTable table
INSERT INTO [dbo].[LinePriceTable] ([PeriodId], [PriceTableId])
VALUES 
    (1, 1),
    (2, 1),
    (3, 1),
    (1, 2),
    (2, 2),
    (3, 2),
    (1, 3),
    (2, 3),
    (3, 3),
    (1, 4),
    (2, 4),
    (3, 4),
    (1, 5),
    (2, 5),
    (3, 5),
    (1, 6),
    (2, 6),
    (3, 6),
    (1, 7),
    (2, 7),
    (3, 7),
    (1, 8),
    (2, 8),
    (3, 8);

-- Mock data for Period table
INSERT INTO [dbo].[Period] ([InitialTime], [FinalTime])
VALUES 
    ('08:00:00', '12:00:00'),
    ('12:00:00', '16:00:00'),
    ('16:00:00', '20:00:00'),
    ('09:00:00', '12:00:00'),
    ('12:00:00', '15:00:00'),
    ('15:00:00', '18:00:00'),
    ('10:00:00', '14:00:00'),
    ('14:00:00', '18:00:00');

-- Mock data for Fraction table
INSERT INTO [dbo].[Fraction] ([Order], [Minutes], [VehicleType], [PeriodId])
VALUES 
    (1, '00:15:00', '0', 1),
    (2, '00:30:00', '1', 1),
    (3, '00:45:00', '2', 1),
    (4, '01:00:00', '3', 1),
    (1, '00:20:00', '0', 2),
    (2, '00:40:00', '1', 2),
    (3, '01:00:00', '2', 2),
    (4, '01:20:00', '3', 2),
    (1, '00:10:00', '0', 3),
    (2, '00:20:00', '1', 3),
    (3, '00:30:00', '2', 3),
    (4, '00:40:00', '3', 3),
    (1, '00:15:00', '0', 4),
    (2, '00:30:00', '1', 4),
    (3, '00:45:00', '2', 4),
    (4, '01:00:00', '3', 4),
    (1, '00:10:00', '0', 5),
    (2, '00:20:00', '1', 5),
    (3, '00:30:00', '2', 5),
    (4, '00:40:00', '3', 5),
    (1, '00:15:00', '0', 6),
    (2, '00:30:00', '1', 6),
    (3, '00:45:00', '2', 6),
    (1, '00:20:00', '0', 7),
    (2, '00:40:00', '1', 7),
    (3, '01:00:00', '2', 7),
    (4, '01:20:00', '3', 7),
    (1, '00:10:00', '0', 8),
    (2, '00:20:00', '1', 8),
    (3, '00:30:00', '2', 8),
    (4, '00:40:00', '3', 8);

-- Mock data for Customer table
INSERT INTO [dbo].[Customer] ([Username], [Email], [Password], [Name], [Blocked], [Invitecode], [RegistrationDate], [ParkyWalletId])
VALUES 
    ('user1', 'user1@example.com', 'password1', 'John Doe', 0, NULL, '2023-11-14T08:00:00', 1),
    ('user2', 'user2@example.com', 'password2', 'Jane Smith', 0, NULL, '2023-11-14T09:00:00', 2),
    ('user3', 'user3@example.com', 'password3', 'Alice Johnson', 0, NULL, '2023-11-14T10:00:00', 3),
    ('user4', 'user4@example.com', 'password4', 'Bob Miller', 0, NULL, '2023-11-14T11:00:00', 4),
    ('user5', 'user5@example.com', 'password5', 'Chris Wilson', 0, NULL, '2023-11-14T12:00:00', 5),
    ('user6', 'user6@example.com', 'password6', 'Eva Brown', 0, NULL, '2023-11-14T13:00:00', 6),
    ('user7', 'user7@example.com', 'password7', 'David Taylor', 0, NULL, '2023-11-14T14:00:00', 7),
    ('user8', 'user8@example.com', 'password8', 'Sophie Lee', 0, NULL, '2023-11-14T15:00:00', 8);

-- Mock data for Vehicle table
INSERT INTO [dbo].[Vehicle] ([LicensePlate], [Model], [Brand], [Type], [CustomerId])
VALUES 
    ('ABC123', 'Sedan', 'Toyota', 'Car', 1),
    ('XYZ456', 'SUV', 'Ford', 'Car', 2),
    ('JKL789', 'Motorcycle', 'Honda', 'Motorcycle', 3),
    ('MNO012', 'Truck', 'Chevrolet', 'Truck', 4),
    ('PQR345', 'Hatchback', 'Volkswagen', 'Car', 5),
    ('STU678', 'Motorcycle', 'Harley-Davidson', 'Motorcycle', 6),
    ('VWX901', 'SUV', 'Nissan', 'Car', 7),
    ('YZA234', 'Convertible', 'Mazda', 'Car', 8);

-- Mock data for ParkLog table
INSERT INTO [dbo].[ParkLog] ([VehicleId], [ParkId], [StartTime], [EndTime])
VALUES 
    (1, 1, '2023-11-14T09:30:00', '2023-11-14T11:30:00'),
    (2, 2, '2023-11-14T11:00:00', '2023-11-14T13:30:00'),
    (3, 3, '2023-11-14T10:45:00', '2023-11-14T12:15:00'),
    (4, 4, '2023-11-14T13:15:00', '2023-11-14T16:45:00'),
    (5, 5, '2023-11-14T12:30:00', '2023-11-14T15:00:00'),
    (6, 6, '2023-11-14T14:00:00', '2023-11-14T16:30:00'),
    (7, 7, '2023-11-14T15:30:00', '2023-11-14T18:00:00'),
    (8, 8, '2023-11-14T16:45:00', '2023-11-14T19:15:00');

-- Mock data for Invoice table
INSERT INTO [dbo].[Invoice] ([ParkLogId], [TotalAmount], [IsPayed])
VALUES 
    (1, 10.00, 1),
    (2, 15.50, 1),
    (3, 7.50, 1),
    (4, 20.00, 0),
    (5, 12.50, 1),
    (6, 18.00, 0),
    (7, 22.50, 1),
    (8, 25.00, 0);

-- Mock data for ParkyCoinsConfiguration table
INSERT INTO [dbo].[ParkyCoinsConfiguration] ([ConfigName], [Description], [Amount])
VALUES 
    ('Basic', 'Basic Package', '10.00'),
    ('Premium', 'Premium Package', '20.00'),
    ('Standard', 'Standard Package', '15.00'),
    ('Deluxe', 'Deluxe Package', '25.00'),
    ('Economy', 'Economy Package', '12.00'),
    ('VIP', 'VIP Package', '30.00'),
    ('Gold', 'Gold Package', '35.00'),
    ('Silver', 'Silver Package', '28.00');

-- Mock data for ParkyWallet table
INSERT INTO [dbo].[ParkyWallet] ([Amount])
VALUES 
    (100),
    (150),
    (75),
    (200),
    (125),
    (180),
    (225),
    (250);

-- Mock data for ParkyWalletMovements table
INSERT INTO [dbo].[ParkyWalletMovements] ([Amount], [Date], [MovementType], [ParkyWalletId])
VALUES 
    (10, '2023-11-14T08:30:00', 'Deposit', 1),
    (20, '2023-11-14T10:00:00', 'Deposit', 2),
    (15, '2023-11-14T11:30:00', 'Deposit', 3),
    (25, '2023-11-14T13:00:00', 'Deposit', 4),
    (12, '2023-11-14T14:30:00', 'Deposit', 5),
    (18, '2023-11-14T16:00:00', 'Deposit', 6),
    (22, '2023-11-14T17:30:00', 'Deposit', 7),
    (28, '2023-11-14T19:00:00', 'Deposit', 8);

-- Mock data for PaymentMethod table
INSERT INTO [dbo].[PaymentMethod] ([CustomerId], [PaymentToken], [PaymentMethodType], [CardLastFourDigits], [FullName], [ExpirationDate])
VALUES 
    (1, 'tok_123456789', 'Credit Card', 5678, 'John Doe', '2024-12-31'),
    (2, 'tok_987654321', 'Credit Card', 4321, 'Jane Smith', '2025-06-30'),
    (3, 'tok_456789123', 'PayPal', NULL, 'Alice Johnson', NULL),
    (4, 'tok_789123456', 'Credit Card', 7890, 'Bob Miller', '2024-10-31'),
    (5, 'tok_321654987', 'Credit Card', 3456, 'Chris Wilson', '2023-08-31'),
    (6, 'tok_654987321', 'PayPal', NULL, 'Eva Brown', NULL),
    (7, 'tok_123789456', 'Credit Card', 2345, 'David Taylor', '2025-03-31'),
    (8, 'tok_987456321', 'Credit Card', 8765, 'Sophie Lee', '2023-11-30');
