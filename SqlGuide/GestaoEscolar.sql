---------------- Cria��o do banco de dados ----------------
--drop database GerenciamentoEscolarBD
--go

create database GerenciamentoEscolarBD
go

use GerenciamentoEscolarBD
go

------------------- Cria��o das tabelas -------------------
-- Cargos
create table Cargos
(
	idCargo int primary key identity,
	cargo	varchar(15) not null,
)
go

insert into Cargos (cargo) values('Diretor') --1
insert into Cargos (cargo) values('Professor') -- 2
insert into Cargos (cargo) values('Aluno') -- 3

select * from Cargos
go

-- Pessoas
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

insert into Pessoas (nome, cpf, dataNascimento, cargoId) values('H.Dezani', '12345678910-12', GETDATE(), 2)	
insert into Pessoas (nome, cpf, dataNascimento, cargoId) values('Djalma', '43215678910-12', GETDATE(), 2)	
insert into Pessoas (nome, cpf, dataNascimento, cargoId) values('Valeria', '09875678910-12', GETDATE(), 2)	

insert into Pessoas (nome, cpf, dataNascimento, cargoId) values('Lucas', '09126542112-12', GETDATE(), 3)	
insert into Pessoas (nome, cpf, dataNascimento, cargoId) values('Leonardo', '09115978910-12', GETDATE(), 3)	
insert into Pessoas (nome, cpf, dataNascimento, cargoId) values('Pedro', '65423412653-12', GETDATE(), 3)	
insert into Pessoas (nome, cpf, dataNascimento, cargoId) values('Jean', '65428412653-12', GETDATE(), 3)	

select pf.idPessoa, Pf.nome, ca.cargo from Pessoas Pf inner join Cargos ca on ca.idCargo = Pf.cargoId
go


-- Cadastros
create table Cadastros
(
	email	 varchar(max) ,
	senha	 varchar(max) not null check(len(senha)>= 8),
	pessoaId int		  not null references Pessoas(idPessoa)
)
go

-- Disciplinas
create table Disciplinas
(
	idDisciplina	 int		  primary key identity,
	nomeDisciplina	 varchar(50)  not null,
	cargaHoraria	 int		  not null check(cargaHoraria in(40,80))
	--professorId		 int		  not null references Pessoas(idPessoa) -- TODO - irei colocar o id do professor na tabela TurmaProfessor
)

insert into Disciplinas (nomeDisciplina, cargaHoraria) values ('Programa��o Orientada a Objetos', 80)
insert into Disciplinas (nomeDisciplina, cargaHoraria) values ('Engenharia de Software', 80)
insert into Disciplinas (nomeDisciplina, cargaHoraria) values ('Algoritmo e L�gica de Programa��o', 80)
insert into Disciplinas (nomeDisciplina, cargaHoraria) values ('Banco de Dados', 80)

select * from Disciplinas Di
go

-- Turma
--TODO Avaliar se sera necess�rio
create table Turma
(
	idTurma			 int		  	primary key identity,
	Turma			 varchar(max)	not null
)

--delete turma
--drop table Turma


insert into Turma values('1-a')
insert into Turma values('2-b')
insert into Turma values('3-c')
insert into Turma values('4-d')

select * from turma
go

--ProfessorTurma
create table TurmaProfessor
(
	idTurmaProfessor	int		    primary key identity,
	TurmaId				int				not null 	references Turma(idTurma),
	ProfessorId			int				not	null 	references Pessoas(idPessoa),
	DisciplinaId		int				not null	references Disciplinas(idDisciplina),
	--primary key(TurmaId, ProfessorId)
)
--delete TurmaProfessor
--drop table TurmaProfessor

insert into TurmaProfessor(TurmaId, ProfessorId, DisciplinaId) values(1, 3, 1)	-- 1 , Valéria - POO
insert into TurmaProfessor(TurmaId, ProfessorId, DisciplinaId) values(2, 3, 3)	-- 2 , Valéria - LP
insert into TurmaProfessor(TurmaId, ProfessorId, DisciplinaId) values(3, 3, 4)  -- 3 , Valéria - BD
insert into TurmaProfessor(TurmaId, ProfessorId, DisciplinaId) values(1, 3, 4)  -- 1 , Valéria - BD
insert into TurmaProfessor(TurmaId, ProfessorId, DisciplinaId) values(4, 2, 2)  -- 4 , djalma - ES
insert into TurmaProfessor(TurmaId, ProfessorId, DisciplinaId) values(1, 3, 3)  -- 1 , Valéria - LP

