
--Primeiro Cria a pessoa

insert into Pessoas (nome, cpf, dataNascimento, cargoId) values('H.Dezani', '12345678910-12', GETDATE(), 2)	


--Criar Turma
insert into Turma values('1-a')

--Criar Disciplina 
insert into Disciplinas (nomeDisciplina, cargaHoraria) values ('Programa��o Orientada a Objetos', 80)

--Por fim criar TurmaProfessor [ QUE CONTÉM:  TurmaID, ProfessorID , DisciplinaID
insert into TurmaProfessor(TurmaId, ProfessorId, DisciplinaId) values(1, 3, 1)	-- 1 , Valéria - POO

select * from TurmaProfessor
select PF.nome,PF.idPessoa, DI.nomeDisciplina, DI.idDisciplina, TU.Turma, TU.idTurma from TurmaProfessor tp
inner join Pessoas PF on tp.ProfessorId = PF.idPessoa
inner join Disciplinas DI on tp.DisciplinaId = DI.idDisciplina
inner join Turma TU on tp.TurmaId = TU.idTurma
order by idTurmaProfessor

--Há de se criar os dias que tera aula também Para que possa haver chamada -- COTÉM O ID TurmaProfessorID
insert into aula values(6,'Apresentação ',CONVERT(date, '2023-05-15'))




