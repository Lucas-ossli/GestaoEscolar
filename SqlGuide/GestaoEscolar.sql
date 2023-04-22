---------------- Criação do banco de dados ----------------
drop database GerenciamentoEscolarBD
go

create database GerenciamentoEscolarBD
go

use GerenciamentoEscolarBD
go

------------------- Criação das tabelas -------------------
-- Cargos
create table Cargos
(
	idCargo int primary key identity,
	cargo	varchar(15) not null,
)
go

--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
insert into Cargos (cargo) values('Diretor') --1
insert into Cargos (cargo) values('Professor') -- 2
insert into Cargos (cargo) values('Aluno') -- 3

select * from Cargos
--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

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

--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
insert into Pessoas (nome, cpf, dataNascimento, cargoId) values('H.Dezani', '12345678910-12', GETDATE(), 2)	
insert into Pessoas (nome, cpf, dataNascimento, cargoId) values('Djalma', '43215678910-12', GETDATE(), 2)	
insert into Pessoas (nome, cpf, dataNascimento, cargoId) values('Valéria', '09875678910-12', GETDATE(), 2)	

insert into Pessoas (nome, cpf, dataNascimento, cargoId) values('Lucas', '09126542112-12', GETDATE(), 3)	
insert into Pessoas (nome, cpf, dataNascimento, cargoId) values('Leonardo', '09115978910-12', GETDATE(), 3)	
insert into Pessoas (nome, cpf, dataNascimento, cargoId) values('Pedro', '65423412653-12', GETDATE(), 3)	
insert into Pessoas (nome, cpf, dataNascimento, cargoId) values('Jean', '65428412653-12', GETDATE(), 3)	

select pf.idPessoa, Pf.nome, ca.cargo from Pessoas Pf inner join Cargos ca on ca.idCargo = Pf.cargoId

--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//


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
	cargaHoraria	 int		  not null check(cargaHoraria in(40,80)),
	professorId		 int		  not null references Pessoas(idPessoa)
)
go
--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

insert into Disciplinas (nomeDisciplina, cargaHoraria, professorId) values ('Programação Orientada a Objetos', 80, 1)
insert into Disciplinas (nomeDisciplina, cargaHoraria, professorId) values ('Engenharia de Software', 80, 2)
insert into Disciplinas (nomeDisciplina, cargaHoraria, professorId) values ('Algoritmo e Lógica de Programação', 80, 3)

select di.idDisciplina, Di.nomeDisciplina, di.cargaHoraria, pf.nome as 'Professor' from Disciplinas Di inner join Pessoas Pf on Pf.idPessoa = di.professorId

--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//


-- Turma
--TODO Avaliar se sera necessário
create table Turma
(
	idTurma			 int		  primary key identity,
	Nome		varchar(max)	not null
)
--Trocar o Nome para Nome Turma

--delete turma
--drop table Turma


insert into Turma values('4-a')
insert into Turma values('4-b')
insert into Turma values('2-b')

--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

--ProfessorTurma
create table TurmaProfessor
(
	idTurmaProfessor	int		    primary key identity,
	TurmaId				int				not null	references Turma(idTurma),
	ProfessorId			int					null	references Pessoas(idPessoa)
)

insert into TurmaProfessor values(1, 1) -- 4-a , Dezani
insert into TurmaProfessor values(2, 1)	-- 4-b , Dezani
insert into TurmaProfessor values(2, 2)	-- 4-b , Djlama


--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//




-- Aproveitamentos
create table Aproveitamentos
(
	disciplinaId	int				not null	references Disciplinas(idDisciplina),
	alunoId			int				not null	references Pessoas(idPessoa),
	ano				int				not null,
	bimestre		int				not null,
	nota			decimal(10,2)		null	default 0	check (nota between 0.0 and 10.0),
	faltas			int					null	default 0,
	TurmaId			int				not null	references Turma(idTurma), --TODO Melhorar isso 
	Ativo			int				not null check(Ativo < 3), --1 ativo, 2 Inativo
	primary key (disciplinaId, alunoId, ano, bimestre)
)
go

--delete Aproveitamentos
--drop table Aproveitamentos
--go

insert into Aproveitamentos (disciplinaId, alunoId, ano, bimestre, nota, faltas, TurmaId, Ativo) values(1,5, 2023, 1, 10, 12,1, 1)
insert into Aproveitamentos (disciplinaId, alunoId, ano, bimestre, nota, faltas, TurmaId, Ativo) values(1,6, 2023, 1, 10, 12,1, 1)
insert into Aproveitamentos (disciplinaId, alunoId, ano, bimestre, nota, faltas, TurmaId, Ativo) values(1,7, 2023, 1, 10, 12,1, 1)

insert into Aproveitamentos (disciplinaId, alunoId, ano, bimestre, nota, faltas, TurmaId, Ativo) values(2,5, 2023, 1, 10, 12,2, 1)
insert into Aproveitamentos (disciplinaId, alunoId, ano, bimestre, nota, faltas, TurmaId, Ativo) values(2,6, 2023, 1, 10, 12,2, 1)
insert into Aproveitamentos (disciplinaId, alunoId, ano, bimestre, nota, faltas, TurmaId, Ativo) values(2,7, 2023, 1, 10, 12,2, 1)

select * from Aproveitamentos
--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//





--SELECT DO INDEX /VIEWS/PROFESSOR/INDEX.CSHTML   (Selecionar as turmas para um professor específico)

--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

declare @CdProfessor as int 
set @CdProfessor = 1

select  di.nomeDisciplina, tu.Nome, tu.idTurma, di.idDisciplina 
from Disciplinas di, TurmaProfessor Tp
inner join Turma tu on tu.idTurma = Tp.TurmaId
where di.professorId = @CdProfessor and Tp.ProfessorId = @CdProfessor


declare @CdTurma as int 
set @CdTurma = 1
declare @CdDisciplina as int 
set @CdDisciplina = 1
--para cada TurmaId e idDisciplina:
select count(*) as QuantidadeAluno from Aproveitamentos Ap 
where Ap.TurmaId = @CdTurma and Ap.disciplinaId = @CdDisciplina



select TurmaId as 'Todas as Turmas do Professor' from TurmaProfessor PT where PT.ProfessorId = @CdProfessor 
--FOREACHE TURMAID DO:

--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