select * from TurmaProfessor
go


-- Aproveitamentos
create table Aproveitamentos
(
	--disciplinaId	int				not null	references Disciplinas(idDisciplina),
	turmaProfessorId			int				not null	references TurmaProfessor(idTurmaProfessor), --TODO Melhorar isso 
	alunoId			int				not null	references Pessoas(idPessoa),
	ano				int				not null,
	bimestre		int				not null,
	nota			decimal(10,2)		null	default 0	check (nota between 0.0 and 10.0),
	faltas			int					null	default 0,
	Ativo			int				not null check(Ativo < 3), --1 ativo, 2 Inativo
	--primary key (disciplinaId, alunoId, ano, bimestre, TurmaId)
)
go

--delete Aproveitamentos
--drop table Aproveitamentos
--go
select * from TurmaProfessor
select * from Disciplinas
--iNSERT Lógica de Programação Turmas 1-a - professora Valéria
insert into Aproveitamentos (alunoId, ano, bimestre, nota, faltas, turmaProfessorId, Ativo) values(4, 2023, 1, 10, 12,6, 1)
insert into Aproveitamentos (alunoId, ano, bimestre, nota, faltas, turmaProfessorId, Ativo) values(5, 2023, 1, 10, 12,6, 1)
insert into Aproveitamentos (alunoId, ano, bimestre, nota, faltas, turmaProfessorId, Ativo) values(6, 2023, 1, 10, 12,6, 1)
insert into Aproveitamentos (alunoId, ano, bimestre, nota, faltas, turmaProfessorId, Ativo) values(7, 2023, 1, 10, 12,6, 1)


--iNSERT BD Turmas 1-a - professora Valéria
insert into Aproveitamentos (alunoId, ano, bimestre, nota, faltas, turmaProfessorId, Ativo) values(4, 2023, 1, 10, 12,4, 1)
insert into Aproveitamentos (alunoId, ano, bimestre, nota, faltas, turmaProfessorId, Ativo) values(5, 2023, 1, 10, 12,4, 1)
insert into Aproveitamentos (alunoId, ano, bimestre, nota, faltas, turmaProfessorId, Ativo) values(6, 2023, 1, 10, 12,4, 1)

--iNSERT LP Turmas 2-b - professora Valéria
insert into Aproveitamentos (alunoId, ano, bimestre, nota, faltas, turmaProfessorId, Ativo) values(4, 2023, 1, 10, 12,2, 1)
insert into Aproveitamentos (alunoId, ano, bimestre, nota, faltas, turmaProfessorId, Ativo) values(5, 2023, 1, 10, 12,2, 1)
insert into Aproveitamentos (alunoId, ano, bimestre, nota, faltas, turmaProfessorId, Ativo) values(6, 2023, 1, 10, 12,2, 1)


--iNSERT BD Turmas 3-c - professora Valéria
insert into Aproveitamentos (alunoId, ano, bimestre, nota, faltas, turmaProfessorId, Ativo) values(4, 2023, 1, 10, 12,3, 1)
insert into Aproveitamentos (alunoId, ano, bimestre, nota, faltas, turmaProfessorId, Ativo) values(5, 2023, 1, 10, 12,3, 1)
insert into Aproveitamentos (alunoId, ano, bimestre, nota, faltas, turmaProfessorId, Ativo) values(6, 2023, 1, 10, 12,3, 1)


select * from Aproveitamentos



--SELECT DO INDEX /VIEWS/PROFESSOR/INDEX.CSHTML   (Selecionar as turmas para um professor espec�fico)

-- 
declare @CdProfessor as int 
set @CdProfessor = 3
select  tp.idTurmaProfessor,
		di.nomeDisciplina,
		tu.Turma
		--tu.idTurma,
		--di.idDisciplina
from TurmaProfessor  tp 
inner join Disciplinas di on di.idDisciplina = tp.DisciplinaId
inner join Turma tu on tu.idTurma = tp.TurmaId 
where tp.ProfessorId = @CdProfessor 
order by nomeDisciplina
--

select * from TurmaProfessor --where ProfessorId = 3
order by ProfessorId

select * from Disciplinas
select * from TurmaProfessor



-- Quantidade de alunos na turma 
declare @CdTurmaProf as int 
set @CdTurmaProf = 6

--para cada TurmaProfessorId:
select count(*) as QuantidadeAluno from Aproveitamentos Ap where Ap.turmaProfessorId = @CdTurmaProf
go
--

--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

