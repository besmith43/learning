CREATE VIEW poll_result AS
select poll.question, pollAnswers.answer, count(vote.id) as votes
from poll
join pollAnswers on poll.id = pollAnswers.poll_id
left join vote on pollAnswers.id = vote.answer_id
group by poll.question, pollAnswers.answer
order by poll.id, pollAnswers.id;