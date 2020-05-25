CREATE DATABASE SportsClub;

USE SportsClub;

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

 
INSERT INTO Sports (Name) VALUES ('Football'), ('Volleyball'), ('Tennis'),
('Athletics'), ('Badminton'), ('Basketball'), ('Cricket'), ('Golf');

INSERT INTO Trainers (Name) VALUES ('Ivan'), ('Peter'), ('Lili'),
('Amelia'), ('Chloe'), ('Aiden'), ('Charles'), ('Gemma');

INSERT INTO Teams (Name, SportId, TrainerId) VALUES ('Angels', 2, 3), ('Dragons', 1, 1), 
('Flash', 3, 2),('Avengers', 4, 5), ('Black Panters', 5, 6),
('Soliders', 6, 8), ('Savages', 6, 7), ('Soliders', 4, 5), ('Influencers', 8, 8), 
 ('Power', 7, 4), ('Bosses', 8, 8),('Royalty', 7, 4);

INSERT INTO Players (Name, Age, TeamId) VALUES ('Kiki', 17, 2), ('Sisi', 13, 3), ('Koko', 16, 1),
('Anastasia', 17, 2), ('Belle', 13, 3), ('Bonnie', 16, 1), ('Charlotte', 12, 4),
('Duke', 13, 5), ('Flora', 16, 6), ('Felix', 16, 7), ('Hector', 16, 4), ('Mary', 14, 6),
('Zack', 16, 1), ('Gina', 16, 1),('Brooklyn', 16, 1),('Sebastian', 17, 2), ('Wendy', 17, 2), 
('Judy', 17, 2),('Anexandra', 13, 3), ('Antonie', 13, 3),('Josef', 13, 3),
('Kamila', 13, 4), ('Katerine', 14, 4),('Mikolas', 15, 4),('Olivia', 13, 5), ('Nasko', 13, 5),
('Adam', 13, 5), ('Harry', 13, 5),('Amadeus', 14, 6), ('Alisha', 16, 6),
('Alejandro', 16, 7), ('Alfredo', 16, 7),('Alica', 16, 7), ('Dino', 16, 7),
('Minni', 16, 9), ('Peter', 16, 9), ('Roxanne', 16, 9),('Georgio', 12, 10),
('Gian', 12, 10), ('Emilia', 12, 10), ('Antonia', 12, 10), ('Caprice', 12, 10), 
('Cherry', 16, 11), ('Emerald', 16, 11), ('Hazel', 16, 11), ('Ivory', 16, 11), ('Kim', 16, 11),
('Christal', 17, 12), ('Pearle', 17, 12), ('Joshua', 17, 12), ('Tom', 17, 12), ('Cole', 17, 12),
('Buddy', 16, 6), ('Kris', 16, 9);

insert into Games (FirstTeamId,SecondTeamId) Values
(1,2), (5,8),(11,2), (7,8),(3,4), (10,9),(8,12), (3,5),(4,6), (11,12),(7,2), (6,8), (5,6);