create table ExistingProblem (
    Id              int         primary key     auto_increment,
    Name            text        not null,
    Contains        text        not null,
    Tests           text        not null,
    DataCreated     timestamp   not null,
    EndPointDate    timestamp   not null,
    decided         bit         not null
);

create table Administrator (
    Id              int         primary key     auto_increment,
    LogCode         text        not null,
    PassCode        text        not null,
    AccessLevel     int         not null
);

create table User (
    Id              int         primary key     auto_increment,
    LogCode         text        not null,
    PassCode        text        not null,
    Email           text        not null
);

create table Solution (
    Id              int         primary key     auto_increment,
    Author_id       int         not null,
    Reference       text        not null
);

create table SolutionToProblem (
    SolutionId      int      not null,
    ProblemId       int      not null,
    constraint PK_SolutionToProblem primary key(SolutionId, ProblemId)
);

alter table SolutionToProblem add constraint FK_SolutionId
foreign key (SolutionId) REFERENCES Solution(Id);

alter table SolutionToProblem add constraint FK_SolutionToProblemProblemId
foreign key (ProblemId) references ExistingProblem(Id); 