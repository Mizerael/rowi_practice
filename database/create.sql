create table if not exists ExistingProblem (
    Id              int         primary key     auto_increment,
    Price           int         not null,
    Name            text        not null,
    Contains        text        not null,
    Tests           text        not null,
    DataCreated     timestamp   not null,
    EndPointDate    timestamp   not null
);

create table if not exists User (
    Id              int         primary key     auto_increment,
    LogCode         text        not null,
    PassCode        text        not null,
    Email           text        not null
);

create table if not exists Solution (
    Id              int         primary key     auto_increment,
    Author_id       int         not null,
    Reference       text        not null,
    ProblemId       int         not null,
    Approve         tinyint(1)  not null
);

alter table User add constraint FK_SolutionId
foreign key (SolutionId) REFERENCES Solution(Id);

alter table Solution add constraint FK_ProblemId
foreign key (ProblemId) references ExistingProblem(Id); 

insert into User (LogCode, PassCode, email) values ("aboba", "aboba", "aboba");
insert into ExistingProblem (Price, Name, Contains, Tests) values (1, "aboba", "aboba", "aboba");
