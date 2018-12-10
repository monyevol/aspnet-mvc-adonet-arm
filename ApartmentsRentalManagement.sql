CREATE SCHEMA Management;
GO
CREATE SCHEMA HumanResources;
GO

CREATE TABLE HumanResources.Employees
(
	EmployeeID	   INT IDENTITY(1, 1),
	EmployeeNumber NVARCHAR(10),
	FirstName      NVARCHAR(20),
	LastName       NVARCHAR(20),
	EmploymentTitle	       NVARCHAR(50), 
	CONSTRAINT PK_Employees PRIMARY KEY(EmployeeID)
);
GO
CREATE TABLE Management.Apartments
(
	ApartmentID	    INT IDENTITY(1, 1),
	UnitNumber	    NVARCHAR(5),
	Bedrooms	    TINYINT,
	Bathrooms	    TINYINT,
	MonthlyRate	    INT,
	SecurityDeposit	INT,
	OccupancyStatus NVARCHAR(25),
	CONSTRAINT PK_Apartments PRIMARY KEY(ApartmentID)
);
GO
CREATE TABLE Management.RentContracts
(
	RentContractID	 INT IDENTITY(1, 1),
	ContractNumber	 INT,
	EmployeeID	     INT,
	ContractDate	 DATE,
	FirstName		 NVARCHAR(20),
	LastName		 NVARCHAR(20),
	MaritalStatus    NVARCHAR(25),
	NumberOfChildren TINYINT,
	ApartmentID	     INT,
	RentStartDate	 DATE,
	CONSTRAINT FK_Registrars FOREIGN KEY(EmployeeID)  REFERENCES HumanResources.Employees(EmployeeID),
	CONSTRAINT FK_Apartments FOREIGN KEY(ApartmentID) REFERENCES Management.Apartments(ApartmentID),
	CONSTRAINT PK_Contracts  PRIMARY KEY(RentContractID)
);
GO
CREATE TABLE Management.Payments
(
	PaymentID	   INT IDENTITY(1, 1),
	ReceiptNumber  INT,
	EmployeeID	   INT,
	RentContractID INT,
	PaymentDate    DATE,
	Amount	       INT,
	Notes	       NVARCHAR(MAX) NOT NULL,
	CONSTRAINT FK_ProcessedBy FOREIGN KEY(EmployeeID) REFERENCES HumanResources.Employees(EmployeeID),
	CONSTRAINT FK_Contracts   FOREIGN KEY(RentContractID) REFERENCES Management.RentContracts(RentContractID),
	CONSTRAINT PK_Payments    PRIMARY KEY(PaymentID)
);
GO