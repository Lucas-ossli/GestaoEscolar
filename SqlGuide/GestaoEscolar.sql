---------------- Cria��o do banco de dados ----------------
--drop database GerenciamentoEscolarBD
--go

create database GerenciamentoEscolarBD
go

use GerenciamentoEscolarBD
go

create table Cargos
(
	idCargo int primary key identity,
	cargo	varchar(15) not null,
)
go

create table Pessoas
(
	idPessoa		int			 primary key identity, 
	nome			varchar(max) not null,
	cpf				varchar(14)  not null	 unique,
	dataNascimento  date		 not null,
	telefone		varchar(14)      null,
	cargoId			int			 not null references Cargos(idCargo)
)                                                         
go

create table Cadastros
(
	email	 varchar(max),
	senha	 varchar(max) not null check(len(senha)>= 8),
	pessoaId int		  not null references Pessoas(idPessoa) unique
)
go

create table Disciplinas
(
	idDisciplina	 int		  primary key identity,
	nomeDisciplina	 varchar(50)  not null,
	cargaHoraria	 int		  not null 
)
go

create table Turma
(
	idTurma			 int		  	primary key identity,
	Turma			 varchar(max)	not null,
	Descricao		 varchar(max)   null
)
go

create table TurmaProfessor
(
	idTurmaProfessor	int		        primary key identity,
	ano					date			not null,
	TurmaId				int				not null 	references Turma(idTurma),
	ProfessorId			int				not	null 	references Pessoas(idPessoa),
	DisciplinaId		int				not null	references Disciplinas(idDisciplina),
	ativo			    bit				not null   --1 ativo, 0 Inativo
	constraint unique_tp unique (TurmaId, ProfessorId, DisciplinaId, ano)
)
go

create table aula 
(
	idAula int primary key identity,
	TurmaProfessorId		int				not null	references TurmaProfessor(idTurmaProfessor),
	descricao	varchar(max),
	dataAula	date	not null
)
go			

create table chamada
(
	idChamada		int primary key identity,
	aulaId			int				not null	references aula(idAula),
	alunoId			int				not null	references Pessoas(idPessoa),
	presenca1		bit default 1,
	presenca2		bit default 1,
	presenca3		bit default 1,
	presenca4		bit default 1,
	constraint unique_ch unique (aulaId, alunoId)
)
go

create table nota
(
	idNota int primary key identity,
	TurmaProfessorId		int				not null	references TurmaProfessor(idTurmaProfessor),
	alunoId			int				not null	references Pessoas(idPessoa),
	nota1			decimal(10,2)		null	default 0	check (nota1 between 0.0 and 10.0),
	nota2			decimal(10,2)		null	default 0	check (nota2 between 0.0 and 10.0),
	nota3			decimal(10,2)		null	default 0	check (nota3 between 0.0 and 10.0),
	nota4			decimal(10,2)		null	default 0	check (nota4 between 0.0 and 10.0),
	constraint unique_nt unique (turmaProfessorId, alunoId)
)
go

create table Aproveitamentos
(
	turmaProfessorId			int				not null	references TurmaProfessor(idTurmaProfessor), --TODO Melhorar isso 
	alunoId			int				not null	references Pessoas(idPessoa),
	notaId	int		not null references nota(idNota),
	Ativo			bit				not null, --1 ativo, 0 Inativo
	constraint unique_ap unique (turmaProfessorId, alunoId)
)
go



--Inserts Obrigatórios

--Cargo
insert into Cargos (cargo) values('Diretor') --1
insert into Cargos (cargo) values('Professor') -- 2
insert into Cargos (cargo) values('Aluno') -- 3
go

--Diretor
insert into Pessoas(nome,cpf,dataNascimento,cargoId) values('Diretor','12345678',GETDATE(),1)
go
insert into Cadastros (email,senha,pessoaId) values('diretor@gestaoescolar.com', '12345678', 1)
go
--