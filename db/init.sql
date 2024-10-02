CREATE DATABASE SurveyDb;

-- Database creation
CREATE TABLE Surveys (
    Id SERIAL PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Description TEXT
);

CREATE TABLE Questions (
    Id SERIAL PRIMARY KEY,
    Text VARCHAR(500) NOT NULL,
    SurveyId INT NOT NULL REFERENCES Surveys(Id) ON DELETE CASCADE,
    QuestionOrder INT NOT NULL
);

CREATE TABLE Answers (
    Id SERIAL PRIMARY KEY,
    Text VARCHAR(255) NOT NULL,
    QuestionId INT NOT NULL REFERENCES Questions(Id) ON DELETE CASCADE
);

CREATE TABLE Users (
    Id SERIAL PRIMARY KEY
);

CREATE TABLE Interviews (
    Id SERIAL PRIMARY KEY,
    SurveyId INT NOT NULL REFERENCES Surveys(Id) ON DELETE CASCADE,
    UserId INT NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    DateStarted TIMESTAMP NOT NULL,
    DateCompleted TIMESTAMP
);

CREATE TABLE Results (
    Id SERIAL PRIMARY KEY,
    InterviewId INT NOT NULL REFERENCES Interviews(Id) ON DELETE CASCADE,
    QuestionId INT NOT NULL REFERENCES Questions(Id) ON DELETE CASCADE,
    AnswerId INT NOT NULL REFERENCES Answers(Id) ON DELETE CASCADE
);


-- Database filling
INSERT INTO Surveys (Title, Description)
VALUES ('Customer Satisfaction Survey', 'A survey to measure customer satisfaction.');

INSERT INTO Questions (Text, SurveyId, QuestionOrder)
VALUES 
    ('How satisfied are you with our product?', 1, 1),
    ('How would you rate our customer service?', 1, 2),
    ('How easy is it to use our website?', 1, 3),
    ('How likely are you to recommend us to a friend?', 1, 4),
    ('What is your overall experience with our company?', 1, 5);

INSERT INTO Answers (Text, QuestionId) VALUES
    ('Very dissatisfied', 1),
    ('Dissatisfied', 1),
    ('Neutral', 1),
    ('Satisfied', 1),
    ('Very satisfied', 1);

INSERT INTO Answers (Text, QuestionId) VALUES
    ('Very poor', 2),
    ('Poor', 2),
    ('Average', 2),
    ('Good', 2),
    ('Excellent', 2);

INSERT INTO Answers (Text, QuestionId) VALUES
    ('Very difficult', 3),
    ('Difficult', 3),
    ('Neutral', 3),
    ('Easy', 3),
    ('Very easy', 3);

INSERT INTO Answers (Text, QuestionId) VALUES
    ('Not likely at all', 4),
    ('Unlikely', 4),
    ('Neutral', 4),
    ('Likely', 4),
    ('Very likely', 4);

INSERT INTO Answers (Text, QuestionId) VALUES
    ('Very bad', 5),
    ('Bad', 5),
    ('Neutral', 5),
    ('Good', 5),
    ('Very good', 5);

INSERT INTO Users (Id) VALUES (DEFAULT), (DEFAULT);