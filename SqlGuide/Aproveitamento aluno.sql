--Primeiro faz o select do aluno 

select 
                        PF.idPessoa, 
                        PF.nome, 
                        PFP.nome as Professor, 
                        TU.Turma, 
                        DI.nomeDisciplina, 
                        AP.notaId,
						AP.turmaProfessorId,
						NT.nota1,
						NT.nota2,
						NT.nota3,
						NT.nota4
                    from Aproveitamentos AP
                        inner join Pessoas PF
                        on PF.idPessoa = AP.alunoId

                        inner join TurmaProfessor TP
                        on AP.turmaProfessorId = TP.idTurmaProfessor

                        inner join Pessoas PFP
                        on PFP.idPessoa = TP.ProfessorId --professor

                        inner join Turma TU
                        on TU.idTurma = TP.TurmaId

                        inner join Disciplinas DI
                        on DI.idDisciplina = TP.DisciplinaId

						inner join nota NT
						on NT.alunoId = AP.alunoId AND NT.TurmaProfessorId = AP.turmaProfessorId
				where AP.alunoId = 5 
go

SELECT * FROM Pessoas

select pf.nome, * from nota
inner join Pessoas pf on pf.idPessoa = alunoId
--segundo pega as presenças e faltas dele 

select 
count(*) total,
pf.idPessoa,
sum(cast(CH.presenca1 as decimal) ) PRESENCA1,
sum(cast(CH.presenca2 as decimal) ) PRESENCA2,
sum(cast(CH.presenca3 as decimal) ) PRESENCA3, 
sum(cast(CH.presenca4 as decimal) ) PRESENCA4 
from chamada CH
inner join Pessoas pf on pf.idPessoa = CH.alunoId
where CH.aulaId in(select AU.idAula from aula AU
					where AU.TurmaProfessorId = 2)
					And CH.alunoId = 8
group by pf.idPessoa

--teceiro Notas

select pf.nome, nota1,nota2,nota3,nota4 from nota nt 
inner join Pessoas pf
on pf.idPessoa = nt.alunoId
where nt.idNota = @cdNota



select					nt.idNota,
                        pf.nome,
                        nt.nota1,
                        nt.nota2,
                        nt.nota3,
                        nt.nota4 
                    from nota nt 
                        inner join Pessoas pf
                        on pf.idPessoa = nt.alunoId
                    where nt.idNota = @cdNota
