CREATE DATABASE SportsClubTest;

USE SportsClubTest;

CREATE TABLE Sports
(
    Id INT IDENTITY PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);

CREATE TABLE Trainers
(
    Id INT IDENTITY PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);

CREATE TABLE Teams
(
    Id INT IDENTITY PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    SportId INT,
    TrainerId INT,
    FOREIGN KEY (SportId) REFERENCES Sports(Id),
    FOREIGN KEY (TrainerId) REFERENCES Trainers(Id)
);

CREATE TABLE Players
(
    Id INT IDENTITY PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Age INT,
    TeamId INT,
    FOREIGN KEY (TeamId) REFERENCES Teams(Id)
);

create table Games
(
	Id int not null identity,
	FirstTeamId int,
	SecondTeamId int,
	foreign key (FirstTeamId) references Teams(Id),
	foreign key (SecondTeamId) references Teams(Id),
);